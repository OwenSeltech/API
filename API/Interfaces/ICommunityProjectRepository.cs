using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICommunityProjectRepository
    {
        Task<bool> AddCommunityProjectAsync(CommunityProject communityProject);
        Task<bool> UpdateCommunityProjectAsync(CommunityProject communityProject);
        Task<IEnumerable<CommunityProject>> GetAllCommunityProjectsAsync();
        Task<CommunityProject> GetCommunityProjectByIdAsync(int id);
        Task<IEnumerable<CommunityProjectResponseDto>> GetAllCommunityProjectsWithSumAsync();
        Task<CommunityProjectResponseDto> GetCommunityProjectByIdDtoAsync(int id);
        public bool CommunityProjectExists(string email);
    }
}
