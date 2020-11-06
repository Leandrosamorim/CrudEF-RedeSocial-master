using Domain.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication14.HttpServices
{
    public interface IProfessorHttpServices
    {
        public Task<IEnumerable<Professor>> GetAllAsync();
        public Task<Professor> GetByIdAsync(int id);
        public Task InsertAsync(Professor insertedEntity);
        public Task UpdateAsync(Professor updatedEntity);
        public Task DeleteAsync(Professor post);
    }
}