using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidosAsync();
        Task<IEnumerable<ProveedorDTOModel>> ObtenerTodosProveedoresAsync();
        Task<PedidoDTOModel> ObtenerPedidoPorIdAsync(int id);
        Task<PedidoDTOModel> ActualizarPedidoAsync(int id, PedidoDTOModel pedido);
        Task<PedidoDTOModel?> CrearPedidoAsync(PedidoDTOModel pedido);
        Task<bool> EliminarPedidoAsync(int id);
    }
}