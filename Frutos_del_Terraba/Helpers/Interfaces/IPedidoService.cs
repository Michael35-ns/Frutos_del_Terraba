using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidosAsync();
        Task<PedidoDTOModel> CrearPedidoAsync(PedidoDTOModel pedido);

        Task<IEnumerable<ProveedorDTOModel>> ObtenerTodosProveedoresAsync();




    }
}
