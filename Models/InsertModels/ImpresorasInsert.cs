using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class ImpresorasInsert
    {
        [Required]
        public string TipoDocumento { get; set; }
        [Required]

        public string Impresora { get; set; }
        [Required]

        public int Sucursal_id { get; set; }
    }
}