using MyResume.Domain.Models.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using MyResume.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyResume.Domain.Models.DataContexts
{
    public class MyResumeDbContext : IdentityDbContext<MyResumeUser, MyResumeRole, int, MyResumeUserClaim,
        MyResumeUserRole,
        MyResumeUserLogin, MyResumeRoleClaim, MyResumeUserToken>
    {
        public MyResumeDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MyResumeUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });
            builder.Entity<MyResumeRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });
            builder.Entity<MyResumeUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");

            });
            builder.Entity<MyResumeUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });
            builder.Entity<MyResumeRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");

            });
            builder.Entity<MyResumeUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });
            builder.Entity<MyResumeUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });
        }

        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ResumeBio> ResumeBios { get; set; }
        public DbSet<ResumeCategory> ResumeCategorys { get; set; }
        public DbSet<ResumeSkill> ResumeSkills { get; set; }
        public DbSet<ResumeExperience> ResumeExperiences { get; set; }
        public DbSet<ResumeDiploma> ResumeDiplomas { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<Services> Services{ get; set; }
        public DbSet<Icons> Icons { get; set; }



    }
}
