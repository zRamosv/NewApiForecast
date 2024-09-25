using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{

    public class Permisos{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Permiso_id {get;set;}
        public int User_id {get;set;}
        public Usuarios User {get;set;}
        public string Modulo {get;set;}
        public string Nivel_acceso {get;set;}
    }
}