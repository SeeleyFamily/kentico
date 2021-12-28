using System.Configuration;
using System.IO;
using TinifyAPI;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Services
{
	public class TinyPngImageCompressionService : ImageCompressionService
	{
		#region Properties		
		#endregion

		public TinyPngImageCompressionService()
		{
			CompressionServicePrefix = "tiny_png";
			Tinify.Key = ConfigurationManager.AppSettings["TinyPngApiKey"];			
		}
		

		protected override void CompressImage(Stream stream)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.Position = 0;
				stream.CopyTo(memoryStream);
				var buffer = memoryStream.GetBuffer();
				var source = Tinify.FromBuffer(buffer);
				var resultData = source.ToBuffer().Result;
				stream.Position = 0;
				stream.SetLength(0);
				stream.Write(resultData, 0, resultData.Length);
				stream.Flush();
				memoryStream.Close();
			}
		}
	}
}
