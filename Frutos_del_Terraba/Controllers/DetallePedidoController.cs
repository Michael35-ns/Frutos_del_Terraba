using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Net.Http;
using System.Text.Json;

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
            var detallesDTO = await _detallesPedido.ObtenerDetallesPedido(id);
            var detallesViewModel = new List<DetallesPedidoViewModel>();

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
            ViewBag.Id_pedido = id;

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var detalle = await _detallesPedido.ObtenerDetallesPedidoId(id);
            ViewBag.Productos = (await _productoService.ObtenerTodosProductosAsync());

            return View(detalle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DetallesPedidoDTOModel detalles)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Error en la validación del formulario.";
                ViewBag.Productos = await _productoService.ObtenerTodosProductosAsync();

                return View(detalles);
            }

            var resultado = await _detallesPedido.ActualizarDetallePedido(id, detalles);

            if (resultado != null)
            {
                TempData["SuccessMessage"] = "Detalle del pedido actualizado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Hubo un problema al actualizar el detalle del pedido.";
            }
            ViewBag.Productos = await _productoService.ObtenerTodosProductosAsync();

            return RedirectToAction("Index", "Pedido");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var detalleEliminado = await _detallesPedido.EliminarDetallesPedido(id);
            if (detalleEliminado)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public async Task<IActionResult> DownloadPDF(int id)
        {
            int[] anchosColumnas = { 100, 100, 200 }; // Observaciones más ancha

            var detallesDTO = await _detallesPedido.ObtenerDetallesPedido(id);
            if (detallesDTO == null || detallesDTO.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pudo obtener el reporte.";
                return RedirectToAction("Index");
            }

            var pedido = await _httpClient.GetFromJsonAsync<PedidoDTOModel>($"https://localhost:7240/api/pedido/{id}");
            if (pedido == null)
            {
                TempData["ErrorMessage"] = "No se pudo obtener el proveedor del pedido.";
                return RedirectToAction("Index");
            }

            var proveedor = await _httpClient.GetFromJsonAsync<ProveedorDTOModel>($"https://localhost:7240/api/proveedor/{pedido.Id_proveedor}");
            string proveedorNombre = proveedor?.Nombre ?? "Proveedor no encontrado";

            var detallesViewModel = new List<DetallesPedidoViewModel>();
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

            using (var stream = new MemoryStream())
            {
                var documento = new PdfDocument();
                var pagina = documento.AddPage();
                var gfx = XGraphics.FromPdfPage(pagina);

                var fuenteFecha = new XFont("Arial", 10, XFontStyle.Regular);
                var fuenteTitulo = new XFont("Arial", 14, XFontStyle.Bold);
                var fuenteEncabezado = new XFont("Arial", 12, XFontStyle.Bold);
                var fuenteTexto = new XFont("Arial", 12, XFontStyle.Regular);
                var fuenteDescripcion = new XFont("Arial", 10, XFontStyle.Italic);

                int margenX = 50;
                int margenY = 40;
                int anchoTabla = (int)pagina.Width - 2 * margenX;
                int altoFila = 30;
                int columna1 = margenX;

                try
                {
                    string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/logo-pdf.png");
                    if (System.IO.File.Exists(logoPath))
                    {
                        XImage logo = XImage.FromFile(logoPath);
                        gfx.DrawImage(logo, margenX, margenY, 80, 60);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar el logo: {ex.Message}");
                }

                margenY += 70;
                gfx.DrawString($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy}", fuenteFecha, XBrushes.Black, new XPoint(pagina.Width - 150, margenY));

                margenY += 20;

                // **Nombre del Proveedor**
                gfx.DrawString($"Proveedor: {proveedorNombre}", fuenteTitulo, XBrushes.Black, new XPoint(margenX, margenY));
                margenY += 20;

                // **Encabezado informativo**
                gfx.DrawString("Reporte de Pedidos", fuenteTitulo, XBrushes.Black, new XPoint(margenX, margenY));
                margenY += 20;
                gfx.DrawString("Este documento contiene información detallada sobre los productos a ser recibidos por los proveedores",
                    fuenteDescripcion, XBrushes.Black, new XPoint(margenX, margenY));
                margenY += 26;

                // **Dibujar la tabla con encabezados una sola vez**
                XPen pen = new XPen(XColors.Black, 1);

                string[] encabezados = { "Producto", "Cantidad", "Observaciones" };

                int xActual = columna1;
                for (int i = 0; i < encabezados.Length; i++)
                {
                    gfx.DrawRectangle(pen, xActual, margenY, anchosColumnas[i], altoFila);
                    gfx.DrawString(encabezados[i], fuenteEncabezado, XBrushes.Black, new XPoint(xActual + 10, margenY + 20));
                    xActual += anchosColumnas[i];
                }

                margenY += altoFila;

                foreach (var detalle in detallesViewModel)
                {
                    string[] valores =
                    {
        detalle.nombreProducto,
        detalle.Cantidad.ToString(),
        detalle.Observaciones ?? "Sin observaciones"
    };

                    xActual = columna1;
                    for (int i = 0; i < valores.Length; i++)
                    {
                        gfx.DrawRectangle(pen, xActual, margenY, anchosColumnas[i], altoFila);
                        gfx.DrawString(valores[i], fuenteTexto, XBrushes.Black, new XPoint(xActual + 10, margenY + 20));
                        xActual += anchosColumnas[i];
                    }

                    margenY += altoFila;
                }

                documento.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", $"Reporte_{id}.pdf");
            }
        }


    }

}
