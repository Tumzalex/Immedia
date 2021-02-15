using LocationImageApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LocationImageApi.Services
{
    public interface ILocationImageService
    {
        Task<IEnumerable<string>> GetLocations();
        Task<IEnumerable<Uri>> GetImagesAsync(string location);
        Task<string> GetImageMetadataAsync(Uri file);
    }
}
