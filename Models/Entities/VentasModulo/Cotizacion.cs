using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiForecast.Models.Entities;


namespace NewApiForecast.Models.Entities.VentasModulo
{
    public class Cotizacion
    {
        [Key]
        public int Id_Cotizacion { get; set; }
        [ForeignKey("Cliente")]
        public int Id_Cliente { get; set; }
        public Clientes Cliente { get; set; }  
        [ForeignKey("Producto")]
        public int Id_Producto { get; set; }
        public Productos Producto { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public decimal  Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        
    }
}