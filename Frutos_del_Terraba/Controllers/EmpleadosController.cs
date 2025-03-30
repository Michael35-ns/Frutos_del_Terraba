using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EmpleadosController : Controller
{
    private readonly HttpClient _client;
    private readonly string _apiUrl = "https://localhost:7240/api/empleados";

    public EmpleadosController()
    {
        _client = new HttpClient();
    }

    public async Task<IActionResult> Index()
    {
        HttpResponseMessage response = await _client.GetAsync(_apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var empleados = JsonSerializer.Deserialize<List<EmpleadoModel>>(await response.Content.ReadAsStringAsync());
            return View(empleados);
        }
        return View(new List<EmpleadoModel>());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmpleadoModel empleado)
    {
        if (ModelState.IsValid)
        {
            var json = JsonSerializer.Serialize(empleado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }
        return View(empleado);
    }

    public async Task<IActionResult> Edit(int id)
    {
        HttpResponseMessage response = await _client.GetAsync(_apiUrl + $"/{id}");
        if (response.IsSuccessStatusCode)
        {
            var empleado = JsonSerializer.Deserialize<EmpleadoModel>(await response.Content.ReadAsStringAsync());
            return View(empleado);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmpleadoModel empleado)
    {
        if (ModelState.IsValid)
        {
            var json = JsonSerializer.Serialize(empleado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(_apiUrl + $"/{empleado.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }
        return View(empleado);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _client.DeleteAsync(_apiUrl + $"/{id}");
        return RedirectToAction("Index");
    }
}
