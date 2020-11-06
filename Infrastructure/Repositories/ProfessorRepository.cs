using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interfaces.Repositories;
using Domain.Models.Models;
using Infrastructure.Context.WebApplication14.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly WebApplication14Context _context; 

        public ProfessorRepository(WebApplication14Context context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            var profModel = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(profModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await _context.Professor.ToListAsync();
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            return await _context.Professor.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(Professor insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Professor updatedEntity)
        {
            _context.Update(updatedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
