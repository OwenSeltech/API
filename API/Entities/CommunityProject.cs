namespace API.Entities
{
    public class CommunityProject
    {
        public int CommunityProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalFundsRequired { get; set; }
        public decimal TotalFundsRaised { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
    }
}
