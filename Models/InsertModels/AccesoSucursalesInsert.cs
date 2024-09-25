using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class AccesoSucursalesInsert
    {
        [Required]
        public int User_id { get; set; }
        public List<int> SucursalesIds { get; set; }

        
    }
}