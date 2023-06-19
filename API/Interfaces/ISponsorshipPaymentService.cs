using API.Data.Repositories;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ISponsorshipPaymentService
    {
       Task<ResponseDto> AddSponsorshipPayment(SponsorshipPaymentRequestDto sponsorshipPaymentRequestDto);
       Task<ResponseDto> EditSponsorshipPayment(SponsorshipPaymentUpdateRequestDto sponsorshipPaymentRequestDto);
       Task<ResponseDto> DeleteSponsorshipPayment(int sponsorshipPaymentId);
       Task<IEnumerable<SponsorshipPayment>> GetAllSponsorshipPayments();
       Task<SponsorshipPayment> GetSponsorshipPaymentById(int Id);
       Task<IEnumerable<SponsorshipPayment>> GetSponsorshipPaymentBySponsorshipPlanId(int Id);
       
    }
}
