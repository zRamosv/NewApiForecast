
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;

namespace NewApiForecast.Services
{
    public interface IProductService
    {
        Task<Productos?> GetProducto(int id);
        Task<List<Productos>> GetAllProductos();
        Task<Productos> CreateProduct(ProductosInsert producto);
        Task<Productos?> UpdateProduct(int id, ProductosDTO producto);
        Task<Productos?> Delete(int id);
    }
}