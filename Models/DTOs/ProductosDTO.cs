using ApiForecast.Models.Entities;

namespace ApiForecast.Models.DTOs
{

    public class ProductosDTO
    {

        public int? Product_Id { get; set; }
        public string? Nombre { get; set; }
        public string? Categoria { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public string? Clave { get; set; }
        public string? Descripcion { get; set; }
        public int? Group_Id { get; set; }
        public Grupos? Grupos { get; set; }

    }
}