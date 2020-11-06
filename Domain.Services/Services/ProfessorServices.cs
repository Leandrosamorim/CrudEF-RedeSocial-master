using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interfaces.Services;
using Domain.Models.Interfaces.Repositories;
using Domain.Models.Models;
using Newtonsoft.Json;

namespace Domain.Services.Services
{
    public class ProfessorServices : IProfessorServices
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IBlobService _blobService;
        private readonly IQueueMessage _queueService;

        public ProfessorServices(IProfessorRepository professorRepository, IBlobService blobService, IQueueMessage queueMessage)
        {
            _professorRepository = professorRepository;
            _blobService = blobService;
            _queueService = queueMessage;
        }

        public async Task DeleteAsync(Professor professor)
        {
            await _blobService.DeleteAsync(professor.ImageUri);
            await _professorRepository.DeleteAsync(professor.Id);

        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await _professorRepository.GetAllAsync();
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            return await _professorRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Professor insertedEntity)
        {
           var newUri = await _blobService.UploadAsync(insertedEntity.ImageUri);
            insertedEntity.ImageUri = newUri;


            await _professorRepository.InsertAsync(insertedEntity);

            var message = new
            {
                ImageUri = insertedEntity.ImageUri,
                Id = insertedEntity.Id,
            };
            var jsonMessage = JsonConvert.SerializeObject(message);
            var bytesJsonMessage = UTF8Encoding.UTF8.GetBytes(jsonMessage);
            string jsonMessageBase64 = Convert.ToBase64String(bytesJsonMessage);

            await _queueService.SendAsync(jsonMessageBase64);
        }

        public async Task UpdateAsync(Professor updatedEntity)
        {
            if(updatedEntity.ImageUri != null)
            {
                await _blobService.DeleteAsync(updatedEntity.ImageUri);

                var newUri = await _blobService.UploadAsync(updatedEntity.ImageUri);
                updatedEntity.ImageUri = newUri;
            }
            await _professorRepository.UpdateAsync(updatedEntity);
        }
    }
}
