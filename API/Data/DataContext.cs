using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                
            });

            modelBuilder.Entity<CommunityProject>(entity =>
            {
                entity.HasKey(p => p.CommunityProjectId);
            });

            modelBuilder.Entity<SponsorshipPlan>(entity =>
            {
                entity.HasKey(p => p.SponsorshipPlanId);
            });

            modelBuilder.Entity<SponsorshipPayment>(entity =>
            {
                entity.HasKey(p => p.SponsorshipPaymentId);
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<SponsorshipPlan> SponsorshipPlans { get; set; }
        public DbSet<CommunityProject> CommunityProjects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SponsorshipPayment> SponsorshipPayments { get; set; }

    }
}
