using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.AppCode.Services;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "sa")]
    [Area("Admin")]
    public class ContactPostsController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly EmailService emailService;

        public ContactPostsController(MyResumeDbContext db, EmailService emailService)
        {
            this.db = db;
            this.emailService = emailService;
        }

        // GET: Admin/ContactPosts
        public async Task<IActionResult> Index()
        {
            return View(await db.ContactPosts.ToListAsync());
        }

        // GET: Admin/ContactPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPost = await db.ContactPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPost == null)
            {
                return NotFound();
            }

            return View(contactPost);
        }

        // GET: Admin/ContactPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactPost = await db.ContactPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPost == null)
            {
                return NotFound();
            }

            return View(contactPost);
        }

        // POST: Admin/ContactPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactPost = await db.ContactPosts.FindAsync(id);
            db.ContactPosts.Remove(contactPost);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reply(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            var contactPost = await db.ContactPosts.FirstOrDefaultAsync(m => m.Id == id);
            if (contactPost == null)
            {
                return NotFound();
            }

            return View(contactPost);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int id, [FromForm][Bind("Name, Email, Content, Subject, Answer, EmailSubject")] ContactPost model)
        {
            var entity = db.ContactPosts.FirstOrDefault(bp => bp.Id == id && bp.AnswerDate == null);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    error = true,
                    message = "Xeta"
                });
            }

            entity.AnswerDate = DateTime.UtcNow.AddHours(4);
            entity.Answer = model.Answer;
            entity.EmailSubject = model.EmailSubject;
            await db.SaveChangesAsync();

            await emailService.SendMailAsync(model.Email, model.EmailSubject, model.Answer);

            //return RedirectToAction(nameof(Index));
            return Json(new
            {
                error = false,
                message = "Your answer has been sended"
            });

        }

        private bool ContactPostExists(int id)
        {
            return db.ContactPosts.Any(e => e.Id == id);
        }
    }
}
