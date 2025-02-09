using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frutos_del_Terraba.Models;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();
}
