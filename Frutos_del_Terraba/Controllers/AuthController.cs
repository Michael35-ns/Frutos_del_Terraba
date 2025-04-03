using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Frutos_del_Terraba.Models;
using System.IdentityModel.Tokens.Jwt;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/login", content);

            if (response.IsSuccessStatusCode)
            {
                HttpResponseMessage response2 = await _client.GetAsync($"https://localhost:7240/api/frutosterraba/Usuario/por-correo/{loginModel.email}");

                var responseContent = await response2.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UsuarioViewModel>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Id))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginModel.email), // Include user's email as Name claim
                        new Claim(ClaimTypes.NameIdentifier, userInfo.Id)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
                    return RedirectToAction("Index", "Dashboards");
                }
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


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth"); // Esto cierra la sesión

        // Redirige a la página de login después de cerrar la sesión
        return RedirectToAction("Login", "Auth");
    }


    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public IActionResult Confirmacion()
    {
        return View();
    }

}
