using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.Background("#FFFFFF");

                    // Header
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            row.RelativeColumn(1).Height(60).Image("wwwroot/img/logo-pdf.png", ImageScaling.FitHeight);
                            row.RelativeColumn(4).AlignCenter().Column(col =>
                            {
                                col.Item().Text("FRUTOS DEL TÉRRABA").FontSize(14).Bold();
                                col.Item().Text("REPORTE DE PEDIDO").FontSize(12).SemiBold();
                                col.Item().Text($"Generado el {DateTime.Now:dd/MM/yyyy}").FontSize(10).Italic();
                            });
                        });
                        header.Item().LineHorizontal(1).LineColor("#8B5E3C");
                    });

                    // Contenido principal
                    page.Content().PaddingVertical(10).Column(content =>
                    {
                        content.Item().Text($"Proveedor: {proveedorNombre}").FontSize(12).Bold().FontColor("#5C4033");
                        content.Item().Text("Este documento contiene información detallada sobre los productos solicitados.").FontSize(10).Italic();

                        content.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Producto
                                columns.RelativeColumn(1); // Cantidad
                                columns.RelativeColumn(4); // Observaciones
                            });

                            string bgColor = "#9DD295";
                            string textColor = "#5C4033";
                            float fontSize = 10;

                            table.Header(header =>
                            {
                                header.Cell().Background(bgColor).Padding(5).Text("PRODUCTO").Bold().FontSize(fontSize).FontColor(textColor);
                                header.Cell().Background(bgColor).Padding(5).Text("CANTIDAD").Bold().FontSize(fontSize).FontColor(textColor);
                                header.Cell().Background(bgColor).Padding(5).Text("OBSERVACIONES").Bold().FontSize(fontSize).FontColor(textColor);
                            });

                            foreach (var item in detallesViewModel)
                            {
                                table.Cell().Padding(5).Text(item.nombreProducto).FontSize(10);
                                table.Cell().Padding(5).Text(item.Cantidad.ToString()).FontSize(10);
                                table.Cell().Padding(5).Text(item.Observaciones ?? "Sin observaciones").FontSize(10);
                            }
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Text("FRUTOS DEL TÉRRABA").FontSize(10).FontColor("#5C4033");
                });
            });

            var pdfBytes = documento.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Reporte_Pedido_{id}.pdf");
        }


    }

}
