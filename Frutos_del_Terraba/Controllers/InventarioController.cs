using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Frutos_del_Terraba.Controllers
{
    public class InventarioController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7240/api");
        private readonly HttpClient _client;

        public InventarioController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            List<InventarioViewModel> inventarioList = new List<InventarioViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Inventario/GetInventario").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                inventarioList = JsonSerializer.Deserialize<List<InventarioViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return View(inventarioList);
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
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Inventario/GetInventario/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var inventario = JsonSerializer.Deserialize<InventarioViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(inventario);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id_inventario, Cantidad")] InventarioViewModel inventario)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(inventario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_client.BaseAddress}/Inventario/ActualizarInventario/{inventario.Id_inventario}";
                HttpResponseMessage response = await _client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Inventario actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el inventario.");
                    TempData["ErrorMessage"] = "Error al actualizar el inventario. Inténtalo de nuevo.";
                    return View(inventario);
                }
            }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                TempData["ErrorMessage"] = "El modelo es inválido. Errores: " + string.Join(", ", errors);
                return View(inventario);
          
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Inventario/EliminarInventario/{id}");

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

        #region Despachado
        [HttpGet]
        public IActionResult Despachado()
        {
            List<DetallesDistribucionViewModel> inventarioDespachadoList = new List<DetallesDistribucionViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Inventario/GetInventarioDespachado").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                inventarioDespachadoList = JsonSerializer.Deserialize<List<DetallesDistribucionViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return View(inventarioDespachadoList);
        }
        #endregion
    }
}
