using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IDetallesPedido
    {
        Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id);
    }
}
