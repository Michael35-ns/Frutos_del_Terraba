using Frutos_del_Terraba_Api.DTO;


namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidos();
        Task<PedidoDTOModel> CrearPedidoAsync(PedidoDTOModel pedido);

    }
}
