using MyResume.Domain.AppCode.Extensions;
using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.DataContexts;
using MyResume.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace MyResume.Domain.Business.BlogPostModule
{
    public class AboutCreateCommand : IRequest<JsonResponse>
    {
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

        public class AboutCreateCommandHandler : IRequestHandler<AboutCreateCommand, JsonResponse>
        {
            private readonly MyResumeDbContext db;
            private readonly IHostEnvironment env;

            public AboutCreateCommandHandler(MyResumeDbContext db, IHostEnvironment env)
            {
                this.db = db;
            }

            
            public async Task<JsonResponse> Handle(AboutCreateCommand request, CancellationToken cancellationToken)
            {
                var entity = new About();

                entity.Name = request.Name;
                entity.Age = request.Age;
                entity.Location = request.Location;
                entity.Experience = request.Experience;
                entity.Degree = request.Degree;
                entity.CareerLevel = request.CareerLevel;
                entity.Phone = request.Phone;
                entity.Fax = request.Fax;
                entity.Email = request.Email;
                entity.Website = request.Website;
                entity.FacebookLink = request.FacebookLink;
                entity.TwitterLink = request.TwitterLink;
                entity.LinkedinLink = request.LinkedinLink;
                entity.GitHubLink = request.GitHubLink;
                entity.YoutubeLink = request.YoutubeLink;
                entity.ShortDescription = request.ShortDescription;
                entity.LongDescription = request.LongDescription;

                await db.Abouts.AddAsync(entity, cancellationToken);
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
