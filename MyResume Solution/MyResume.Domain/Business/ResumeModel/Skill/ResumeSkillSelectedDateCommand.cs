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
    public class ResumeSkillSelectedDateCommand : IRequest<ResumeSkill>
    {
        public int Id { get; set; }
        public class ResumeSkillSelectedDateCommandHandler : IRequestHandler<ResumeSkillSelectedDateCommand, ResumeSkill>
        {
            private readonly MyResumeDbContext db;

            public ResumeSkillSelectedDateCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ResumeSkill> Handle(ResumeSkillSelectedDateCommand request, CancellationToken cancellationToken)
            {
                var data = db.ResumeSkills.FirstOrDefault(m => m.Id == request.Id);

                if (data == null)
                {
                    return null;
                }
                if(data.SelectedDate != null)
                {
                    data.SelectedDate = null;
                }
                else if(data.SelectedDate == null)
                {
                data.SelectedDate = DateTime.UtcNow.AddHours(4);
                }
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
