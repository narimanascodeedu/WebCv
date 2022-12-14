using MediatR;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ResumeModel.Skill
{
    public class ResumeCategoryPostCommand : IRequest<JsonResponse>
    {
        public string Name { get; set; }
        public class ResumeCategoryPostCommandHandler : IRequestHandler<ResumeCategoryPostCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;

            public ResumeCategoryPostCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ResumeCategoryPostCommand request, CancellationToken cancellationToken)
            {
                var data = new ResumeCategory();
                data.Name = request.Name;
                await db.ResumeCategorys.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                }; ;
            }
        }
    }
}
