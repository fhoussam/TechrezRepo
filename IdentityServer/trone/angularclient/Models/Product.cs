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
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string PhotoUrl { get; set; }
        public List<Order> Orders { get; set; }
    }
}
