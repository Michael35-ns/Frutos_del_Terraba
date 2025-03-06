using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Frutos_del_Terraba.Controllers
{
    public class ProveedorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7240/api");
        private readonly HttpClient _client;

        public ProveedorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProveedorViewModel> proveedoresList = new List<ProveedorViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Proveedor/GetProveedores").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                proveedoresList = JsonSerializer.Deserialize<List<ProveedorViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            return View(proveedoresList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProveedorViewModel proveedor)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(proveedor);
                Console.WriteLine(json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Proveedor/CrearProveedor", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Proveedor creado exitosamente.";
                    return RedirectToAction("Index"); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el proveedor.");
                    TempData["ErrorMessage"] = "Error al crear el proveedor. Inténtalo de nuevo.";
                    return View(proveedor);
                }

            }

            TempData["ErrorMessage"] = "Error en la validacion del formulario.";
            return View(proveedor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Proveedor/GetProveedor/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var proveedor = JsonSerializer.Deserialize<ProveedorViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(proveedor);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProveedorViewModel proveedor)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(proveedor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_client.BaseAddress}/Proveedor/ActualizarProveedor/{proveedor.Id_proveedor}";
                HttpResponseMessage response = await _client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Proveedor actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el proveedor.");
                    TempData["ErrorMessage"] = "Error al actualizar el proveedor. Inténtalo de nuevo.";
                    return View(proveedor);
                }
            }
            return View(proveedor);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + $"/Proveedor/EliminarProveedor/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }


    }
}
