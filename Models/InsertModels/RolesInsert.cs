using System.ComponentModel.DataAnnotations;

namespace ApiForecast.Models.InsertModels
{
    public class RolesInsert
    {
        [Required]
        public string Name { get; set; }
    }
}