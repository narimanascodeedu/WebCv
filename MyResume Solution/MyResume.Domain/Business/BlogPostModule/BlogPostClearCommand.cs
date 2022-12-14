using MyResume.Domain.Business.BlogPostModule;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.BrandModule
{
    public class BlogPostClearCommand : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogPostClearCommandHandler : IRequestHandler<BlogPostClearCommand, BlogPost>
        {
            private readonly MyResumeDbContext db;

            public BlogPostClearCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(BlogPostClearCommand request, CancellationToken cancellationToken)
            {
                var data = db.BlogPosts.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate != null);

                if (data == null)
                {
                    return null;
                }
                db.BlogPosts.Remove(data);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}

