using Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task InsertAsync(Post insertedPost);
        Task UpdateAsync(Post updatedPost);
        Task DeleteAsync(int id);
    }
}
