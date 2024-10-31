namespace ApiForecast.Models.InsertModels
{

    public class InsertOrdenCompra
    {
        public List<OrdenDeCompraBody> OrdenDeCompra { get; set; }
    }
    public class OrdenDeCompraBody
    {
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
    }
}