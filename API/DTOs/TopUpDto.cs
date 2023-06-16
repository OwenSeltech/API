using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TopUpDto
    {
        [Required(ErrorMessage = "The Customer Id field is required.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "The Balance field is required.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "The Product Id field is required.")]
        public int ProductId { get; set; }
    }
}
