using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogPost.Data;
using BlogPost.Models;

namespace BlogPost.Controllers
{
    public class AddPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddPosts
        public async Task<IActionResult> Index()
        {
              return View(await _context.addpost.ToListAsync());
        }

        // GET: AddPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.addpost == null)
            {
                return NotFound();
            }

            var addPost = await _context.addpost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addPost == null)
            {
                return NotFound();
            }

            return View(addPost);
        }

        // GET: AddPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,CreatedDate")] AddPost addPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addPost);
        }

        // GET: AddPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.addpost == null)
            {
                return NotFound();
            }

            var addPost = await _context.addpost.FindAsync(id);
            if (addPost == null)
            {
                return NotFound();
            }
            return View(addPost);
        }

        // POST: AddPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,CreatedDate")] AddPost addPost)
        {
            if (id != addPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddPostExists(addPost.Id))
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
            return View(addPost);
        }

        // GET: AddPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.addpost == null)
            {
                return NotFound();
            }

            var addPost = await _context.addpost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addPost == null)
            {
                return NotFound();
            }

            return View(addPost);
        }

        // POST: AddPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.addpost == null)
            {
                return Problem("Entity set 'ApplicationDbContext.addpost'  is null.");
            }
            var addPost = await _context.addpost.FindAsync(id);
            if (addPost != null)
            {
                _context.addpost.Remove(addPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddPostExists(int id)
        {
          return _context.addpost.Any(e => e.Id == id);
        }
    }
}
