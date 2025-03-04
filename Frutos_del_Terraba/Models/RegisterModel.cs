using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "Debe tener al menos 6 caracteres.")]
        public string password { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string userName { get; set; }
    }
}
