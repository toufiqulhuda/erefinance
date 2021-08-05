using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erefinance.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public String CategoryName { get; set; }
    }
}
