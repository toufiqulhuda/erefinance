using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erefinance.Models
{
    public class DistrictModel
    {
        [Key]
        public int Id { get; set; }
        public int DistrictCode { get; set; }
        public String DistrictName { get; set; }
    }
}
