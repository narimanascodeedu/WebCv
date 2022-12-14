using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Business.ResumeModel.Bio;
using MyResume.Domain.Business.ResumeModule.Bio;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "sa")]
    [Area("Admin")]
    public class ResumeBiosController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly IMediator mediator;

        public ResumeBiosController(MyResumeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/ResumeBios
        public async Task<IActionResult> Index(ResumeBioGetAllQuery query)
        {
            var response = await mediator.Send(query);
            //if (response == null)
            //{
            //    return NotFound();
            //}
            return View(response);
        }

        // GET: Admin/ResumeBios/Details/5
        public async Task<IActionResult> Details(int? id, ResumeBioGetSingleQuery query)
        {
            if (id != query.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        // GET: Admin/ResumeBios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ResumeBios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResumeBioPostCommand command)
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

        // GET: Admin/ResumeBios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resumeBio = await db.ResumeBios.FindAsync(id);
            if (resumeBio == null)
            {
                return NotFound();
            }
            var editCommand = new ResumeBioPutCommand();
            editCommand.Id = resumeBio.Id;
            editCommand.Text = resumeBio.Text;
            return View(resumeBio);
        }

        // POST: Admin/ResumeBios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResumeBioPutCommand command)
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

   

        private bool ResumeBioExists(int id)
        {
            return db.ResumeBios.Any(e => e.Id == id);
        }
    }
}
