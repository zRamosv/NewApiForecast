using NewApiForecast.Models.Entities;

namespace NewApiForecast.Services.Caja
{
    public interface IPedidosService
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task<Pedido> CreatePedido(Pedido factura);
        Task<Pedido> UpdatePedido(int id, Pedido factura);
    }
}