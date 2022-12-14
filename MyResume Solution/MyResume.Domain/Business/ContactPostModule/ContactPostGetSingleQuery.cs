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

namespace MyResume.Domain.Business.ContactPostModule
{
    public class ContactPostGetSingleQuery : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public class ContactPostGetSingleQueryHandler : IRequestHandler<ContactPostGetSingleQuery, ContactPost>
        {
            private readonly MyResumeDbContext db;

            public ContactPostGetSingleQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<ContactPost> Handle(ContactPostGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.ContactPosts.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return query;
            }
        }
    }
}
