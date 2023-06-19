using API.Data.Repositories;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ISponsorshipPlanService
    {
        Task<ResponseDto> AddSponsorshipPlan(SponsorshipPlanRequestDto sponsorshipPlanRequestDto);
        Task<ResponseDto> EditSponsorshipPlan(SponsorshipPlanUpdateRequestDto sponsorshipPlanRequestDto);
        Task<ResponseDto> DeleteSponsorshipPlan(int sponsorshipPlanId);
        Task<IEnumerable<SponsorshipPlan>> GetAllSponsorshipPlans();
        Task<SponsorshipPlan> GetSponsorshipPlanById(int Id);
        Task<IEnumerable<SponsorshipPlan>> GetSponsorshipPlanByCustId(int Id);


    }
}
