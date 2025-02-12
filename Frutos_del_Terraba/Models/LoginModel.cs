using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "Debe tener al menos 6 caracteres.")]
        public string Password { get; set; }
    }
}
