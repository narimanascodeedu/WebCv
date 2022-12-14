using MediatR;
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
    public class ResumeSkillViewCommand : IRequest<ResumeSkill>
    {
        public int Id { get; set; }
        public class ResumeSkillViewCommandHandler : IRequestHandler<ResumeSkillViewCommand, ResumeSkill>
        {
            private readonly MyResumeDbContext db;

            public ResumeSkillViewCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ResumeSkill> Handle(ResumeSkillViewCommand request, CancellationToken cancellationToken)
            {
                string tag = "tag";
                var data = db.ResumeSkills.FirstOrDefault(m => m.Id == request.Id);

                if (data == null)
                {
                    return null;
                }
                if(data.View != null)
                {
                    data.View = null;
                }
                else if(data.View == null)
                {
                data.View = tag ;
                }
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
