using MyResume.Domain.Models.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPdf.WebUI.ViewComponents.PdfExperience
{
    public class PdfExperienceViewComponent : ViewComponent
    {
        private readonly MyResumeDbContext db;

        public PdfExperienceViewComponent(MyResumeDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await db.ResumeExperiences.Where(re=>re.DeletedDate==null).ToListAsync();

            if (data == null)
            {
                return null;
            }

            return View(await Task.FromResult(data));
        }
    }
}
