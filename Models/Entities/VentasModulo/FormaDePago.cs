using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewApiForecast.Models.Entities.VentasModulo
{
    public class FormaDePago
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID_FormaDePago { get; set; }
        public string Description { get; set; }
        public float Comision { get; set; }
    }
}