using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CommunityProjectRequestDto
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The StartDate field is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The EndDate field is required.")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "The TotalFundsRequired field is required.")]
        public decimal TotalFundsRequired { get; set; }
      
    }
}
