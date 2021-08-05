using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erefinance.Models
{
    public class FundModel
    {
        [Key]
        public int Id { get; set; }
        public int FundCode { get; set; }
        public String FundName { get; set; }
    }
}
