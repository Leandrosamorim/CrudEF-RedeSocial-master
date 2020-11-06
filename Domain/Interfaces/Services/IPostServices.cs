using Domain.Models.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IPostServices
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task InsertAsync(Post updatedEntity);
        Task UpdateAsync(Post insertedEntity);
        Task DeleteAsync(Post deletedEntity);
    }
}