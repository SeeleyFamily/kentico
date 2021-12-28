using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Core.Abstractions.Services
{
    public interface IMediaService
    {
        MediaFile GetMediaFile(Guid guid);
        string GetMediaUrl(Guid guid);
        IEnumerable<MediaFile> GetGalleryFolderAssets(Guid guid);
    }
}
