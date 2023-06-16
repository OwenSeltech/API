using API.Entities;

namespace API.Interfaces
{
    public interface ISponsorshipPlanRepository
    {
        Task<bool> AddSponsorshipPlanAsync(SponsorshipPlan sponsorshipPlan);
        Task<bool> UpdateSponsorshipPlanAsync(SponsorshipPlan sponsorshipPlan);
        Task<IEnumerable<SponsorshipPlan>> GetAllSponsorshipPlansAsync();
        Task<SponsorshipPlan> GetSponsorshipPlanByIdAsync(int id);
        public bool SponsorshipPlanExists(int projectId);
    }
}
