using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SponsorshipPlanController : BaseApiController
    {
        private readonly ISponsorshipPlanService _sponsorshipPlanService;
        public SponsorshipPlanController(ISponsorshipPlanService sponsorshipPlanService)
        {
            _sponsorshipPlanService = sponsorshipPlanService;
        }
        [HttpPost("addSponsorshipPlan")]
        public async Task<ActionResult<ResponseDto>> AddSponsorshipPlan(SponsorshipPlanRequestDto requestDto)
        {
            var response = await _sponsorshipPlanService.AddSponsorshipPlan(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpPost("updateSponsorshipPlan")]
        public async Task<ActionResult<ResponseDto>> UpdateSponsorshipPlan(SponsorshipPlanUpdateRequestDto requestDto)
        {
            var response = await _sponsorshipPlanService.EditSponsorshipPlan(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpPost("deleteSponsorshipPlan/{SponsorshipPlanId}")]
        public async Task<ActionResult<ResponseDto>> DeleteSponsorshipPlan(int SponsorshipPlanId)
        {
            var response = await _sponsorshipPlanService.DeleteSponsorshipPlan(SponsorshipPlanId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<SponsorshipPlan>> GetAllSponsorshipPlans()
        {
            var response = await _sponsorshipPlanService.GetAllSponsorshipPlans();
            return Ok(response);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<SponsorshipPlan>> GetSponsorshipPlanByCustomer(int customerId)
        {
            var response = await _sponsorshipPlanService.GetSponsorshipPlanByCustId(customerId);
            return Ok(response);
        }
        [HttpGet("{SponsorshipPlanId}")]
        public async Task<ActionResult<SponsorshipPlan>> GetSponsorshipPlan(int SponsorshipPlanId)
        {
            var response = await _sponsorshipPlanService.GetSponsorshipPlanById(SponsorshipPlanId);
            return Ok(response);
        }

    }
}
