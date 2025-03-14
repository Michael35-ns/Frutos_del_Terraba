namespace Frutos_del_Terraba.Models
{
    public class HistorialProveedorViewModel
    {
        public ProveedorViewModel Proveedor { get; set; }
        public List<PedidoDetalladoViewModel> HistorialPedidos { get; set; }
    }

    public class PedidoDetalladoViewModel
    {
        public int Id_pedido { get; set; }
        public DateTime Fecha { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public List<DetallesPedidoViewModel> DetallesPedidos { get; set; }
    }
}
