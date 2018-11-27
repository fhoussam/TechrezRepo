using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
    public class ProductDtoPut
    {
        [Required]
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
    }
}
