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
    public class ResumeSkillGetAllQuery : IRequest<List<ResumeSkill>>
    {
        public class ResumeSkillGetAllQueryHandler : IRequestHandler<ResumeSkillGetAllQuery, List<ResumeSkill>>
        {
            private readonly MyResumeDbContext db;

            public ResumeSkillGetAllQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<List<ResumeSkill>> Handle(ResumeSkillGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ResumeSkills.Where(re => re.DeletedDate == null).Include(re=>re.ResumeCategory).ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
