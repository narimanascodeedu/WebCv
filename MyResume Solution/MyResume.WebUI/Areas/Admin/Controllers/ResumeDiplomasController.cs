using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Business.ResumeModel.Diploma;
using MyResume.Domain.Business.ResumeModel.Experience;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyResume.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "sa")]
    [Area("Admin")]
    public class ResumeDiplomasController : Controller
    {
        private readonly MyResumeDbContext db;
        private readonly IMediator mediator;

        public ResumeDiplomasController(MyResumeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/ResumeDiplomas
        public async Task<IActionResult> Index(ResumeDiplomaGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/ResumeDiplomas/Details/5
        public async Task<IActionResult> Details(ResumeDiplomaGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: Admin/ResumeDiplomas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ResumeDiplomas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResumeDiplomaPostCommand command)
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

        // GET: Admin/ResumeDiplomas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resumeDiploma = await db.ResumeDiplomas.FindAsync(id);
            if (resumeDiploma == null)
            {
                return NotFound();
            }
            var editCommand = new ResumeDiplomaPutCommand();
            editCommand.Id = resumeDiploma.Id;
            editCommand.Degree = resumeDiploma.Degree;
            editCommand.StartYear = resumeDiploma.StartYear;
            editCommand.EndYear = resumeDiploma.EndYear;
            editCommand.UniversityName = resumeDiploma.UniversityName;
            editCommand.YearObtention = resumeDiploma.YearObtention;
            editCommand.Details = resumeDiploma.Details;
            editCommand.ImagePath = resumeDiploma.ImagePath;
            return View(resumeDiploma);
        }

        // POST: Admin/ResumeDiplomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResumeDiplomaPutCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            if(response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // POST: Admin/ResumeDiplomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ResumeDiplomaRemoveCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private bool ResumeDiplomaExists(int id)
        {
            return db.ResumeDiplomas.Any(e => e.Id == id);
        }
    }
}
