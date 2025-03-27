using Frutos_del_Terraba_Api.DTO;


namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidosAsync();
        Task<PedidoDTOModel> CrearPedidoAsync(PedidoDTOModel pedido);
        Task<PedidoDTOModel> ObtenerPedidoPorIdAsync(int id);
        Task<bool> ActualizarPedidoAsync(int id, PedidoDTOModel pedido);
        Task<bool> EliminarPedidoAsync(int id);

    }
}
