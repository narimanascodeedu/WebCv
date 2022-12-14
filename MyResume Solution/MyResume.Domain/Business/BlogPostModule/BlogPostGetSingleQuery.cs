using MediatR;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.BlogPostModule
{
    public class ResumeBioGetSingleQuery : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public class BlogPostGetSingleQueryHandler : IRequestHandler<ResumeBioGetSingleQuery, BlogPost>
        {
            private readonly MyResumeDbContext db;

            public BlogPostGetSingleQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(ResumeBioGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = db.BlogPosts
                    .Include(bp => bp.Comments)
                    .ThenInclude(c=>c.CreatedByUser)

                    .Include(bp => bp.Comments)
                    .ThenInclude(c => c.Comments)

                    .AsQueryable();
                if (string.IsNullOrWhiteSpace(request.Slug))
                {
                    return await query.FirstOrDefaultAsync(bp => bp.Id == request.Id, cancellationToken);

                }
                return await query.FirstOrDefaultAsync(bp => bp.Slug.Equals(request.Slug), cancellationToken);
            }
        }
    }
}
