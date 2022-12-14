using MyResume.Domain.AppCode.Infrastructure;
using MyResume.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class BlogPostComment : BaseEntity
    {
        [Required]
        public int? CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public virtual MyResumeUser CreatedByUser { get; set; }
        public string Text { get; set; }
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public int? ParentId { get; set; }
        public virtual BlogPostComment Parent { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
        public bool Approved { get; set; }
    }
}
