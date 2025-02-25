using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frutos_del_Terraba.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
    public IActionResult Index()
    {
        /*var token = HttpContext.Session.GetString("token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth");
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var username = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

        ViewData["Username"] = username;*/
        return View(); 
    }
}
