using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class About : BaseEntity
    {
        [Required(ErrorMessage = "bos buraxila bilmez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public int Age { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public string Location { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public int Experience { get; set; }

        public string Degree { get; set; }
        public string CareerLevel { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public string Phone { get; set; }
        public string Fax { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public string Email { get; set; }
        public string Website { get; set; }

        [Required(ErrorMessage = "bos buraxila bilmez")]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GitHubLink { get; set; }
        public string YoutubeLink { get; set; }

        public ICollection<Services> Services { get; set; }

    }
}
