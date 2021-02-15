using Azure.Storage.Blobs;
using MetadataExtractor.Formats.Jpeg;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationImageApi.Services
{
    public class LocationImageService : ILocationImageService
    {
        private readonly string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        private readonly BlobServiceClient blobServiceClient;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LocationImageService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("StorageConnectionString");

            blobServiceClient = new BlobServiceClient(connectionString);
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<string>> GetLocations()
        {
            var locations = blobServiceClient.GetBlobContainers().AsEnumerable().Select(ci => ci.Name).ToList();
            return locations;
        }

        public async Task<IEnumerable<Uri>> GetImagesAsync(string location)
        {
            List<Uri> fileUrls = new List<Uri>();

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, location);
            var blobs = blobContainerClient.GetBlobsAsync();

            await foreach (var blobItem in blobs)
            {
                var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);

                fileUrls.Add(blobClient.Uri);
            }

            return fileUrls;
        }

        public async Task<string> GetImageMetadataAsync(Uri file)
        {
            var blobDownloadDir = Path.Combine(webHostEnvironment.ContentRootPath, "file.jpg");

            await DownloadBlobAsync(file, blobDownloadDir);
            return ExtractFileMetadata(blobDownloadDir);
        }

        private static async Task DownloadBlobAsync(Uri file, string blobDownloadDir)
        {
            var blobClient = new BlobClient(file);
            var response = await blobClient.DownloadAsync();
            var value = response.Value;

            using FileStream downloadStream = File.OpenWrite(blobDownloadDir);
            value.Content.CopyTo(downloadStream);
        }

        private static string ExtractFileMetadata(string tempFileDirectory)
        {
            using FileStream fileStream = File.OpenRead(tempFileDirectory);
            
            StringBuilder stringBuilder = new StringBuilder();
            var metadataDirectories = JpegMetadataReader.ReadMetadata(fileStream);

            foreach (var directory in metadataDirectories)
                foreach (var tag in directory.Tags)
                    stringBuilder.AppendLine($"{tag.Name} = {tag.Description}");

            return stringBuilder.ToString();
        }
    }
}
