using MediatR;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.BlogPostModule
{
    public class BlogPostGetAllDeletedQuery : IRequest<List<BlogPost>>
    {
        public class BlogPostGetAllDeletedHandler : IRequestHandler<BlogPostGetAllDeletedQuery, List<BlogPost>>
        {
            private readonly MyResumeDbContext db;

            public BlogPostGetAllDeletedHandler(MyResumeDbContext db)
            {
                this.db = db;
            }


            public async Task<List<BlogPost>> Handle(BlogPostGetAllDeletedQuery request, CancellationToken cancellationToken)
            {
                var query = await db.BlogPosts.Where(bp => bp.DeletedDate != null) //&& bp.PublishedDate != null)
                                             .ToListAsync(cancellationToken);
                return query;
            }
        }
    }
}
