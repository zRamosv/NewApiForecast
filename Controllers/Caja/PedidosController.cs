using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewApiForecast.Services.Caja;

namespace NewApiForecast.Controllers.Caja
{
    [ApiController]
    [Route("api/ModuloVentas/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosService _pedidosService;
        public PedidosController(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
        }
    }
}