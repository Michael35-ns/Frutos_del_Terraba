using Frutos_del_Terraba.Helpers.Implementaciones;
using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.ObtenerTodosPedidosAsync();
            var proveedores = await _pedidoService.ObtenerTodosProveedoresAsync(); // Obtenemos los proveedores
            var viewModel = new PedidoViewModel
            {
                Pedido = pedidos.ToList(),
                Proveedores = proveedores.ToList(), // Pasamos los proveedores al ViewModel
                NuevoPedido = new PedidoDTOModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoDTOModel pedido)
        {
            if (!ModelState.IsValid)
            {
                // Registra los errores de validación para más detalles
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }

                return BadRequest(ModelState); // Devolvemos los errores específicos
            }

            var pedidoCreado = await _pedidoService.CrearPedidoAsync(pedido);
            return RedirectToAction("Index");
        }
    }
}
