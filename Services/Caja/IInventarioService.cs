using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;

namespace ApiForecast.Services.Caja
{
    public interface IInventarioService
    {
        Task<List<Productos>> GetFullInventario();
        Task<List<Productos>> ComprarDeProveedores(OrdenesDeCompra orden);
        Task<List<Productos>> VenderAClientes(Clientes cliente);
        Task<Productos> UpdateInventario(ProductosDTO nuevoStock);
    }
}