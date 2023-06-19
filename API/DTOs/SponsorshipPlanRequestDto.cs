using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class SponsorshipPlanRequestDto
    {
        [Required(ErrorMessage = "The CustomerId field is required.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "The CommunityProjectId field is required.")]
        public int CommunityProjectId { get; set; }
        [Required(ErrorMessage = "The ProductId field is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The SourceOfFunds field is required.")]
        public string SourceOfFunds { get; set; }
        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "The SponsorshipFrequency field is required.")]
        public string SponsorshipFrequency { get; set; }
       
    }
}
