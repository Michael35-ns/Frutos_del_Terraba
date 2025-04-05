using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

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
            var proveedores = await _pedidoService.ObtenerTodosProveedoresAsync();
            var viewModel = new PedidoViewModel
            {
                Pedido = pedidos.ToList(),
                Proveedores = proveedores.ToList(),
                NuevoPedido = new PedidoDTOModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoDTOModel pedido)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            var pedidoCreado = await _pedidoService.CrearPedidoAsync(pedido);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var pedidoDTO = await _pedidoService.ObtenerPedidoPorIdAsync(id);
            if (pedidoDTO == null) return NotFound();

            var proveedores = await _pedidoService.ObtenerTodosProveedoresAsync(); // Obtiene la lista de proveedores

            var pedidoViewModel = new PedidoViewModel
            {
                Proveedores = proveedores.ToList(),
                NuevoPedido = new PedidoDTOModel
                {
                    Id_pedido = pedidoDTO.Id_pedido,
                    UserId = pedidoDTO.UserId,
                    Fecha = pedidoDTO.Fecha,

                    Id_proveedor = pedidoDTO.Id_proveedor
                },

            };

            return View(pedidoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, PedidoDTOModel pedido)
        {
           
                var pedidoJson = JsonSerializer.Serialize(pedido);
                Console.WriteLine($"JSON recibido: {pedidoJson}");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pedidoActualizado = await _pedidoService.ActualizarPedidoAsync(id, pedido);
            TempData["SuccessMessage"] = "Producto actualizado exitosamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pedidoEliminado = await _pedidoService.EliminarPedidoAsync(id);
            if (pedidoEliminado)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        private string GetUserIdFromCookie()
        {
            var token = Request.Cookies["AuthToken"];  // Obtener el token desde la cookie
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);  // Leer el JWT
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;  // Extraer el userID

            return userId;
        }


    }
}