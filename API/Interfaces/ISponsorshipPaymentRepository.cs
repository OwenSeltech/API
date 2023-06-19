using API.Entities;

namespace API.Interfaces
{
    public interface ISponsorshipPaymentRepository
    {
        Task<bool> AddSponsorshipPaymentAsync(SponsorshipPayment sponsorshipPayment);
        Task<bool> UpdateSponsorshipPaymentAsync(SponsorshipPayment sponsorshipPayment);
        Task<IEnumerable<SponsorshipPayment>> GetAllSponsorshipPaymentsAsync();
        Task<SponsorshipPayment> GetSponsorshipPaymentByIdAsync(int id);
        Task<IEnumerable<SponsorshipPayment>> GetSponsorshipPaymentBySponsorshipPlanIdAsync(int id);
    }
}
