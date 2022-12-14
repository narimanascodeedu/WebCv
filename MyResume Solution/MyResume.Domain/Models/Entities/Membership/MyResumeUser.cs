using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyResume.Domain.Models.Entities.Membership
{
    public class MyResumeUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public ICollection<BlogPostComment> BlogPostComments { get; set; }

    }
}
