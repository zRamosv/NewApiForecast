using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class UserRoleInsert
    {
        [Required]
        public int User_Id {get;set;}
        [Required]
        public int Role_Id {get;set;}
        [Required]
        public int Sucursal_id {get;set;}
    }
}