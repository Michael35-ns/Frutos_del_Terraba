using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.ObtenerPedidosAsync();
            var viewModel = new PedidoViewModel
            {
                Pedidos = pedidos.ToList(), 
                NuevoPedido = new Pedido()  
            };
            return View(viewModel); 
        }

    }
}
