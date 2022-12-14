using Microsoft.EntityFrameworkCore;
using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class BlogPost : BaseEntity, IPageable
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Slug { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }

    }
}
