using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Servicios.Implementaciones;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/detallepedido")]
    [ApiController]
    public class DetallePedidoController : Controller
    {
        private readonly IDetallesPedidoService _detallesPedido;

        public DetallePedidoController(IDetallesPedidoService detallePedidoService)
        {
            _detallesPedido = detallePedidoService ?? throw new ArgumentNullException(nameof(detallePedidoService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDetallesPedido(int id)
        {
            var detalles = await _detallesPedido.ObtenerDetallesPedidos(id);
            return Ok(detalles);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDetallesPedido(DetallesPedidoDTOModel detalles)
        {
            var detallesPedido = await _detallesPedido.AgregarDetallesPedido(detalles);
            return Ok(detallesPedido);
        }
    }
}
