using MyResume.Domain.AppCode.Services;
using MediatR;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ResumeModel.Bio
{
    public class ResumeBioPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public class ResumeBioPutCommandHandler : IRequestHandler<ResumeBioPutCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;

            public ResumeBioPutCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ResumeBioPutCommand request, CancellationToken cancellationToken)
            {
                var entity = db.ResumeBios.FirstOrDefault(bg => bg.Id == request.Id);

                if (entity == null)
                {
                    return null;
                }
                entity.Text = request.Text;
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
