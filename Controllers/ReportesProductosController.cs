using ApiForecast.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiForecast.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReportesProductosController : ControllerBase
    {

        private readonly ForecastContext _context;
        public ReportesProductosController(ForecastContext context)
        {

            _context = context;
        }


    }
}