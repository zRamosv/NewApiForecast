using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.Entities{
    public class UserRoles{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public int User_Id {get;set;}
        public Usuarios User {get;set;}
        public int Role_Id {get;set;}
        public Roles Role {get;set;}
        public int Sucursal_id {get;set;}
        public Sucursales Sucursal {get;set;}
    }
}