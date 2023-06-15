namespace API.Entities
{
    public class SponsorshipPlan
    {
        public int PlanId { get; set; }
        public int CustomerId { get; set; }
        public int ProjectId { get; set; }
        public int ProductId { get; set; }
        public string SourceOfFunds { get; set; }
        public decimal Amount { get; set; }
        public SponsorshipFrequency Frequency { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
    }
    public enum SponsorshipFrequency
    {
        OnceOff,
        Weekly,
        Monthly
    }
}
