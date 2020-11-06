using Azure.Storage.Blobs;
using Domain.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public class BlobServices : IBlobService
    {
            private readonly BlobServiceClient _blobServiceClient;
            private const string _container = "userimages";

            public BlobServices(string storageAccount)
            {
                _blobServiceClient = new BlobServiceClient(storageAccount);
            }

            public async Task<string> UploadAsync(string Uri)
            {
            using var webClient = new WebClient();
            var imageBytes = webClient.DownloadData(Uri);

            var stream = new MemoryStream(imageBytes);

                var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

                if (!await containerClient.ExistsAsync())
                {
                    await containerClient.CreateIfNotExistsAsync();
                    await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
                }

                var blobClient = containerClient.GetBlobClient($"{Guid.NewGuid()}.jpg");

                await blobClient.UploadAsync(stream, true);

                return blobClient.Uri.ToString();
            }

            public async Task DeleteAsync(string BlobName)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_container);

                var blob = new BlobClient(new Uri(BlobName));

                var blobClient = containerClient.GetBlobClient(blob.Name);

                await blobClient.DeleteIfExistsAsync();
            }


        }
    }

