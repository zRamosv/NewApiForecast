using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForecast.Models.InsertModels
{
    public class ProductosInsert
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Categoria { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Group_Id { get; set; }
    }
}