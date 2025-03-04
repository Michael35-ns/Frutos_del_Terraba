using Frutos_del_Terraba.Models;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> ObtenerPedidosAsync();

    }
}
