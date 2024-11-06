using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiForecast.Models.Entities;
using NewApiForecast.Models.Entities.VentasModulo;
using NewApiForecast.Models.Enums;

namespace NewApiForecast.Models.Entities
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Pedido { get; set; }
        public int Id_FormaDePago { get; set; }
        public FormaDePago FormaDePago { get; set; }
        [ForeignKey("Id_Cliente")]
        public int Id_Cliente { get; set; }
        public Clientes Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Moneda { get; set; }
        public decimal TipoDeCambio { get; set; }
        public EstatusDePedido Estado { get; set; }
        [JsonIgnore]
        public ICollection<Factura> Facturas { get; set; }
        [JsonIgnore]
        public ICollection<ConsignacionPedido> Consignaciones { get; set; }
        [JsonIgnore]
        public ICollection<Productos> Productos { get; set; }
    }
}