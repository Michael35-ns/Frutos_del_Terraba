using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Frutos_del_Terraba_Api.Controllers
{
    [ApiController]
    [Route("api/frutosterraba/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(
            UserManager<IdentityUser> userManager,
            ILogger<UsuarioController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        #region Obtener usuario por correo
        [HttpGet("por-correo/{correo}")]
        public async Task<IActionResult> GetUsuarioPorCorreo(string correo)
        {
            if (string.IsNullOrEmpty(correo))
            {
                return BadRequest(new { message = "El correo es requerido" });
            }

            var usuario = await _userManager.FindByEmailAsync(correo);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new
            {
                Id = usuario.Id,
                Email = usuario.Email,
                UserName = usuario.UserName
            });
        }
        #endregion
    }
}