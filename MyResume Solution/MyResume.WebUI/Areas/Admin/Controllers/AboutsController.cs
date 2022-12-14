using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using MediatR;
using MyResume.Domain.Business.AboutModule;
using MyResume.Domain.Business.BlogPostModule;
using Microsoft.AspNetCore.Authorization;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "sa")]
    [Area("Admin")]
    public class AboutsController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly IMediator mediator;

        public AboutsController(MyResumeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(AboutGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        //baxariq
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateCommand command)
        {
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


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await db.Abouts.Include(x=>x.Services).FirstOrDefaultAsync(x=>x.Id == id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutEditCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);

            if (response.Error == false)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }



        private bool AboutExists(int id)
        {
            return db.Abouts.Any(e => e.Id == id);
        }
    }
}
