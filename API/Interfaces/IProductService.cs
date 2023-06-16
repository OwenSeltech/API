using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDto> AddProduct(ProductRequestDto productRequestDto);
        Task<ResponseDto> EditProduct(ProductUpdateRequestDto productRequestDto);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int Id);
        Task<ResponseDto> DeleteProduct(int ProductId);
        Task<IEnumerable<Product>> GetProductsByCustId(int Id);
        Task<ResponseDto> TopUpProduct(TopUpDto topUpDto);
    }
}
