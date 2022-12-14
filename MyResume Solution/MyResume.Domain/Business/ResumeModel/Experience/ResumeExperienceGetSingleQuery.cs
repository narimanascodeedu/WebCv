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

namespace MyResume.Domain.Business.ResumeModel.Experience
{
    
    public class ResumeExperienceGetSingleQuery : IRequest<ResumeExperience>
    {
        public int Id { get; set; }
        public class ResumeExperienceGetSingleQueryHandler : IRequestHandler<ResumeExperienceGetSingleQuery, ResumeExperience>
        {
            private readonly MyResumeDbContext db;

            public ResumeExperienceGetSingleQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ResumeExperience> Handle(ResumeExperienceGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.ResumeExperiences.FirstOrDefaultAsync(re => re.Id == request.Id, cancellationToken);
                return query;
            }
        }
    }
}
