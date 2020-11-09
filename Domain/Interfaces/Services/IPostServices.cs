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
        Task InsertAsync(Post updatedEntity, string base64);
        Task UpdateAsync(Post insertedEntity, string base64);
        Task DeleteAsync(Post deletedEntity);
    }
}