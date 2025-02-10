using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Models;
using System.IdentityModel.Tokens.Jwt;

public class AuthController : Controller
{
   protected string apiUrl = "https://localhost:7187/api/auth";

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

    public IActionResult Login()
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
        var response = await httpClient.PostAsync($"{apiUrl}/register", content);

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

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var loginModel = new
        {
            Email = model.Email,
            Password = model.Password
        };

        var json = JsonSerializer.Serialize(loginModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsync($"{apiUrl}/login", content);

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(tokenResponse);
            var token = jsonDocument.RootElement.GetProperty("token").GetString();
            HttpContext.Session.SetString("Token", token);

            var handler = new JwtSecurityTokenHandler();
            Console.WriteLine($"Received Token: {token}");
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Token is empty or null.");
            }

            if (!handler.CanReadToken(token))
            {
                return BadRequest("Invalid token format.");
            }

            var jwtToken = handler.ReadJwtToken(token);

            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;

            if (!string.IsNullOrEmpty(email))
            { 
                HttpContext.Session.SetString("Email", email);
            }

            return RedirectToAction("Index", "Dashboards");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", "Error en el login: " + errorMessage);
            return View(model);
        }
    }

    public IActionResult Confirmacion()
    {
        return View();
    }
}

