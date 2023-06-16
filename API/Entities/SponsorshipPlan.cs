namespace API.Entities
{
    public class SponsorshipPlan
    {
        public int SponsorshipPlanId { get; set; }
        public int CustomerId { get; set; }
        public int CommunityProjectId { get; set; }
        public int ProductId { get; set; }
        public string SourceOfFunds { get; set; }
        public decimal Amount { get; set; }
        public string SponsorshipFrequency { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
        public CommunityProject CommunityProject { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }

    }
    public enum SponsorshipFrequency
    {
        OnceOff,
        Weekly,
        Monthly
    }
}
