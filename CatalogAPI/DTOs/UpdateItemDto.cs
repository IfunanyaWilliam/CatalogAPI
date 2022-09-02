using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.DTOs
{
    public class UpdateItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}
