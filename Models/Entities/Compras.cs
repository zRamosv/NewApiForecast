using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiForecast.Models.Entities;

namespace ApiForescast.Models.Entities{


    public class Compras{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Purchase_id {get;set;}
        public int Product_id {get;set;}
        public Productos Product {get;set;}
        public DateOnly Fecha {get;set;}
        public int Cantidad {get;set;}
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio {get;set;}
        public int Provider_id {get;set;}
        public Proveedores Proveedor {get;set;}
        public bool MonedaUSD {get;set;}

    }

}