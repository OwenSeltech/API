using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class SponsorshipPaymentUpdateRequestDto
    {
        public int SponsorshipPaymentId { get; set; }
        [Required(ErrorMessage = "The SponsorshipPlanId  field is required.")]
        public int SponsorshipPlanId { get; set; }
        [Required(ErrorMessage = "The Amount  field is required.")]
        public decimal Amount { get; set; }

    }
}
