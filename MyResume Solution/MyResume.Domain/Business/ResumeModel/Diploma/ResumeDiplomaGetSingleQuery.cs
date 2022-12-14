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

namespace MyResume.Domain.Business.ResumeModel.Diploma
{
    
    public class ResumeDiplomaGetSingleQuery : IRequest<ResumeDiploma>
    {
        public int Id { get; set; }
        public class ResumeDiplomaGetSingleQueryHandler : IRequestHandler<ResumeDiplomaGetSingleQuery, ResumeDiploma>
        {
            private readonly MyResumeDbContext db;

            public ResumeDiplomaGetSingleQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ResumeDiploma> Handle(ResumeDiplomaGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.ResumeDiplomas.FirstOrDefaultAsync(re => re.Id == request.Id, cancellationToken);
                return query;
            }
        }
    }
}
