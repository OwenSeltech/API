using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class SponsorshipPlanRepository : ISponsorshipPlanRepository
    {
        private readonly DataContext _context;
        public SponsorshipPlanRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSponsorshipPlanAsync(SponsorshipPlan sponsorshipPlan)
        {
            _context.Entry(sponsorshipPlan).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSponsorshipPlanAsync(SponsorshipPlan sponsorshipPlan)
        {
            _context.Entry(sponsorshipPlan).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SponsorshipPlan>> GetAllSponsorshipPlansAsync()
        {
            return await _context.SponsorshipPlans.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<SponsorshipPlan> GetSponsorshipPlanByIdAsync(int id)
        {
            return await _context.SponsorshipPlans
                .Where(x => x.SponsorshipPlanId == id)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SponsorshipPlan>> GetSponsorshipPlanByCustomerIdAsync(int id)
        {
            return await _context.SponsorshipPlans
                .Where(x => x.CustomerId == id)
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .Include(x => x.CommunityProject)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool SponsorshipPlanExists(int projectId)
        {
            return _context.SponsorshipPlans.Any(o => o.CommunityProjectId == projectId);
        }
    }
}
