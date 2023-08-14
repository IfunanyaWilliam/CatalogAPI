using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    public class UpdateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}
