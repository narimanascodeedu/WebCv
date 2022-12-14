using MediatR;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.ContactPostModule
{
    public class ContactPostGetAllQuery : IRequest<List<ContactPost>>
    {
        public class ContactPostGetAllHandler : IRequestHandler<ContactPostGetAllQuery, List<ContactPost>>
        {
            private readonly MyResumeDbContext db;

            public ContactPostGetAllHandler(MyResumeDbContext db)
            {
                this.db = db;
            }


            public async Task<List<ContactPost>> Handle(ContactPostGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.ContactPosts.Where(bp => bp.DeletedDate == null)
                                             .ToListAsync(cancellationToken);
                return query;
            }
        }
    }
}
