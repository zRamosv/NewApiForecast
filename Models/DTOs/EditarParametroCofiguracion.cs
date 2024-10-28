using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Options;

namespace ApiForecast.Models.DTOs{
    public class EditarParametroCofiguracion{

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Valor { get; set; }
    }
}