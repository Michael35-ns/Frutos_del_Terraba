using Frutos_del_Terraba_Api.Models;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> ObtenerPedidos();

    }
}
