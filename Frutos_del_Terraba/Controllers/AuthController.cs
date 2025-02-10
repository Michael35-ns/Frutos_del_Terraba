using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Models;

public class AuthController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var registerModel = new
        {
            Email = model.Email,
            Password = model.Password,
            UserName = model.UserName
        };

        var json = JsonSerializer.Serialize(registerModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsync("https://localhost:7187/api/auth/register", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Confirmacion");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", "Error en el registro: " + errorMessage);
            return View(model);
        }
    }

    public IActionResult Confirmacion()
    {
        return View();
    }
}

