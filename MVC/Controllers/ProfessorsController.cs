using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Models;
using WebApplication14.Data;
using Domain.Models.Interfaces.Services;
using Domain.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Domain.Services.Services;
using WebApplication14.Areas.Identity;
using Infrastructure.Context.WebApplication14.Data;
using System.Net.Http;
using Newtonsoft.Json;
using WebApplication14.Models;
using WebApplication14.HttpServices;

namespace WebApplication14.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly IProfessorHttpServices _professorServices;
        private readonly IAuthorizationService _authService;
        private readonly UserManager<IdentityUser> _professor;

        public ProfessorsController(IProfessorHttpServices professorServices, IAuthorizationService authService, UserManager<IdentityUser> professor)
        {
            _professorServices = professorServices;
            _authService = authService;
            _professor = professor;
        }

        // GET: Professors
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            return View(await _professorServices.GetAllAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _professorServices.GetByIdAsync(id.Value);

            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Professor professor, IFormFile ImageFile)
        {
                try 
                { 
                await _professorServices.InsertAsync(professor);
                return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }

            return View(professor);
        }

        // GET: Professors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _professorServices.GetByIdAsync(id.Value);
            if (professor == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authService.AuthorizeAsync(
                                                  User, professor,
                                                  ContactOperations.Update);

            if (User.Identity.Name != professor.Email)
            {
                return Forbid();
            }

            
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Professor professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }

            if (professor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Form.Files.SingleOrDefault();

                    await _professorServices.UpdateAsync(professor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_professorServices.GetByIdAsync(professor.Id) != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _professorServices.GetByIdAsync(id.Value);
  
            if (professor == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authService.AuthorizeAsync(
                                                 User, professor,
                                                 ContactOperations.Update);

            if (User.Identity.Name != professor.Email)
            {
                return Forbid();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Professor professor)
        {
            var profEntity = await _professorServices.GetByIdAsync(professor.Id);
            await _professorServices.DeleteAsync(profEntity);
            return RedirectToAction(nameof(Index));
        }
    }
}
