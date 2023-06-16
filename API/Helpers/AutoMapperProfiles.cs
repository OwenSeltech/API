using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
            CreateMap<CustomerRequestDto, Customer>()
                .ForMember(x => x.DateAdded, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CustomerUpdateRequestDto, Customer>();
            CreateMap<ProductRequestDto, Product>()
                .ForMember(x => x.DateAdded, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.ProductType, opt => opt.MapFrom(src => src.ProductType.ToUpper()));
            CreateMap<ProductUpdateRequestDto, Product>()
                .ForMember(x => x.ProductType, opt => opt.MapFrom(src => src.ProductType.ToUpper()));
            CreateMap<CommunityProjectRequestDto, CommunityProject>()
               .ForMember(x => x.DateAdded, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CommunityProjectUpdateRequestDto, CommunityProject>();
            CreateMap<CommunityProject, CommunityProjectResponseDto>();
        }
	}
}
