using AutoMapper;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class SponsorshipPaymentService : ISponsorshipPaymentService
    {
        private readonly ISponsorshipPaymentRepository _sponsorshipPaymentRepository;
        private readonly ISponsorshipPlanRepository _sponsorshipPlanRepository;
       
        private readonly IMapper _mapper;
        public SponsorshipPaymentService(ISponsorshipPaymentRepository sponsorshipPaymentRepository, IMapper mapper, ISponsorshipPlanRepository sponsorshipPlanRepository)
        {
            _mapper = mapper;
            _sponsorshipPaymentRepository = sponsorshipPaymentRepository;
            _sponsorshipPlanRepository = sponsorshipPlanRepository;
           
        }

        public async Task<ResponseDto> AddSponsorshipPayment(SponsorshipPaymentRequestDto sponsorshipPaymentRequestDto)
        {
            var responseDto = new ResponseDto();

            var sponsorshipPlan = await _sponsorshipPlanRepository.GetSponsorshipPlanByIdAsync(sponsorshipPaymentRequestDto.SponsorshipPlanId);
            if (sponsorshipPlan == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Sponsorship Plan not found";
                return responseDto;
            }
            if (sponsorshipPaymentRequestDto.Amount < 1)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Amount should be greater than 0";
                return responseDto;
            }
            var sponsorshipPayment = new SponsorshipPayment();
            _mapper.Map(sponsorshipPaymentRequestDto, sponsorshipPayment);
            if (await _sponsorshipPaymentRepository.AddSponsorshipPaymentAsync(sponsorshipPayment))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPayment added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add SponsorshipPayment";
            return responseDto;

        }

        public async Task<ResponseDto> EditSponsorshipPayment(SponsorshipPaymentUpdateRequestDto sponsorshipPaymentRequestDto)
        {
            var responseDto = new ResponseDto();

            var sponsorshipPaymentResponse = await _sponsorshipPaymentRepository.GetSponsorshipPaymentByIdAsync(sponsorshipPaymentRequestDto.SponsorshipPaymentId);
            if (sponsorshipPaymentResponse == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "SponsorshipPayment Does Not Exist";
                return responseDto;
            }
            var sponsorshipPlan = await _sponsorshipPlanRepository.GetSponsorshipPlanByIdAsync(sponsorshipPaymentRequestDto.SponsorshipPlanId);
            if (sponsorshipPlan == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Sponsorship Plan not found";
                return responseDto;
            }

            if (sponsorshipPaymentRequestDto.Amount < 1)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Amount should be greater than 0";
                return responseDto;
            }
           
            var sponsorshipPayment = new SponsorshipPayment();
            _mapper.Map(sponsorshipPaymentRequestDto, sponsorshipPayment);
            sponsorshipPayment.DateAdded = sponsorshipPaymentResponse.DateAdded;
            if (await _sponsorshipPaymentRepository.UpdateSponsorshipPaymentAsync(sponsorshipPayment))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPayment edited successfully";
                return responseDto;
            }
            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit SponsorshipPayment";
            return responseDto;

        }

        public async Task<ResponseDto> DeleteSponsorshipPayment(int sponsorshipPaymentId)
        {
            var responseDto = new ResponseDto();

            var sponsorshipPayment = await _sponsorshipPaymentRepository.GetSponsorshipPaymentByIdAsync(sponsorshipPaymentId);
            if (sponsorshipPayment == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "SponsorshipPayment Does Not Exist";
                return responseDto;
            }

            sponsorshipPayment.IsDeleted = true;
            sponsorshipPayment.DateDeleted = DateTime.Now;

            if (await _sponsorshipPaymentRepository.UpdateSponsorshipPaymentAsync(sponsorshipPayment))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPayment deleted successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to delete SponsorshipPayment";
            return responseDto;

        }

        public async Task<IEnumerable<SponsorshipPayment>> GetAllSponsorshipPayments()
        {
            var SponsorshipPayments = await _sponsorshipPaymentRepository.GetAllSponsorshipPaymentsAsync();
            return SponsorshipPayments;
        }
        public async Task<SponsorshipPayment> GetSponsorshipPaymentById(int Id)
        {
            var SponsorshipPayment = await _sponsorshipPaymentRepository.GetSponsorshipPaymentByIdAsync(Id);
            return SponsorshipPayment;
        }
        public async Task<IEnumerable<SponsorshipPayment>> GetSponsorshipPaymentBySponsorshipPlanId(int Id)
        {
            var sponsorshipPayments = await _sponsorshipPaymentRepository.GetSponsorshipPaymentBySponsorshipPlanIdAsync(Id);
            return sponsorshipPayments;
        }
    }
}
