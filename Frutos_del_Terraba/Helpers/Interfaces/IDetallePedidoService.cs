using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IDetallePedidoService
    {
        Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id);
        Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel dettales);
    }
}
