using Frutos_del_Terraba_Api.DTO;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba.Models
{
    public class PedidoViewModel
    {
        public int Id_pedido { get; set; }

        public String Id_usuario { get; set; } 

        public bool Estado {  get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public int Id_proveedor { get; set; }

        public List<PedidoDTOModel> Pedido { get; set; }
        public List<ProveedorDTOModel> Proveedores { get; set; }

        public PedidoDTOModel NuevoPedido { get; set; } = new PedidoDTOModel();
    }
}
