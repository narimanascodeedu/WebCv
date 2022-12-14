using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostCommentsController : Controller
    {
        private readonly MyResumeDbContext _context;

        public BlogPostCommentsController(MyResumeDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogPostComments
        public async Task<IActionResult> Index()
        {
            var myResumeDbContext = _context.BlogPostComments.Include(b => b.BlogPost).Include(b => b.CreatedByUser).Include(b => b.Parent);
            return View(await myResumeDbContext.ToListAsync());
        }

        // GET: Admin/BlogPostComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComments
                .Include(b => b.BlogPost)
                .Include(b => b.CreatedByUser)
                .Include(b => b.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            return View(blogPostComment);
        }

        // GET: Admin/BlogPostComments/Create
        public IActionResult Create()
        {
            ViewData["BlogPostId"] = new SelectList(_context.BlogPosts, "Id", "Body");
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["ParentId"] = new SelectList(_context.BlogPostComments, "Id", "Id");
            return View();
        }

        // POST: Admin/BlogPostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedByUserId,Text,BlogPostId,ParentId,Approved,Id,CreatedDate,DeletedDate")] BlogPostComment blogPostComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPostComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPosts, "Id", "Body", blogPostComment.BlogPostId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", blogPostComment.CreatedByUserId);
            ViewData["ParentId"] = new SelectList(_context.BlogPostComments, "Id", "Id", blogPostComment.ParentId);
            return View(blogPostComment);
        }

        // GET: Admin/BlogPostComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComments.FindAsync(id);
            if (blogPostComment == null)
            {
                return NotFound();
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPosts, "Id", "Body", blogPostComment.BlogPostId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", blogPostComment.CreatedByUserId);
            ViewData["ParentId"] = new SelectList(_context.BlogPostComments, "Id", "Id", blogPostComment.ParentId);
            return View(blogPostComment);
        }

        // POST: Admin/BlogPostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatedByUserId,Text,BlogPostId,ParentId,Approved,Id,CreatedDate,DeletedDate")] BlogPostComment blogPostComment)
        {
            if (id != blogPostComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPostComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostCommentExists(blogPostComment.Id))
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
            ViewData["BlogPostId"] = new SelectList(_context.BlogPosts, "Id", "Body", blogPostComment.BlogPostId);
            ViewData["CreatedByUserId"] = new SelectList(_context.Users, "Id", "Name", blogPostComment.CreatedByUserId);
            ViewData["ParentId"] = new SelectList(_context.BlogPostComments, "Id", "Id", blogPostComment.ParentId);
            return View(blogPostComment);
        }

        // GET: Admin/BlogPostComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComments
                .Include(b => b.BlogPost)
                .Include(b => b.CreatedByUser)
                .Include(b => b.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            return View(blogPostComment);
        }

        // POST: Admin/BlogPostComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPostComment = await _context.BlogPostComments.FindAsync(id);
            _context.BlogPostComments.Remove(blogPostComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostCommentExists(int id)
        {
            return _context.BlogPostComments.Any(e => e.Id == id);
        }
    }
}
