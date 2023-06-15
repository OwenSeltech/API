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
        }
	}
}
