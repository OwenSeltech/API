using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class SponsorshipPaymentRepository : ISponsorshipPaymentRepository
    {
        private readonly DataContext _context;
        public SponsorshipPaymentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSponsorshipPaymentAsync(SponsorshipPayment sponsorshipPayment)
        {
            _context.Entry(sponsorshipPayment).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSponsorshipPaymentAsync(SponsorshipPayment sponsorshipPayment)
        {
            _context.Entry(sponsorshipPayment).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SponsorshipPayment>> GetAllSponsorshipPaymentsAsync()
        {
            return await _context.SponsorshipPayments.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<SponsorshipPayment> GetSponsorshipPaymentByIdAsync(int id)
        {
            return await _context.SponsorshipPayments
                .Where(x => x.SponsorshipPaymentId == id)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
       
    }
}
