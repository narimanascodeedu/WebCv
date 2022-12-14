using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class Services : BaseEntity
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string IconKey { get; set; }
        public int? AboutId { get; set; }
    }
}
