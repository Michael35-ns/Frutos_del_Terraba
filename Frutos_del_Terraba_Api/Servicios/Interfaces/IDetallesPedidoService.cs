using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IDetallesPedidoService
    {
        Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id);
        Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel dettales);

    }
}
