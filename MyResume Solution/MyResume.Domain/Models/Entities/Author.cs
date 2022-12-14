using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Role { get; set; }
        public virtual ICollection<ContactPost> ContactPosts { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
