using AutoMapper;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class CommunityProjectService : ICommunityProjectService
    {
        private readonly ICommunityProjectRepository _communityProjectRepository;
        private readonly IMapper _mapper;
        public CommunityProjectService(ICommunityProjectRepository communityProjectRepository, IMapper mapper)
        {
            _mapper = mapper;
            _communityProjectRepository = communityProjectRepository;
        }

        public async Task<ResponseDto> AddCommunityProject(CommunityProjectRequestDto communityProjectRequestDto)
        {
            var responseDto = new ResponseDto();
            if (_communityProjectRepository.CommunityProjectExists(communityProjectRequestDto.Name))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Community Project Exists";
                return responseDto;
            }

            if(communityProjectRequestDto.StartDate > communityProjectRequestDto.EndDate)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Start Date Cannot Be Greater Than End Date";
                return responseDto;
            }

            var communityProject = new CommunityProject();
            _mapper.Map(communityProjectRequestDto, communityProject);
            if (await _communityProjectRepository.AddCommunityProjectAsync(communityProject))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Community Project added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add CommunityProject";
            return responseDto;

        }

        public async Task<ResponseDto> EditCommunityProject(CommunityProjectUpdateRequestDto communityProjectRequestDto)
        {
            var responseDto = new ResponseDto();

            var communityProjectResponse = await _communityProjectRepository.GetCommunityProjectByIdAsync(communityProjectRequestDto.CommunityProjectId);
            if (communityProjectResponse == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Community Project Does Not Exist";
                return responseDto;
            }
            if (communityProjectResponse.Name.Trim() != communityProjectRequestDto.Name.Trim())
            {
                if (_communityProjectRepository.CommunityProjectExists(communityProjectRequestDto.Name.Trim()))
                {
                    responseDto = new ResponseDto();
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Community Project Exist";
                    return responseDto;
                }
            }
            var communityProject = new CommunityProject();
            _mapper.Map(communityProjectRequestDto, communityProject);
            communityProject.DateAdded = communityProjectResponse.DateAdded;
            if (await _communityProjectRepository.UpdateCommunityProjectAsync(communityProject))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Community Project edited successfully";
                return responseDto;
            }
            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit Community Project";
            return responseDto;

        }

        public async Task<ResponseDto> DeleteCommunityProject(int communityProjectId)
        {
            var responseDto = new ResponseDto();

            var communityProject = await _communityProjectRepository.GetCommunityProjectByIdAsync(communityProjectId);
            if (communityProject == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Community Project Does Not Exist";
                return responseDto;
            }

            communityProject.IsDeleted = true;
            communityProject.DateDeleted = DateTime.Now;

            if (await _communityProjectRepository.UpdateCommunityProjectAsync(communityProject))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Community Project deleted successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to delete Community Project";
            return responseDto;

        }

        public async Task<IEnumerable<CommunityProjectResponseDto>> GetAllCommunityProjects()
        {
            var communityProjects = await _communityProjectRepository.GetAllCommunityProjectsWithSumAsync();
            if(communityProjects.Count() == 0)
            {
                var projects = await _communityProjectRepository.GetAllCommunityProjectsAsync();
                _mapper.Map(projects, communityProjects);
            }
            return communityProjects;
        }
        public async Task<CommunityProjectResponseDto> GetCommunityProjectById(int Id)
        {
            var communityProjects = await _communityProjectRepository.GetCommunityProjectByIdDtoAsync(Id);
            if (communityProjects == null)
            {
                var communityProjectResponse = new CommunityProjectResponseDto();
                var projects = await _communityProjectRepository.GetCommunityProjectByIdAsync(Id);
                _mapper.Map(projects, communityProjectResponse);
                return communityProjectResponse;
            }
            return communityProjects;
        }
    }
}
