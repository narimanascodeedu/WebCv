using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MyResume.Domain.AppCode.Extensions;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ResumeModel.Bio
{
    public class ResumeBioPostCommand : IRequest<JsonResponse>
    {
        public string Text { get; set; }

        public class ResumeBioPostCommandHandler : IRequestHandler<ResumeBioPostCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;
            public ResumeBioPostCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ResumeBioPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new ResumeBio();

                entity.Text = request.Text;

                await db.ResumeBios.AddAsync(entity, cancellationToken);
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
