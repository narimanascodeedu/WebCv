using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MyResume.Domain.AppCode.Extensions;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ResumeModel.Experience
{
    public class ResumeExperiencePutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public class ResumeExperiencePutCommandHandler : IRequestHandler<ResumeExperiencePutCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;
            private readonly IHostEnvironment env;

            public ResumeExperiencePutCommandHandler(MyResumeDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<JsonResponse> Handle(ResumeExperiencePutCommand request, CancellationToken cancellationToken)
            {
                var entity = db.ResumeExperiences.FirstOrDefault(bg => bg.Id == request.Id && bg.DeletedDate == null);


                if (entity == null)
                {
                    return null;
                }

                entity.Title = request.Title;
                entity.StartYear = request.StartYear;
                entity.EndYear = request.EndYear;
                entity.CompanyName = request.CompanyName;
                entity.Location = request.Location;
                entity.Details = request.Details;

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName); //.jpg-ni goturur
                request.ImagePath = $"experience-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(entity.ImagePath);


                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{entity.ImagePath}"));

                entity.ImagePath = request.ImagePath;

            end:

                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
