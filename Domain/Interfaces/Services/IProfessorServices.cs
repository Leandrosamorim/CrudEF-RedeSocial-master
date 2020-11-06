using Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Interfaces.Services
{
    public interface IProfessorServices
    {
        Task<IEnumerable<Professor>> GetAllAsync();
        Task<Professor> GetByIdAsync(int id);
        Task InsertAsync(Professor updatedEntity);
        Task UpdateAsync(Professor insertedEntity);
        Task DeleteAsync(Professor deletedEntity);
    }
}
