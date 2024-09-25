using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{

    public class AccessoSucursales{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Acceso_id { get; set; }
        public int User_id { get; set; }
        public Usuarios Usuario { get; set; }
        public string Nombre_user { get; set; }
        public int Sucursal_id { get; set; }
        public Sucursales Sucursal { get; set; }
    }
}