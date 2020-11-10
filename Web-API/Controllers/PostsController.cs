﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Models;
using Infrastructure.Context;
using Domain.Services.Services;
using Infrastructure.Context.WebApplication14.Data;
using Infrastructure.Migrations.Post;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly WebApplication14Context _context;

        public PostsController(IPostServices postServices, WebApplication14Context context)
        {
            _postServices = postServices;
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            var posts = await _postServices.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postServices.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, CreateAndUpdateHttpPost postModel)
        {
            var post = postModel.Post;
            var imageBase64 = postModel.Uri;

            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _postServices.UpdateAsync(post, imageBase64);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreateAndUpdateHttpPost post)
        {
            var postModel = post.Post;
            var uri = post.Uri;

            await _postServices.InsertAsync(postModel, uri);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _postServices.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postServices.DeleteAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
