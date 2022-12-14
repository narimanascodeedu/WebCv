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
    public class ResumeDiplomaGetAllQuery : IRequest<List<ResumeDiploma>>
    {
        public class ResumeDiplomaGetAllQueryHandler : IRequestHandler<ResumeDiplomaGetAllQuery, List<ResumeDiploma>>
        {
            private readonly MyResumeDbContext db;

            public ResumeDiplomaGetAllQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<List<ResumeDiploma>> Handle(ResumeDiplomaGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ResumeDiplomas.Where(re => re.DeletedDate == null).ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
