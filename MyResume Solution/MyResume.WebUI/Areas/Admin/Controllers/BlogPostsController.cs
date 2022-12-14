using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Business.BlogPostModule;
using MyResume.Domain.Business.BrandModule;
using MyResume.Domain.Models.DataContexts;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "sa")]
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly IMediator mediator;

        public BlogPostsController(MyResumeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        //GET: Admin/BlogPosts
        public async Task<IActionResult> Index(/*BlogPostGetAllQuery query*/)
        {
            //var response = await mediator.Send(query);
            //return View(response);
            var data = await db.BlogPosts
                .Where(bg => bg.DeletedDate == null)
                .ToListAsync();

            return View(data);
        }

        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(ResumeBioGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: Admin/BlogPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostPostCommand command)
        {
            if (command.Image == null)
            {
                ModelState.AddModelError("ImagePath", "Shekil Gonderilmelidir");
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                if (response.Error == false)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(command);
        }

        // GET: Admin/BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .FirstOrDefaultAsync(bp => bp.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            var editCommand = new BlogPostPutCommand();
            editCommand.Id = blogPost.Id;
            editCommand.Title = blogPost.Title;
            editCommand.Body = blogPost.Body;
            editCommand.ImagePath = blogPost.ImagePath;

            return View(blogPost);
        }

        // POST: Admin/BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPostPutCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, BlogPostRemoveCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Publish")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishConfirmed(int id, BlogPostPublishCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeletedPosts(BlogPostGetAllDeletedQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }


        [HttpPost, ActionName("Back")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BackToPosts(int id, BlogPostRemoveBackCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeletedPostDetails(ResumeBioGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost, ActionName("Clear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearDeletedPosts(int id, BlogPostClearCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        private bool BlogPostExists(int id)
        {
            return db.BlogPosts.Any(e => e.Id == id);
        }
    }
}
