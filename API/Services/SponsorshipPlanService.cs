using AutoMapper;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class SponsorshipPlanService : ISponsorshipPlanService
    {
        private readonly ISponsorshipPlanRepository _sponsorshipPlanRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICommunityProjectRepository _communityProjectRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public SponsorshipPlanService(ISponsorshipPlanRepository sponsorshipPlanRepository, IMapper mapper, ICustomerRepository customerRepository, ICommunityProjectRepository communityProjectRepository,IProductRepository productRepository)
        {
            _mapper = mapper;
            _sponsorshipPlanRepository = sponsorshipPlanRepository;
            _customerRepository = customerRepository;
            _communityProjectRepository = communityProjectRepository;
            _productRepository = productRepository;
        }

        public async Task<ResponseDto> AddSponsorshipPlan(SponsorshipPlanRequestDto sponsorshipPlanRequestDto)
        {
            var responseDto = new ResponseDto();

            var customer = await _customerRepository.GetCustomerByIdAsync(sponsorshipPlanRequestDto.CustomerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer not found";
                return responseDto;
            }
            var product = await _productRepository.GetProductByIdAsync(sponsorshipPlanRequestDto.ProductId);
            if (product == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product not found";
                return responseDto;
            }

            if(product.CustomerId != customer.CustomerId)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product not found";
                return responseDto;
            }

            if(product.ProductType != sponsorshipPlanRequestDto.SourceOfFunds.ToUpper())
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Invalid Source of Funds";
                return responseDto;
            }

            var communityProject = await _communityProjectRepository.GetCommunityProjectByIdAsync(sponsorshipPlanRequestDto.CommunityProjectId);
            if (communityProject == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Project Not Found";
                return responseDto;
            }
            bool sponsorshipFrequency = Enum.TryParse(sponsorshipPlanRequestDto.SponsorshipFrequency.ToUpper(), out SponsorshipFrequencyEnum result);
            if (!sponsorshipFrequency)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Sponsorship Frequency Invalid";
                return responseDto;
            }
            if (sponsorshipPlanRequestDto.Amount < 1)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Amount should be greater than 0";
                return responseDto;
            }
            var sponsorshipPlan = new SponsorshipPlan();
            _mapper.Map(sponsorshipPlanRequestDto, sponsorshipPlan);
            if (await _sponsorshipPlanRepository.AddSponsorshipPlanAsync(sponsorshipPlan))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPlan added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add SponsorshipPlan";
            return responseDto;

        }

        public async Task<ResponseDto> EditSponsorshipPlan(SponsorshipPlanUpdateRequestDto sponsorshipPlanRequestDto)
        {
            var responseDto = new ResponseDto();

            var sponsorshipPlanResponse = await _sponsorshipPlanRepository.GetSponsorshipPlanByIdAsync(sponsorshipPlanRequestDto.SponsorshipPlanId);
            if (sponsorshipPlanResponse == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "SponsorshipPlan Does Not Exist";
                return responseDto;
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(sponsorshipPlanRequestDto.CustomerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer not found";
                return responseDto;
            }
            var product = await _productRepository.GetProductByIdAsync(sponsorshipPlanRequestDto.ProductId);
            if (product == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product not found";
                return responseDto;
            }

            if (product.CustomerId != customer.CustomerId)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product not found";
                return responseDto;
            }

            if (product.ProductType != sponsorshipPlanRequestDto.SourceOfFunds.ToUpper())
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Invalid Source of Funds";
                return responseDto;
            }

            var communityProject = await _communityProjectRepository.GetCommunityProjectByIdAsync(sponsorshipPlanRequestDto.CommunityProjectId);
            if (communityProject == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Project Not Found";
                return responseDto;
            }

            if (sponsorshipPlanRequestDto.Amount < 1)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Amount should be greater than 0";
                return responseDto;
            }

            bool sponsorshipFrequency = Enum.TryParse(sponsorshipPlanRequestDto.SponsorshipFrequency.ToUpper(), out SponsorshipFrequencyEnum result);
            if (!sponsorshipFrequency)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Sponsorship Frequency Invalid";
                return responseDto;
            }
            var sponsorshipPlan = new SponsorshipPlan();
            _mapper.Map(sponsorshipPlanRequestDto, sponsorshipPlan);
            sponsorshipPlan.DateAdded = sponsorshipPlanResponse.DateAdded;
            if (await _sponsorshipPlanRepository.UpdateSponsorshipPlanAsync(sponsorshipPlan))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPlan edited successfully";
                return responseDto;
            }
            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit SponsorshipPlan";
            return responseDto;

        }

        public async Task<ResponseDto> DeleteSponsorshipPlan(int sponsorshipPlanId)
        {
            var responseDto = new ResponseDto();

            var sponsorshipPlan = await _sponsorshipPlanRepository.GetSponsorshipPlanByIdAsync(sponsorshipPlanId);
            if (sponsorshipPlan == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "SponsorshipPlan Does Not Exist";
                return responseDto;
            }

            sponsorshipPlan.IsDeleted = true;
            sponsorshipPlan.DateDeleted = DateTime.Now;

            if (await _sponsorshipPlanRepository.UpdateSponsorshipPlanAsync(sponsorshipPlan))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "SponsorshipPlan deleted successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to delete SponsorshipPlan";
            return responseDto;

        }

        public async Task<IEnumerable<SponsorshipPlan>> GetAllSponsorshipPlans()
        {
            var SponsorshipPlans = await _sponsorshipPlanRepository.GetAllSponsorshipPlansAsync();
            return SponsorshipPlans;
        }
        public async Task<SponsorshipPlan> GetSponsorshipPlanById(int Id)
        {
            var SponsorshipPlan = await _sponsorshipPlanRepository.GetSponsorshipPlanByIdAsync(Id);
            return SponsorshipPlan;
        }
        public async Task<IEnumerable<SponsorshipPlan>> GetSponsorshipPlanByCustId(int Id)
        {
            var sponsorshipPlans = await _sponsorshipPlanRepository.GetSponsorshipPlanByCustomerIdAsync(Id);
            return sponsorshipPlans;
        }
    }
}
