using MyResume.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.Entities
{
    public class PortfolioCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
    }
}
