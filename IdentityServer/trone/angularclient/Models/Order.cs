using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class Order
    {
        [Key]
        public int Code { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string TechRezUserId { get; set; }
        public int ProductId { get; set; }
    }
}
