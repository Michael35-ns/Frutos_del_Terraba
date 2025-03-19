using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Frutos_del_Terraba.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly IDetallePedidoService _detallesPedido;
        private readonly IProductoService _productoService;
        private readonly HttpClient _httpClient;
        private readonly string _productosUrl = "https://localhost:7240/api/producto";


        public DetallePedidoController(IDetallePedidoService detallesPedido, IProductoService productoService, HttpClient httpClient)
        {
            _detallesPedido = detallesPedido;
            _productoService = productoService;
            _httpClient = httpClient;
        }

        public async Task<string> ObtenerNombreProducto(int idProducto)
        {
            var producto = await _httpClient.GetFromJsonAsync<ProductoDTOModel>($"https://localhost:7240/api/producto/{idProducto}");
            return producto?.Nombre ?? "Producto no encontrado";
        }

        public async Task<IActionResult> VerDetallesPedido(int id)
        {
            // Obtener los detalles del pedido desde la API
            var detallesDTO = await _detallesPedido.ObtenerDetallesPedidos(id);
            var detallesViewModel = new List<DetallesPedidoViewModel>();

            // Obtener los nombres de los productos
            foreach (var detalle in detallesDTO)
            {
                var producto = await _httpClient.GetFromJsonAsync<ProductoDTOModel>($"{_productosUrl}/{detalle.Id_producto}");

                detallesViewModel.Add(new DetallesPedidoViewModel
                {
                    Id_detalle = detalle.Id_detalle,
                    Cantidad = detalle.Cantidad,
                    Observaciones = detalle.Observaciones,
                    Id_pedido = detalle.Id_pedido,
                    Id_producto = detalle.Id_producto,
                    nombreProducto = producto?.Nombre ?? "Producto no encontrado"
                });
            }

            return View(detallesViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            var model = new DetallesPedidoDTOModel
            {
                Id_pedido = id 
            };

            ViewBag.Productos = await _productoService.ObtenerTodosProductosAsync();

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetallesPedidoDTOModel detallePedido)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error en la validación del formulario.";
                ViewBag.Productos = await _productoService.ObtenerTodosProductosAsync();

                return View(detallePedido);
            }

            var resultado = await _detallesPedido.AgregarDetallesPedido(detallePedido);

            if (resultado != null)
            {
                TempData["SuccessMessage"] = "Detalle del pedido agregado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un problema al agregar el detalle del pedido.";
            }
            ViewBag.Productos = await _productoService.ObtenerTodosProductosAsync();

            return View(detallePedido);
        }

    }
}
