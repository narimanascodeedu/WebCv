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

namespace MyResume.Domain.Business.ResumeModel.Diploma
{
    public class ResumeDiplomaPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Details { get; set; }
        public int YearObtention { get; set; }
        public string UniversityName { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public class ResumeDiplomaPutCommandHandler : IRequestHandler<ResumeDiplomaPutCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;
            private readonly IHostEnvironment env;

            public ResumeDiplomaPutCommandHandler(MyResumeDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<JsonResponse> Handle(ResumeDiplomaPutCommand request, CancellationToken cancellationToken)
            {
                var entity = db.ResumeDiplomas.FirstOrDefault(bg => bg.Id == request.Id && bg.DeletedDate == null);


                if (entity == null)
                {
                    return null;
                }

                entity.Degree = request.Degree;
                entity.StartYear = request.StartYear;
                entity.EndYear = request.EndYear;
                entity.UniversityName = request.UniversityName;
                entity.YearObtention = request.YearObtention;
                entity.Details = request.Details;

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName);
                request.ImagePath = $"diploma-{Guid.NewGuid().ToString().ToLower()}{extension}";

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
