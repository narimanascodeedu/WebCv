using MediatR;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;
using MyResume.Domain.AppCode.Infrastructure;
using System.Linq;

namespace MyResume.Domain.Business.AboutModule
{
    public class AboutEditCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public int Experience { get; set; }
        public string Degree { get; set; }
        public string CareerLevel { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GitHubLink { get; set; }
        public string YoutubeLink { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public Services[] Items { get; set; }



        public class AboutEditCommandHandler : IRequestHandler<AboutEditCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;

            public AboutEditCommandHandler(MyResumeDbContext db)
            {
                this.db = db;
            }

            public async Task<JsonResponse> Handle(AboutEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Abouts
                    .Include(m=>m.Services)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);

                if (data == null)
                {
                    return null;
                }

                data.Name = request.Name;
                data.Age = request.Age;
                data.Location = request.Location;
                data.Experience = request.Experience;
                data.Degree = request.Degree;
                data.CareerLevel = request.CareerLevel;
                data.Phone = request.Phone;
                data.Fax = request.Fax;
                data.Email = request.Email;
                data.Website = request.Website;
                data.FacebookLink = request.FacebookLink;
                data.TwitterLink = request.TwitterLink;
                data.LinkedinLink = request.LinkedinLink;
                data.GitHubLink = request.GitHubLink;
                data.YoutubeLink = request.YoutubeLink;
                data.ShortDescription = request.ShortDescription;
                data.LongDescription = request.LongDescription;

                if (request.Items != null)
                {
                    foreach (var item in request.Items)
                    {
                        var service = data.Services.FirstOrDefault(m => m.Id == item.Id);
                        service.Text = item.Text;
                        service.Description = item.Description;
                        service.IconKey = item.IconKey;
                    }
                }



                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }

        }
    }
}