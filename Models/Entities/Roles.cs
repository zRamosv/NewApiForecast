using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{
    public class Roles{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<UserRoles> UserRole { get; set; }
    }
}