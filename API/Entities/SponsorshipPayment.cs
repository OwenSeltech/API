namespace API.Entities
{
    public class SponsorshipPayment
    {
        public int PaymentId { get; set; }
        public int PlanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
    }
}
