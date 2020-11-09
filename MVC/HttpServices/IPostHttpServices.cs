using Domain.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication14.HttpServices
{
    public interface IPostHttpServices
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<Post> GetByIdAsync(int id);
        public Task InsertAsync(Post insertedEntity, string base64);
        public Task UpdateAsync(Post updatedEntity, string base64);
        public Task DeleteAsync(Post post);

    }
}