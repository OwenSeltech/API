using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SponsorshipPaymentController : BaseApiController
    {
        private readonly ISponsorshipPaymentService _sponsorshipPaymentService;
        public SponsorshipPaymentController(ISponsorshipPaymentService sponsorshipPaymentService)
        {
            _sponsorshipPaymentService = sponsorshipPaymentService;
        }
        [HttpPost("addSponsorshipPayment")]
        public async Task<ActionResult<ResponseDto>> AddSponsorshipPayment(SponsorshipPaymentRequestDto requestDto)
        {
            var response = await _sponsorshipPaymentService.AddSponsorshipPayment(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpPost("updateSponsorshipPayment")]
        public async Task<ActionResult<ResponseDto>> UpdateSponsorshipPayment(SponsorshipPaymentUpdateRequestDto requestDto)
        {
            var response = await _sponsorshipPaymentService.EditSponsorshipPayment(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpPost("deleteSponsorshipPayment/{SponsorshipPaymentId}")]
        public async Task<ActionResult<ResponseDto>> DeleteSponsorshipPayment(int SponsorshipPaymentId)
        {
            var response = await _sponsorshipPaymentService.DeleteSponsorshipPayment(SponsorshipPaymentId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<SponsorshipPayment>> GetAllSponsorshipPayments()
        {
            var response = await _sponsorshipPaymentService.GetAllSponsorshipPayments();
            return Ok(response);
        }

        [HttpGet("sponsorshipPlan/{planId}")]
        public async Task<ActionResult<SponsorshipPayment>> GetSponsorshipPaymentByPlan(int planId)
        {
            var response = await _sponsorshipPaymentService.GetSponsorshipPaymentBySponsorshipPlanId(planId);
            return Ok(response);
        }
        [HttpGet("{SponsorshipPaymentId}")]
        public async Task<ActionResult<SponsorshipPayment>> GetSponsorshipPayment(int SponsorshipPaymentId)
        {
            var response = await _sponsorshipPaymentService.GetSponsorshipPaymentById(SponsorshipPaymentId);
            return Ok(response);
        }

    }
}
