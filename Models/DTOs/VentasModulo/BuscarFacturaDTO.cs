using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewApiForecast.Models.DTOs.VentasModulo
{
    public class BuscarFacturaDTO
    {
        public int Id_Pedido { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        [Required]
        public int ID_Cliente { get; set; }
    }
}