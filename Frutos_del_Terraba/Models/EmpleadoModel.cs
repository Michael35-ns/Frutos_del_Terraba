using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class EmpleadoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe asignar un rol.")]
        public string Role { get; set; }
    }
}
