using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ProductUpdateRequestDto
    {
        [Required(ErrorMessage = "The Customer Id field is required.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "The Product Type field is required.")]
        public string ProductType  { get; set; }
        [Required(ErrorMessage = "The Product Id field is required.")]
        public int ProductId { get; set; }
    }
}
