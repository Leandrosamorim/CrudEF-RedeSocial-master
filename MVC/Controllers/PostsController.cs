using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Models;
using Infrastructure.Context;
using Domain.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Domain.Models.Exceptions;
using WebApplication14.Areas.Identity;
using WebApplication14.HttpServices;
using System.Transactions;
using System.IO;

namespace WebApplication14.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostHttpServices _postServices;
        private readonly IAuthorizationService _authService;
        private readonly UserManager<IdentityUser> _post;

        public PostsController(IPostHttpServices postServices, IProfessorHttpServices professorServices, IAuthorizationService authService, UserManager<IdentityUser> post)
        {
            _postServices = postServices;
            _authService = authService;
            _post = post;
        }

        // GET: Posts
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            return View(await _postServices.GetAllAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postServices.GetByIdAsync(id.Value);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile ImageFile)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                post.OwnerEmail = User.Identity.Name;
                await _postServices.InsertAsync(post, ConvertIFormFileToBase64(ImageFile));

                scope.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postServices.GetByIdAsync(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authService.AuthorizeAsync(
                                                  User, post,
                                                  ContactOperations.Update);

            if (User.Identity.Name != post.OwnerEmail && !(post.OwnerEmail == null))
                {
                    return Forbid();
                }


                return View(post);
            }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post, IFormFile ImageFile)
        {
            if (post.Id <= 0)
            {
                return NotFound();
            }

            if (post == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _postServices.UpdateAsync(post, ConvertIFormFileToBase64(ImageFile));

                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_postServices.GetByIdAsync(post.Id) != null)
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
            return View(post);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postServices.GetByIdAsync(id.Value);

            if (post == null)
            {
                return NotFound();
            }
            var isAuthorized = await _authService.AuthorizeAsync(
                                                 User, post,
                                                 ContactOperations.Update);

            if (User.Identity.Name != post.OwnerEmail)
            {
                return Forbid();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Post post)
            {
                var profEntity = await _postServices.GetByIdAsync(post.Id);
                await _postServices.DeleteAsync(profEntity);
                return RedirectToAction(nameof(Index));
            }

        public string ConvertIFormFileToBase64(IFormFile image)
        {
            if (image != null)
            {
                string imageBase64;
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    imageBase64 = Convert.ToBase64String(fileBytes);
                }

                return imageBase64;
            }
            return null;
        }
    }
}
