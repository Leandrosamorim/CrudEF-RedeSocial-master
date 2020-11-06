using Domain.Models.Interfaces.Repositories;
using Domain.Models.Models;
using Infrastructure.Context;
using Infrastructure.Context.WebApplication14.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly WebApplication14Context _context;

        public PostRepository(WebApplication14Context context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            var postModel = await _context.Post.FindAsync(id);
            _context.Post.Remove(postModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Post.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Post.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Post insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post updatedEntity)
        {
            _context.Update(updatedEntity);
            await _context.SaveChangesAsync();
        }
    }

}
