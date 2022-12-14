using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class ContactPost : BaseEntity
    {
        [Required(ErrorMessage = "Bosh buraxma")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bosh buraxma")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bosh buraxma")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Bosh buraxma")]
        public string Content { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string EmailSubject { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
