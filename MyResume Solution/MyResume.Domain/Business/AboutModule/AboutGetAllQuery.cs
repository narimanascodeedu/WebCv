using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyResume.Domain.Business.AboutModule
{
    public class AboutGetAllQuery : IRequest<List<About>>
    {
        public class AboutGetAllQueryHandler : IRequestHandler<AboutGetAllQuery, List<About>>
        {
            private readonly MyResumeDbContext db;

            public AboutGetAllQueryHandler(MyResumeDbContext db)
            {
                this.db = db;
            }
            public async Task<List<About>> Handle(AboutGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Abouts
                .Include(x=>x.Services)
                .Where(bp => bp.DeletedDate == null)
                .ToListAsync(cancellationToken);

                return data;
            }
        }


    }
}
