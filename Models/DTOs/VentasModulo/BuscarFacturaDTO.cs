using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewApiForecast.Models.DTOs.VentasModulo
{
    public class BuscarFacturaDTO
    {
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public float Iva_Aplicable { get; set; }
        public int ID_Cliente { get; set; }
    }
}