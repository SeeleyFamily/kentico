using CMS.IO;
using CMS.MediaLibrary;
using ImageMagick;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions;
using System;
using System.IO;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Services
{
	public class ImageMagickImageCompressionService : ImageCompressionService
	{

		public ImageMagickImageCompressionService()
		{
			CompressionServicePrefix = "image_magick";
		}

		protected override void CompressImage(Stream stream)
		{
			stream.Position = 0;
			var optimizer = new ImageOptimizer();
			optimizer.OptimalCompression = true;
			optimizer.LosslessCompress(stream);
		}
	}
}
