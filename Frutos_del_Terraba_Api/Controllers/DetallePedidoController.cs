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

        [HttpGet("todos/{id}")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDetallesId(int id)
        {
            var detalles = await _detallesPedido.ObtenerDetallesPedidoPorId(id);
            return Ok(detalles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarDetallesPedido(int id, DetallesPedidoDTOModel detalles)
        {
            if(!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }
            var detallesPedido = await _detallesPedido.ActualizarDetallesPedido(id, detalles);
            return Ok(new { message = "Detalle actualizado correctamente"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDetallesPedido(int id)
        {
            var detalles = await _detallesPedido.EliminarDetallesPedido(id);
            return Ok(new { message = "Detalle eliminado correctamente" });
        }
    }
}
