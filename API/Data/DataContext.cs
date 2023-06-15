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

                // Relationship with Customer
                entity.HasOne<Customer>()
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CommunityProject>(entity =>
            {
                entity.HasKey(p => p.ProjectId);
            });

            modelBuilder.Entity<SponsorshipPlan>(entity =>
            {
                entity.HasKey(p => p.PlanId);

                // Relationship with Customer
                entity.HasOne<Customer>()
                    .WithMany(c => c.SponsorshipPlans)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship with Product
                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relationship with CommunityProject
                entity.HasOne<CommunityProject>()
                    .WithMany()
                    .HasForeignKey(p => p.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SponsorshipPayment>(entity =>
            {
                entity.HasKey(p => p.PaymentId);

                // Relationship with SponsorshipPlan
                entity.HasOne<SponsorshipPlan>()
                    .WithMany()
                    .HasForeignKey(p => p.PlanId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<SponsorshipPlan> SponsorshipPlans { get; set; }
        public DbSet<CommunityProject> CommunityProjects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SponsorshipPayment> SponsorshipPayments { get; set; }

    }
}
