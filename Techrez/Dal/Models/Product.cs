using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public float Price { get; set; }

        [Required]
        public virtual int CategoryID { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
