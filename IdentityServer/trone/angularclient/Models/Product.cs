using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class Product
    {
        [Key]
        public int Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<Category> Category { get; set; }
    }
}
