using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class FoliosInsert
    {
        [Required]
        public string TipoDocumento { get; set; }
        [Required]
        public int SiguienteFolio { get; set; }
        [Required]
        public int Sucursal_id { get; set; }
    }
}