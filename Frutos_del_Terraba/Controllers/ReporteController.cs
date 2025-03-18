using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http.Json;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;


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

            using (var stream = new MemoryStream())
            {
                var documento = new PdfDocument();
                var pagina = documento.AddPage();
                var gfx = XGraphics.FromPdfPage(pagina);

                // **Fuentes**
                var fuenteFecha = new XFont("Arial", 10, XFontStyle.Regular);
                var fuenteTitulo = new XFont("Arial", 14, XFontStyle.Bold);
                var fuenteEncabezado = new XFont("Arial", 12, XFontStyle.Bold);
                var fuenteTexto = new XFont("Arial", 12, XFontStyle.Regular);
                var fuenteDescripcion = new XFont("Arial", 10, XFontStyle.Italic);

                // **Margen y dimensiones**
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

                // **Encabezado informativo**
                gfx.DrawString("Reporte de Incidencias en Productos", fuenteTitulo, XBrushes.Black, new XPoint(margenX, margenY));
                margenY += 20;
                gfx.DrawString("Este documento contiene información detallada sobre las incidencias reportadas en los productos.",
                    fuenteDescripcion, XBrushes.Black, new XPoint(margenX, margenY));
                margenY += 26; 


                // **Dibujar la tabla con encabezados**
                XPen pen = new XPen(XColors.Black, 1);

                string[] encabezados = { "ID del Reporte", "Producto", "Cantidad", "Motivo", "Fecha" };
                string[] valores =
                {
                    reporte.Id_reporte.ToString(),
                    reporte.Producto?.Nombre ?? "Sin nombre",
                    reporte.Cantidad_reportada.ToString(),
                    reporte.Motivo,
                     reporte.Fecha.ToString("dd/MM/yyyy")
                };

                for (int i = 0; i < encabezados.Length; i++)
                {
                    gfx.DrawRectangle(pen, columna1 + (i * 100), margenY, 100, altoFila);
                    gfx.DrawString(encabezados[i], fuenteEncabezado, XBrushes.Black, new XPoint(columna1 + (i * 100) + 10, margenY + 20));
                }

                margenY += altoFila;

                for (int i = 0; i < valores.Length; i++)
                {
                    gfx.DrawRectangle(pen, columna1 + (i * 100), margenY, 100, altoFila);
                    gfx.DrawString(valores[i], fuenteTexto, XBrushes.Black, new XPoint(columna1 + (i * 100) + 10, margenY + 20));
                }

                documento.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", $"Reporte_{reporte.Id_reporte}.pdf");
            }
        }
      
        #endregion
    }
}