using BlogPost.Data;
using BlogPost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace BlogPost.Areas.User.Controllers
{    
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddPosts
        public async Task<IActionResult> Index()
        {
            var curUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserPost = _context.posts.Where(p => p.AuthorId == curUserId);
            
            return View(await UserPost.ToListAsync());
        }

        // GET: AddPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.posts == null)
            {
                return NotFound();
            }

            var addPost = await _context.posts
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
        public async Task<IActionResult> Create([Bind("Id,Title,Text")] Post posts)
        {
            Post curPost = new Post();
            if (ModelState.IsValid)
            {
                var curUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                curPost.Id = posts.Id;
                curPost.AuthorId = curUserId;
                curPost.Text = posts.Text;
                curPost.Title= posts.Title;
                curPost.CreatedDate=DateTime.Now;

                _context.Add(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: AddPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.posts == null)
            {
                return NotFound();
            }

            var addPost = await _context.posts.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,CreatedDate")] Post posts)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddPostExists(posts.Id))
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
            return View(posts);
        }

        // GET: AddPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.posts == null)
            {
                return NotFound();
            }

            var addPost = await _context.posts
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
            if (_context.posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.addpost'  is null.");
            }
            var addPost = await _context.posts.FindAsync(id);
            if (addPost != null)
            {
                _context.posts.Remove(addPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddPostExists(int id)
        {
            return _context.posts.Any(e => e.Id == id);
        }
    }
}

