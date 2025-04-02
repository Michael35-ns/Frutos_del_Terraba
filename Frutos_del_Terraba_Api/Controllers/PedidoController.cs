using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPedidos()
        {
            var pedidos = await _pedidoService.ObtenerTodosPedidosAsync(); // Correcto: método asíncrono
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedidoPorId(int id)
        {
            try
            {
                var pedido = await _pedidoService.ObtenerPedidoPorIdAsync(id); // Correcto: método asíncrono
                return Ok(pedido);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] PedidoDTOModel pedido)
        {
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                    }

                    return BadRequest(ModelState); 
                }

                var pedidoActualizado = await _pedidoService.ActualizarPedidoAsync(id, pedido);

                return Ok(new { message = "Pedido actualizado exitosamente." });
            
        }


        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDTOModel Pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Correcto: devuelve los errores de validación
            }

            var pedido = await _pedidoService.CrearPedidoAsync(Pedido);
            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var pedidoEliminado = await _pedidoService.EliminarPedidoAsync(id);
            if (pedidoEliminado)
            {
                return Ok(new { mensaje = "Pedido eliminado exitosamente." });
            }
            else
            {
                return NotFound(new { mensaje = "Pedido no encontrado." });
            }
        }

    }
}