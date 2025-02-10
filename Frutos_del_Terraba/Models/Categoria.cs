using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

}
