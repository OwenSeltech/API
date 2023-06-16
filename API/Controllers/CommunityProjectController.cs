using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommunityProjectController : BaseApiController
    {
        private readonly ICommunityProjectService _communityProjectService;
        public CommunityProjectController(ICommunityProjectService communityProjectService)
        {
            _communityProjectService = communityProjectService;
        }
        [HttpPost("addCommunityProject")]
        public async Task<ActionResult<ResponseDto>> AddCommunityProject(CommunityProjectRequestDto requestDto)
        {
            var response = await _communityProjectService.AddCommunityProject(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpPost("updateCommunityProject")]
        public async Task<ActionResult<ResponseDto>> UpdateCommunityProject(CommunityProjectUpdateRequestDto requestDto)
        {
            var response = await _communityProjectService.EditCommunityProject(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpPost("deleteCommunityProject/{CommunityProjectId}")]
        public async Task<ActionResult<ResponseDto>> DeleteCommunityProject(int CommunityProjectId)
        {
            var response = await _communityProjectService.DeleteCommunityProject(CommunityProjectId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<CommunityProjectResponseDto>> GetAllCommunityProjects()
        {
            var response = await _communityProjectService.GetAllCommunityProjects();
            return Ok(response);
        }

        [HttpGet("{CommunityProjectId}")]
        public async Task<ActionResult<CommunityProject>> GetCommunityProject(int CommunityProjectId)
        {
            var response = await _communityProjectService.GetCommunityProjectById(CommunityProjectId);
            return Ok(response);
        }

    }
}
