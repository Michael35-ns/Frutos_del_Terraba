using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba_Api.DTO;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Frutos_del_Terraba.Controllers
{
    public class DistribucionController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7240/api");
        private readonly HttpClient _client;
        private const string PRODUCTOS_DISTRIBUCION_SESSION_KEY = "ProductosDistribucion";

        public DistribucionController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            List<DistribucionViewModel> distribucionList = new List<DistribucionViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Distribucion/GetDistribuciones").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                distribucionList = JsonSerializer.Deserialize<List<DistribucionViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return View(distribucionList);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var inventarioList = await GetInventarioList();

            var productosDistribucion = HttpContext.Session
                .GetObjectFromJson<List<DetallesDistribucionViewModel>>(PRODUCTOS_DISTRIBUCION_SESSION_KEY) 
                ?? new List<DetallesDistribucionViewModel>();

            var viewModel = new DistribucionViewModel
            {
                DetallesDistribuciones = productosDistribucion
            };

            ViewBag.InventarioList = inventarioList;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DistribucionViewModel model)
        {
            try
            {
                var detalles = HttpContext.Session
                    .GetObjectFromJson<List<DetallesDistribucionViewModel>>(PRODUCTOS_DISTRIBUCION_SESSION_KEY)
                    ?? new List<DetallesDistribucionViewModel>();

                if (detalles.Count == 0)
                {
                    TempData["ErrorMessage"] = "Debe agregar al menos un producto a la distribucion";
                    return View(model);
                }

                var distribucion = new DistribucionDTOModel
                {
                    Destino = model.Destino,
                    Ubicacion = model.Ubicacion,
                    Observaciones = model.Observaciones,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    DetallesDistribuciones = detalles.Select(d => new DetallesDistribucionDTOModel
                    {
                        Cantidad = d.Cantidad,
                        Id_inventario = d.Id_inventario
                    }).ToList() 
                };

                var json = JsonSerializer.Serialize(distribucion);
                Console.WriteLine(json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Distribucion/PostDistribucion", content);

                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.Remove(PRODUCTOS_DISTRIBUCION_SESSION_KEY);
                    TempData["SuccessMessage"] = "Distribucion creada exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Error al crear la distribucion: {error}";
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error del try");
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        #endregion

        #region Show
        [HttpGet]
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Distribucion/GetDistribucion/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<DistribucionViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(resultado);
            }

            return NotFound();
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Distribucion/DeleteDistribucion/{id}");

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

        private async Task<List<InventarioViewModel>> GetInventarioList()
        {
            List<InventarioViewModel> inventarioList = new List<InventarioViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Inventario/GetInventario").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                return inventarioList = JsonSerializer.Deserialize<List<InventarioViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return new List<InventarioViewModel>();
        }

        [HttpPost]
        public IActionResult AddProductToDistribution(int inventarioID, string nombre, string categoria, int cantidad, int stock)
        {
            if (cantidad <= 0)
            {
                TempData["ErrorMessage"] = "La cantidad debe ser mayor que cero";
                return RedirectToAction(nameof(Create));
            }

            var distribucionProductos = HttpContext.Session
                .GetObjectFromJson<List<DetallesDistribucionViewModel>>(PRODUCTOS_DISTRIBUCION_SESSION_KEY)
                ?? new List<DetallesDistribucionViewModel>();

            var existingProduct = distribucionProductos
                .FirstOrDefault(p => p.Id_inventario == inventarioID);

            if (existingProduct != null)
            {
                var cantidadTemp = existingProduct.Cantidad + cantidad;

                if (cantidadTemp > existingProduct.Stock)
                {
                    var stockRestante = existingProduct.Stock - existingProduct.Cantidad;
                    TempData["ErrorMessage"] = $"La cantidad no puede exceder el stock disponible, puedes intentar con un numero menor o igual a {stockRestante}, que es la cantidad faltante para alcanzar el maximo del inventario";
                    return RedirectToAction(nameof(Create));
                }

                existingProduct.Cantidad += cantidad;
            }
            else
            {
                distribucionProductos.Add(new DetallesDistribucionViewModel
                {
                    Cantidad = cantidad,
                    Stock = stock,
                    Nombre = nombre,
                    Categoria = categoria,
                    Id_inventario = inventarioID,
                });
            }

            HttpContext.Session.SetObjectAsJson(PRODUCTOS_DISTRIBUCION_SESSION_KEY, distribucionProductos);

            TempData["SuccessMessage"] = $"Se agregaron {cantidad} unidades de {nombre} a la distribucion";

            return RedirectToAction(nameof(Create));
        }

        [HttpPost]
        public IActionResult EditProductToDistribution(int inventarioID, int cantidad)
        {
            if (cantidad <= 0)
            {
                TempData["ErrorMessage"] = "La cantidad debe ser mayor que cero";
                return RedirectToAction(nameof(Create));
            }

            var distribucionProductos = HttpContext.Session
                .GetObjectFromJson<List<DetallesDistribucionViewModel>>(PRODUCTOS_DISTRIBUCION_SESSION_KEY)
                ?? new List<DetallesDistribucionViewModel>();

            var existingProduct = distribucionProductos
                .FirstOrDefault(p => p.Id_inventario == inventarioID);

            if (existingProduct != null)
            {
                existingProduct.Cantidad = cantidad;

                HttpContext.Session.SetObjectAsJson(PRODUCTOS_DISTRIBUCION_SESSION_KEY, distribucionProductos);

                TempData["SuccessMessage"] = $"Se actualizo a {cantidad} unidades de {existingProduct.Nombre} en la distribucion";
            }

            return RedirectToAction(nameof(Create));
        }

        [HttpPost]
        public IActionResult DeleteProductToDistribution(int id)
        {
            var distribucionProductos = HttpContext.Session
                .GetObjectFromJson<List<DetallesDistribucionViewModel>>(PRODUCTOS_DISTRIBUCION_SESSION_KEY)
                ?? new List<DetallesDistribucionViewModel>();

            var existingProduct = distribucionProductos
                .FirstOrDefault(p => p.Id_inventario == id);

            if (existingProduct != null)
            {
                distribucionProductos.Remove(existingProduct);

                HttpContext.Session.SetObjectAsJson(PRODUCTOS_DISTRIBUCION_SESSION_KEY, distribucionProductos);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> encontrarProductoInventario(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Inventario/GetInventario/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var inventario = JsonSerializer.Deserialize<InventarioViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(inventario);
            }

            return NotFound();
        }
    
    }
}
