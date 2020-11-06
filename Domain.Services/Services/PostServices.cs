using Azure.Storage.Blobs.Models;
using Domain.Models.Interfaces.Repositories;
using Domain.Models.Interfaces.Services;
using Domain.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
        public class PostServices : IPostServices
        {
            private readonly IPostRepository _postRepository;
            private readonly IBlobService _blobService;
            private readonly IQueueMessage _queueService;

            public PostServices(IPostRepository postRepository, IBlobService blobService, IQueueMessage queueMessage)
            {
                _postRepository = postRepository;
                _blobService = blobService;
                _queueService = queueMessage;
            }

            public async Task DeleteAsync(Post post)
            {
                await _blobService.DeleteAsync(post.BlobUri);
                await _postRepository.DeleteAsync(post.Id);

            }

            public async Task<IEnumerable<Post>> GetAllAsync()
            {
                return await _postRepository.GetAllAsync();
            }

            public async Task<Post> GetByIdAsync(int id)
            {
                return await _postRepository.GetByIdAsync(id);
            }

            public async Task InsertAsync(Post insertedEntity)
            {
                var newUri = await _blobService.UploadAsync(insertedEntity.BlobUri);
                insertedEntity.BlobUri = newUri;

                var message = new
                {
                    ImageUri = insertedEntity.BlobUri,
                    Id = insertedEntity.Id,
                };
                var jsonMessage = JsonConvert.SerializeObject(message);
                var bytesJsonMessage = UTF8Encoding.UTF8.GetBytes(jsonMessage);
                string jsonMessageBase64 = Convert.ToBase64String(bytesJsonMessage);

                await _queueService.SendAsync(jsonMessageBase64);
                await _postRepository.InsertAsync(insertedEntity);
            }

            public async Task UpdateAsync(Post updatedEntity)
            {
                if (updatedEntity.BlobUri != null)
                {
                    await _blobService.DeleteAsync(updatedEntity.BlobUri);

                    var newUri = await _blobService.UploadAsync(updatedEntity.BlobUri);
                    updatedEntity.BlobUri = newUri;
                }
                await _postRepository.UpdateAsync(updatedEntity);
            }
        }
}
