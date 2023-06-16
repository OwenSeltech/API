using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class CommunityProjectRepository : ICommunityProjectRepository
    {
        private readonly DataContext _context;
        public CommunityProjectRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCommunityProjectAsync(CommunityProject communityProject)
        {
            _context.Entry(communityProject).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCommunityProjectAsync(CommunityProject communityProject)
        {
            _context.Entry(communityProject).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CommunityProject>> GetAllCommunityProjectsAsync()
        {
            return await _context.CommunityProjects.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<CommunityProject> GetCommunityProjectByIdAsync(int id)
        {
            return await _context.CommunityProjects
                .Where(x => x.CommunityProjectId == id)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<CommunityProjectResponseDto> GetCommunityProjectByIdDtoAsync(int id)
        {
            var query = from comProj in _context.CommunityProjects
                        join SpPlans in _context.SponsorshipPlans on comProj.CommunityProjectId equals SpPlans.CommunityProjectId
                        join SpPayment in _context.SponsorshipPayments on SpPlans.SponsorshipPlanId equals SpPayment.SponsorshipPlanId
                        where comProj.CommunityProjectId == id
                        group new { comProj.StartDate, comProj.EndDate, comProj.TotalFundsRequired, SpPayment.Amount } by new { comProj.StartDate, comProj.EndDate, comProj.TotalFundsRequired } into g
                        select new CommunityProjectResponseDto
                        {
                            StartDate = g.Key.StartDate,
                            EndDate = g.Key.EndDate,
                            TotalFundsRequired = g.Key.TotalFundsRequired,
                            TotalFundsRaised = g.Sum(sp => sp.Amount)
                        };
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<CommunityProjectResponseDto>> GetAllCommunityProjectsWithSumAsync()
        {
            var query = from comProj in _context.CommunityProjects
                        join SpPlans in _context.SponsorshipPlans on comProj.CommunityProjectId equals SpPlans.CommunityProjectId
                        join SpPayment in _context.SponsorshipPayments on SpPlans.SponsorshipPlanId equals SpPayment.SponsorshipPlanId
                        group new { comProj.StartDate, comProj.EndDate, comProj.TotalFundsRequired, SpPayment.Amount } by new { comProj.StartDate, comProj.EndDate, comProj.TotalFundsRequired } into g
                        select new CommunityProjectResponseDto
                        {
                            StartDate = g.Key.StartDate,
                            EndDate=  g.Key.EndDate,
                            TotalFundsRequired = g.Key.TotalFundsRequired,
                            TotalFundsRaised  = g.Sum(sp => sp.Amount)
                        };
            return query.ToList();
        }

        public bool CommunityProjectExists(string name)
        {
            return _context.CommunityProjects.Any(o => o.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
