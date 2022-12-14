using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class ResumeBio : BaseEntity
    {
        [Required]
        public string Text { get; set; }
    }
}
