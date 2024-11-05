using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;

namespace ApiForecast.Services
{
    public interface IVentasService
    {
        Task<List<Ventas>> GetVentasAsync();
        Task<Ventas?> GetVentaByIdAsync(int id);
        Task<Ventas> CreateVenta(VentasInsert venta);

        Task<Ventas> UpdateVenta(int id, VentasDTO venta);

        Task<Ventas> DeleteVenta(int id);
    }
}