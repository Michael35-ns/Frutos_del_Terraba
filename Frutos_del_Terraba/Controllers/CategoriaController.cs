using Frutos_del_Terraba.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Frutos_del_Terraba.Controllers
{
    public class CategoriaController : Controller

    {
        protected string apiUrl = "https://localhost:7240/api/categoria";
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categorias = JsonSerializer.Deserialize<List<Categoria>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var viewModel = new CategoriaViewModel
                {
                    Categorias = categorias,
                    NuevaCategoria = new Categoria()
                };
                return View(viewModel);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = "Error al obtener categorías." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var json = JsonSerializer.Serialize(model.NuevaCategoria);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear la categoría");
                }
            }
            var reloadClient = _httpClientFactory.CreateClient();
            var reloadResponse = await reloadClient.GetAsync(apiUrl);
            if (reloadResponse.IsSuccessStatusCode)
            {
                var reloadJson = await reloadResponse.Content.ReadAsStringAsync();
                model.Categorias = JsonSerializer.Deserialize<List<Categoria>>(reloadJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return View("Error", new ErrorViewModel { RequestId = $"Error al eliminar la categoría. Detalle: {errorMessage}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categoria = JsonSerializer.Deserialize<Categoria>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(categoria);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(categoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{apiUrl}/{categoria.Id_categoria}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }
    }
}
   