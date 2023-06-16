namespace API.Entities
{
    public class SponsorshipPayment
    {
        public int SponsorshipPaymentId { get; set; }
        public int SponsorshipPlanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
        public SponsorshipPlan SponsorshipPlan { get; set; }
    }
}
