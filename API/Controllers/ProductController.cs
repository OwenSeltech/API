using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("addProduct")]
        public async Task<ActionResult<ResponseDto>> AddProduct(ProductRequestDto requestDto)
        {
            var response = await _productService.AddProduct(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
      
        [HttpPost("deleteProduct/{ProductId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(int ProductId)
        {
            var response = await _productService.DeleteProduct(ProductId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpPost("topUpProduct")]
        public async Task<ActionResult<ResponseDto>> TopUpProduct(TopUpDto topUpDto)
        {
            var response = await _productService.TopUpProduct(topUpDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();
            return Ok(response);
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<Product>> GetProduct(int ProductId)
        {
            var response = await _productService.GetProductById(ProductId);
            return Ok(response);
        }
        [HttpGet("customerProducts/{CustId}")]
        public async Task<ActionResult<Product>> GetProductsByCustId(int CustId)
        {
            var response = await _productService.GetProductsByCustId(CustId);
            return Ok(response);
        }


    }
}
