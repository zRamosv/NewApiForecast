using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{

    public class InsertUserRoleBatch
    {

        [Required]
        public int IdUsuario { get; set; }
        public List<BodyRolesBatch> Roles { get; set; }



    }
    public class BodyRolesBatch
    {
        [Required]
        public int SucursalId { get; set; }
        [Required]
        public int RolId { get; set; }
    }
}