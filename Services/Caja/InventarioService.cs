using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Services.Caja;
using NewApiForecast.Services;

namespace ApiForecast.Controllers.Caja
{
    public class InventarioService: IInventarioService
    {
        private readonly IProductService _productService;
        public InventarioService(IProductService productService)
        {
            _productService = productService;
        }

        public Task<List<Productos>> ComprarDeProveedores(OrdenesDeCompra orden)
        {
            throw new NotImplementedException();
        }

        public Task<List<Productos>> GetFullInventario()
        {
            throw new NotImplementedException();
        }

        public Task<Productos> UpdateInventario(ProductosDTO nuevoStock)
        {
            throw new NotImplementedException();
        }

        public Task<List<Productos>> VenderAClientes(Clientes cliente)
        {
            throw new NotImplementedException();
        }
    }
}