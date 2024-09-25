using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class PermisosInsert
    {
       [Required]
       public int User_id { get; set; }
       [Required]
       public List<PermisosBodyInsert> Permisos { get; set; }
    }

    public class PermisosBodyInsert
    {
        [Required]

        public string Modulo { get; set; }
        [Required]
        public string Aplicacion { get; set; }
        [Required]
        public string Nivel_acceso { get; set; }
    }

}
