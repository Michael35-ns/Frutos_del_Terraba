using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IDetallesPedidoService
    {
        Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id);
        Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel dettales);
        Task<DetallesPedidoDTOModel> ObtenerDetallesPedidoPorId(int id);
        Task<bool> ActualizarDetallesPedido(int id, DetallesPedidoDTOModel detalles);
        Task<bool> EliminarDetallesPedido(int id);

    }
}
