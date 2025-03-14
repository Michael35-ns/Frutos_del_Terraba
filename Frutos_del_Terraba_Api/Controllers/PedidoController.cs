using Frutos_del_Terraba_Api.DTO;

using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var pedidos = await _pedidoService.ObtenerTodosPedidos();
            return Ok(pedidos);
        }



        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDTOModel Pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pedido = await _pedidoService.CrearPedidoAsync(Pedido);
            return Ok(pedido);
        }
    }
}
