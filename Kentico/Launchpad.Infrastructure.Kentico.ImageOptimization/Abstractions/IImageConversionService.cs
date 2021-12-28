using CMS.MediaLibrary;
using CMS.IO;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions
{
    public interface IImageConversionService
    {        
        string GetOptimizedImageType();
        string GetOptimizedImagePhysicalFilePath(MediaFileInfo mediaFileInfo);
        byte[] CovertImage(FileStream fileStream);
        bool CovertImage(MediaFileInfo mediaFileInfo, out FileInfo file);
    }
}
