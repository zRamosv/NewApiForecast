using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiForecast.Data;
using ApiForecast.Models.DTOs;
using ApiForecast.Models.Entities;
using ApiForecast.Models.InsertModels;
using Microsoft.EntityFrameworkCore;

namespace NewApiForecast.Services
{
    public class ProductService : IProductService
    {
        private readonly ForecastContext _context;
        public ProductService(ForecastContext context)
        {
            _context = context;
        }
        public async Task<List<Productos>> GetAllProductos()
        {
            return await _context.Productos
                .Include(x => x.Grupos)
                .Include(x => x.Proveedor)
                .ToListAsync();
        }

        public async Task<Productos?> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(x => x.Grupos)
                .Include(x => x.Proveedor)
                .FirstOrDefaultAsync(x => x.Product_Id == id);
            return producto;
        }

        public async Task<Productos> CreateProduct(ProductosInsert producto)
        {
            var insert = new Productos
            {
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Clave = producto.Clave,
                Descripcion = producto.Descripcion,
                Group_Id = producto.Group_Id,
                Provider_id = producto.Provider_id
            };
            _context.Productos.Add(insert);
            await _context.SaveChangesAsync();
            return insert;
        }

        public async Task<Productos?> UpdateProduct(int id, ProductosDTO producto)
        {
            var existingProduct = await _context.Productos.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Nombre = producto.Nombre ?? existingProduct.Nombre;
            existingProduct.Categoria = producto.Categoria ?? existingProduct.Categoria;
            existingProduct.Precio = producto.Precio ?? existingProduct.Precio;
            existingProduct.Stock = producto.Stock ?? existingProduct.Stock;
            existingProduct.Clave = producto.Clave ?? existingProduct.Clave;
            existingProduct.Descripcion = producto.Descripcion ?? existingProduct.Descripcion;
            existingProduct.Group_Id = producto.Group_Id ?? existingProduct.Group_Id;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async  Task<Productos?> Delete(int id)
        {
            var delete = await _context.Productos.FirstOrDefaultAsync(x => x.Product_Id == id);
            if (delete == null)
            {
                return null;
            }
            _context.Productos.Remove(delete);
            await _context.SaveChangesAsync();
            return delete;
        }
    }
}