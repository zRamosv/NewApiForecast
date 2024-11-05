using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewApiForecast.Controllers.Caja
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        public PedidosController(IPedidosService pedidosService)
        {
            
        }
    }

    public interface IPedidosService
    {
        
    }
}