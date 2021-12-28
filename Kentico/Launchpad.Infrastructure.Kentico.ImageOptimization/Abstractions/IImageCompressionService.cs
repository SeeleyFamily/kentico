using CMS.MediaLibrary;
using CMS.IO;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions
{
    public interface IImageCompressionService
    {
        void CompressImage(MediaFileInfo mediaFileInfo);
    }
}
