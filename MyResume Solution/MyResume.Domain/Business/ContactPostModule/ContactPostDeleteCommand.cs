using MediatR;
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
    public class ContactPostDeleteCommand : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostDeleteCommandHandler : IRequestHandler<ContactPostDeleteCommand, ContactPost>
        {
            private readonly MyResumeDbContext db;

            public ContactPostDeleteCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ContactPost> Handle(ContactPostDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ContactPosts.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate == null);

                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
