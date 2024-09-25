using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities
{
    public class Folios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Folio_id { get; set; }
        public string TipoDocumento { get; set; }
        public int SiguienteFolio { get; set; }
        public int Sucursal_id { get; set; }
        public Sucursales Sucursales { get; set; }
    }
}