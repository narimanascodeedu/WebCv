using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class Portfolio : BaseEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImagePath { get; set; }
        public virtual int? PortfolioCategoryId { get; set; }
        public virtual PortfolioCategory Category { get; set; }
    }
}
