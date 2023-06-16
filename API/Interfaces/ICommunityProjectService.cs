using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICommunityProjectService
    {
        Task<ResponseDto> AddCommunityProject(CommunityProjectRequestDto communityProjectRequestDto);
        Task<ResponseDto> EditCommunityProject(CommunityProjectUpdateRequestDto communityProjectRequestDto);
        Task<IEnumerable<CommunityProjectResponseDto>> GetAllCommunityProjects();
        Task<CommunityProjectResponseDto> GetCommunityProjectById(int Id);
        Task<ResponseDto> DeleteCommunityProject(int CommunityProjectId);
    }
}
