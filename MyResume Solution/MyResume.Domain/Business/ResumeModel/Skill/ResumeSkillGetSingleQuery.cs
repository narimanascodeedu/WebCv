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

namespace MyResume.Domain.Business.ResumeModel.Skill
{
  
    public class ResumeSkillGetSingleQuery : IRequest<ResumeSkill>
    {
        public int Id { get; set; }

        public class ResumeSkillGetSingleQueryHandler : IRequestHandler<ResumeSkillGetSingleQuery, ResumeSkill>
        {
            private readonly MyResumeDbContext db;

            public ResumeSkillGetSingleQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ResumeSkill> Handle(ResumeSkillGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.ResumeSkills.Include(r => r.ResumeCategory).FirstOrDefaultAsync(re => re.Id == request.Id, cancellationToken);
                return query;
            }
        }
    }
}
