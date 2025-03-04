using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Models;
using System.IdentityModel.Tokens.Jwt;
using Frutos_del_Terraba_Api.Models;

public class AuthController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7240/identity");
    private readonly HttpClient _client;

    public AuthController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (ModelState.IsValid)
        {
            var json = JsonSerializer.Serialize(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/register", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Confirmacion");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Error en el registro: " + errorMessage);
                return View(registerModel);
            }
        }
        return View(registerModel);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/login", content).Result;

            if (response.IsSuccessStatusCode)
            {

                var tokenResponse = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(tokenResponse);

                if (!jsonDocument.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                {
                    ModelState.AddModelError("", "La respuesta de la API no contiene un token.");
                    return View(loginModel);
                }

                var token = tokenElement.GetString();

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
                return View(loginModel);
            }
        }
        return View(loginModel);
    }

    public IActionResult Confirmacion()
    {
        return View();
    }
}
