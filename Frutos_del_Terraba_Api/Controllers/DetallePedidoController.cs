using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/detallepedido")]
    [ApiController]
    public class DetallePedidoController : Controller
    {
        private readonly IDetallesPedido _detallesPedido;

        public DetallePedidoController(IDetallesPedido detallesPedido)
        {
            _detallesPedido = detallesPedido;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDetallesPedido(int id)
        {
            var detalles = await _detallesPedido.ObtenerDetallesPedidos(id);
            return Ok(detalles);
        }
    }
}
