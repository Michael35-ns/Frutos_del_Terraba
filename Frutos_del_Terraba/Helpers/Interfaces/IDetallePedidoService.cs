using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IDetallePedidoService
    {
        Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel dettales);
        Task<DetallesPedidoDTOModel> ActualizarDetallePedido(int id, DetallesPedidoDTOModel detalles);
        Task<List<DetallesPedidoDTOModel>> ObtenerDetallesPedido(int id);
        Task<DetallesPedidoDTOModel> ObtenerDetallesPedidoId(int id);
        Task<bool> EliminarDetallesPedido(int id);
    }
}
