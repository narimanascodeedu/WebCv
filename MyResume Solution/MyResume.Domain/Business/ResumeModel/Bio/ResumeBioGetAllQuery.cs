using MediatR;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ResumeModel.Bio
{
    public class ResumeBioGetAllQuery : IRequest<ResumeBio>
    {
        public class ResumeBioGetAllQueryHandler : IRequestHandler<ResumeBioGetAllQuery, ResumeBio>
        {
            private readonly MyResumeDbContext db;

            public ResumeBioGetAllQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<ResumeBio> Handle(ResumeBioGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ResumeBios.FirstOrDefaultAsync(cancellationToken);
                return data;
            }
        }
    }
}
