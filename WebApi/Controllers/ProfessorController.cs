using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Exceptions;
using Domain.Models.Interfaces.Services;
using Domain.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorServices _professorService;

        public ProfessorController(
            IProfessorServices professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessorEntity()
        {
            var livros = await _professorService.GetAllAsync();
            return livros.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var professorEntity = await _professorService.GetByIdAsync(id);

            if (professorEntity == null)
            {
                return NotFound();
            }

            return professorEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessorEntity(int id, Professor professorEntity, Stream stream)
        {
            if (id != professorEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _professorService.UpdateAsync(professorEntity, stream);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessorEntity(Professor professorEntity, Stream stream)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _professorService.InsertAsync(professorEntity, stream);

                return CreatedAtAction(
                    "GetProfessorEntity",
                    new { id = professorEntity.Id }, professorEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professor>> DeleteProfessorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var professorEntity = await _professorService.GetByIdAsync(id);
            if (professorEntity == null)
            {
                return NotFound();
            }

            await _professorService.DeleteAsync(id);

            return professorEntity;
        }    
    }
}
