namespace Frutos_del_Terraba_Api.DTO
{
    public class PedidoRequestDTO
    {
        public PedidoDTOModel Pedido { get; set; }
        public List<DetallesPedidoDTOModel> Detalles { get; set; }
    }
}
