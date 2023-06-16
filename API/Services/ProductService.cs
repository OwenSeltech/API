using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using System;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ResponseDto> AddProduct(ProductRequestDto productRequestDto)
        {
            var responseDto = new ResponseDto();

            bool productTypeExist = Enum.TryParse(productRequestDto.ProductType.ToUpper(), out ProductTypeEnum result);
            if (!productTypeExist)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Product Type Invalid";
                return responseDto;
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(productRequestDto.CustomerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Does Not Exist";
                return responseDto;
            }
            var product = new Product();
            _mapper.Map(productRequestDto, product);

            product.ProductNumber = Utils.GenerateRandomNumber(productRequestDto.ProductType.ToUpper());
            product.Customer = customer;
            if (await _productRepository.AddProductAsync(product))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Product added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Product";
            return responseDto;

        }

        public async Task<ResponseDto> EditProduct(ProductUpdateRequestDto productRequestDto)
        {
            var responseDto = new ResponseDto();
            bool productTypeExist = Enum.TryParse(productRequestDto.ProductType.ToUpper(), out ProductTypeEnum result);
            if (!productTypeExist)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Product Type Invalid";
                return responseDto;
            }
            var productResponse = await _productRepository.GetProductByIdAsync(productRequestDto.ProductId);
            if (productResponse == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product Does Not Exist";
                return responseDto;
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(productRequestDto.CustomerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Does Not Exist";
                return responseDto;
            }

            var product = new Product();
            _mapper.Map(productRequestDto, product);
            product.DateAdded = productResponse.DateAdded;
            product.Customer = customer;
            if (await _productRepository.UpdateProductAsync(product))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Product edited successfully";
                return responseDto;
            }
            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit Product";
            return responseDto;

        }

        public async Task<ResponseDto> DeleteProduct(int productId)
        {
            var responseDto = new ResponseDto();

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product Does Not Exist";
                return responseDto;
            }

            product.IsDeleted = true;
            product.DateDeleted = DateTime.Now;

            if (await _productRepository.UpdateProductAsync(product))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Product deleted successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to delete Product";
            return responseDto;

        }
        public async Task<ResponseDto> TopUpProduct(TopUpDto topUpDto)
        {
            var responseDto = new ResponseDto();

            var customer = await _customerRepository.GetCustomerByIdAsync(topUpDto.CustomerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Does Not Exist";
                return responseDto;
            }
            var product = await _productRepository.GetProductByIdAsync(topUpDto.ProductId);
            if (product == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product Does Not Exist";
                return responseDto;
            }

            if (product.CustomerId != customer.CustomerId)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Product Does Not Belong To Customer";
                return responseDto;
            }

            product.Balance = product.Balance + topUpDto.Amount;

            if (await _productRepository.UpdateProductAsync(product))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "TopUp Success";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to TopUp";
            return responseDto;

        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var Products = await _productRepository.GetAllProductsAsync();
            return Products;
        }
        public async Task<Product> GetProductById(int Id)
        {
            var Product = await _productRepository.GetProductByIdAsync(Id);
            return Product;
        }
        public async Task<IEnumerable<Product>> GetProductsByCustId(int Id)
        {
            var Products = await _productRepository.GetProductsByCustIdAsync(Id);
            return Products;
        }
    }
}
