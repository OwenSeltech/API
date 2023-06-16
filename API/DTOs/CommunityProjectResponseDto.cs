namespace API.DTOs
{
    public class CommunityProjectResponseDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalFundsRequired { get; set; }
        public decimal TotalFundsRaised { get; set; }
    }
}
