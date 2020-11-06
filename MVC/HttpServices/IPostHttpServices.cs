using Domain.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication14.HttpServices
{
    public interface IPostHttpServices
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<Post> GetByIdAsync(int id);
        public Task InsertAsync(Post insertedEntity);
        public Task UpdateAsync(Post updatedEntity);
        public Task DeleteAsync(Post post);

    }
}