using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frutos_del_Terraba.Models;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
    public IActionResult Index()
    {
        return View(); // No pases un layout manualmente aquí
    }
}
