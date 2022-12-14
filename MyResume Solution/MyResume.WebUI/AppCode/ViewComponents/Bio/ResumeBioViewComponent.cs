using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Business.ResumeModel.Bio;
using MyResume.Domain.Models.DataContexts;
using System.Threading.Tasks;

namespace MyResume.WebUI.AppCode.ViewComponents.Bio
{
    public class ResumeBioViewComponent : ViewComponent
    {
        private readonly MyResumeDbContext db;

        public ResumeBioViewComponent(MyResumeDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await db.ResumeBios.FirstOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return View(await Task.FromResult(data));
        }
    }
}
