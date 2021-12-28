using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Launchpad.Infrastructure.Kentico.Web.Filters
{
    public class SanitizeUrlFilterStream : Stream
    {
        private readonly MemoryStream cacheStream = new MemoryStream();
        private readonly ResultExecutedContext filterContext;
        private readonly Stream responseFilter;


        public SanitizeUrlFilterStream(ResultExecutedContext filterContext)
        {
            this.filterContext = filterContext;
            this.responseFilter = filterContext.HttpContext.Response.Filter;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // Write to our cache stream instead so we can get the entire html output
            cacheStream.Write(buffer, 0, count);
        }
        /// <remarks>
        /// The purpose of overriding this method is to capture the entire response instead of chunked responses
        /// so that we can apply filters to the entire output unbroken.
        /// </remarks>
        public override void Flush()
        {
            if (cacheStream.Length > 0)
            {
                FilterResponse();

                // Wipe the cache stream
                cacheStream.SetLength(0);
            }


            responseFilter.Flush();
        }
        public override void Close()
        {
            // If flush hasn't occurred, write everything at once before close (TODO: Better test, perhaps for the Kentico stream)
            if (!responseFilter.CanRead && cacheStream.Length > 0)
            {
                FilterResponse();
            }

            responseFilter.Close();
        }
        private void FilterResponse()
        {
            //Regex getMediaLibraryPathSanitizedRegex = new Regex("\"/getmedia/(.[^\"]*?)(jpg|png|jpeg)(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            // remove optimize media from href links
            // var getMediaLibraryPathHrefSanitizedRegex = new Regex("href=\"/optimize/getmedia/(.*?)(jpg|png|jpeg)(.*?)(format=webp)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //html = getMediaLibraryPathHrefSanitizedRegex.Replace(html, m => {
            //    var url = CDNBaseURL + m.Groups[0].Value.Replace("optimize/", "").Replace("&format=webp", "").Replace("?format=webp", "");
            //    return url;
            //});

            // Rewrite the buffer with our modified stream buffer


            string html = Encoding.UTF8.GetString(cacheStream.ToArray(), 0, (int)cacheStream.Length);

            html = RemoveAzureWebsiteReferencesFromAnchors(html);

            byte[] buffer = Encoding.UTF8.GetBytes(html);
            responseFilter.Write(buffer, 0, buffer.Length);

        }

        /// <summary>
        /// Adds the lazy loading classes found in <see cref="lazyLoadImageClasses"/> to "img" elements and any elements with "background-image" styles if they do not already have them.
        /// </summary>
        private string RemoveAzureWebsiteReferencesFromAnchors(string html)
        {
            IConfiguration config = AngleSharp.Configuration.Default
                .WithDefaultLoader();

            IBrowsingContext context = BrowsingContext.New(config);

            IHtmlParser htmlParser = context.GetService<IHtmlParser>();

            IHtmlDocument document = htmlParser.ParseDocument(html);

            // Check for any "<img>" elements and elements that have a "background-image" style and apply lazy load classes as needed
            foreach (IElement ele in document.GetElementsByTagName("a"))
            {
                
                // remove the "src" and add it to the data attributes to be lazy-loaded
                string src = ele.GetAttribute("href");
                if (!string.IsNullOrWhiteSpace(src))
                {


                    Regex getMediaLibraryPathSanitizedRegex = new Regex("\"/getmedia/(.[^\"]*?)(jpg|png|jpeg)(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    //remove optimize media from href links
                    src = getMediaLibraryPathSanitizedRegex.Replace(src, m => {
                        var url = m.Groups[0].Value.Replace("optimize/", "").Replace("&format=webp", "").Replace("?format=webp", "");
                        return url;
                    });


                    //https://azurewebsite.net/communities/arizona/rock-sun-place
                    //ele.SetAttribute("data-image", src);
                    //ele.SetAttribute("data-image-webp", src);

                    ele.SetAttribute("src", src);
                }
            }

            return document.ToHtml();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.responseFilter.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.responseFilter.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.responseFilter.Read(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return this.responseFilter.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.responseFilter.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.responseFilter.CanWrite; }
        }

        public override long Length
        {
            get { return this.responseFilter.Length; }
        }

        public override long Position
        {
            get { return this.responseFilter.Position; }
            set { this.responseFilter.Position = value; }
        }
    }
}
