using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
    }
}
