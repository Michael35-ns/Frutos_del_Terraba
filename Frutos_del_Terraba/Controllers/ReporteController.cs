using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.Json;
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;




namespace Frutos_del_Terraba.Controllers
{
    public class ReporteController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7240/api");
        private readonly HttpClient _client = new();
        private readonly IProductoService _productoService;

        public ReporteController(IProductoService productoService)
        {
            _client.BaseAddress = baseAddress;
            _productoService = productoService;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var reportesList = new List<ReporteViewModel>();

            var response = _client.GetAsync(_client.BaseAddress + "/Reporte/GetReportes").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                reportesList = JsonSerializer.Deserialize<List<ReporteViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }

            return View(reportesList);
        }
        #endregion



        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productos = (await _productoService.ObtenerTodosProductosAsync())?.ToList() ?? [];

            if (productos.Count == 0)

            {
                productos.Add(new() { IdProducto = 0, Nombre = "No hay productos disponibles" });
            }

            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ReporteViewModel reporte)
        {
            var json = JsonSerializer.Serialize(reporte, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Reporte/CrearReporte", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Reporte creado exitosamente.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al crear el reporte. Inténtalo de nuevo.";

            var productos = await _productoService.ObtenerTodosProductosAsync();
            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");

            return View(reporte);
        }
        #endregion


        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Reporte/GetReporte/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reporte = JsonSerializer.Deserialize<ReporteViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (reporte == null)
                {
                    return NotFound();
                }
                var productos = (await _productoService.ObtenerTodosProductosAsync())?.ToList() ?? [];


                if (productos.Count == 0)
                {
                    productos.Add(new ProductoViewModel { IdProducto = 0, Nombre = "No hay productos disponibles" });
                }

                ViewBag.Productos = productos.Select(p => new SelectListItem
                {
                    Value = p.IdProducto.ToString(),
                    Text = p.Nombre
                }).ToList();

                return View(reporte);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReporteViewModel reporte)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(reporte);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_client.BaseAddress}/Reporte/ActualizarReporte/{reporte.Id_reporte}";
                HttpResponseMessage response = await _client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Reporte actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el reporte.");
                    TempData["ErrorMessage"] = "Error al actualizar el reporte. Inténtalo de nuevo.";
                    return View(reporte);
                }
            }
            return View(reporte);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Reporte/EliminarReporte/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion


        #region Descarga PDF
        public async Task<IActionResult> DownloadPDF(int id)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Reporte/GetReporte/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "No se pudo obtener el reporte.";
                return RedirectToAction("Index");
            }

            var json = await response.Content.ReadAsStringAsync();
            var reporte = JsonSerializer.Deserialize<ReporteViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (reporte == null)
            {
                TempData["ErrorMessage"] = "El reporte no existe.";
                return RedirectToAction("Index");
            }

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.Background("#FFFFFF");

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            row.RelativeColumn(1).Height(60).Image("wwwroot/img/logo-pdf.png", ImageScaling.FitHeight);
                            row.RelativeColumn(4).AlignCenter().Column(col =>
                            {
                                col.Item().Text("FRUTOS DEL TÉRRABA").FontSize(14).Bold();
                                col.Item().Text("REPORTE DE INCIDENCIA EN PRODUCTOS").FontSize(12).SemiBold();
                                col.Item().Text($"Reporte generado el {DateTime.Now:dd/MM/yyyy}").FontSize(10).Italic();
                            });
                            row.RelativeColumn(1).AlignRight().Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                .FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                        header.Item().LineHorizontal(1).LineColor("#8B5E3C");
                    });

                    // Tabla
                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            string bgColor = "#9DD295";
                            string textColor = "#5C4033";
                            float fontSize = 10;

                            header.Cell().Background(bgColor).Padding(5).Text("ID REPORTE").Bold().FontSize(fontSize).FontColor(textColor);
                            header.Cell().Background(bgColor).Padding(5).Text("PRODUCTO").Bold().FontSize(fontSize).FontColor(textColor);
                            header.Cell().Background(bgColor).Padding(5).Text("CANTIDAD").Bold().FontSize(fontSize).FontColor(textColor);
                            header.Cell().Background(bgColor).Padding(5).Text("MOTIVO").Bold().FontSize(fontSize).FontColor(textColor);
                            header.Cell().Background(bgColor).Padding(5).Text("FECHA").Bold().FontSize(fontSize).FontColor(textColor);
                        });

                        table.Cell().Padding(5).Text(reporte.Id_reporte.ToString()).FontSize(10);
                        table.Cell().Padding(5).Text(reporte.Producto?.Nombre ?? "Sin nombre").FontSize(10);
                        table.Cell().Padding(5).Text(reporte.Cantidad_reportada.ToString()).FontSize(10);
                        table.Cell().Padding(5).Text(reporte.Motivo).FontSize(10);
                        table.Cell().Padding(5).Text(reporte.Fecha.ToString("dd/MM/yyyy")).FontSize(10);
                    });

                    // Pie de página
                    page.Footer().AlignCenter().Text("FRUTOS DEL TÉRRABA").FontSize(10).FontColor("#5C4033");
                });
            });

            var pdfBytes = documento.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Reporte_{reporte.Id_reporte}.pdf");
        }

        #endregion
    }
}