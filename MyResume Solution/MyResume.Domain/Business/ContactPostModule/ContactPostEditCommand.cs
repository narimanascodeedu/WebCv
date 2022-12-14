using MyResume.Domain.AppCode.Services;
using MediatR;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ContactPostModule
{
    public class ContactPostEditCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Email { get; set; }

        public class ContactPostEditCommandHandler : IRequestHandler<ContactPostEditCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;
            private readonly EmailService emailService;

            public ContactPostEditCommandHandler(MyResumeDbContext db, EmailService emailService)
            {
                this.db = db;
                this.emailService = emailService;
            }
            public async Task<JsonResponse> Handle(ContactPostEditCommand request, CancellationToken cancellationToken)
            {
                var entity = db.ContactPosts.FirstOrDefault(bg => bg.Id == request.Id && /*bg.AnswerDate == null*/ bg.DeletedDate == null);

                if (entity == null)
                {
                    return null;
                }
                entity.Answer = request.Answer;
                entity.Email = request.Email;
                entity.AnswerDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
               // await emailService.SendMailAsync(request.Email, "Answer by Admin", request.Answer);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };

            }
        }
    }
}
