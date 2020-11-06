using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Models;

namespace Domain.Models.Interfaces.Repositories
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> GetAllAsync();
        Task<Professor> GetByIdAsync(int id);
        Task InsertAsync(Professor updatedEntity);
        Task UpdateAsync(Professor insertedEntity);
        Task DeleteAsync(int id);
    }
}
