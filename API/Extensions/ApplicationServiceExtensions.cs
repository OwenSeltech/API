using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<ICommunityProjectService, CommunityProjectService>();
			services.AddScoped<ISponsorshipPlanService, SponsorshipPlanService>();
			services.AddScoped<ISponsorshipPaymentService, SponsorshipPaymentService>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<ICommunityProjectRepository, CommunityProjectRepository>();
			services.AddScoped<ISponsorshipPlanRepository, SponsorshipPlanRepository>();
			services.AddScoped<ISponsorshipPaymentRepository, SponsorshipPaymentRepository>();

			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			var connectionString = config.GetConnectionString("DefaultConnection");
			services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(
				connectionString, sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure();

				}), ServiceLifetime.Transient);
			return services;
		}
	}
}
