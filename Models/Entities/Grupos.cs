using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForecast.Models.Entities{

    public class Grupos{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Group_id {get; set;}
        public string descripcion {get; set;}
        [JsonIgnore]
        public ICollection<Productos> Productos {get; set;}
        
    }
}