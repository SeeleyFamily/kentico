using System;
using System.Collections;
using System.Collections.Generic;


namespace Launchpad.Core.Models
{

    public class People : PageNode
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PreviewCtaText { get; set; }
        public string PreviewTitle { get; set; }
        public string PreviewSnippet { get; set; }
        public string PreviewImage { get; set; }
        public string PeopleTitle { get; set; }

        public People()
        {

        }

        public People(PageNode pageNode)
        : base(pageNode)
        {

        }

    }

}
