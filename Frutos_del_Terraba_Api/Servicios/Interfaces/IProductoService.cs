using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTOModel>> ObtenerTodosProductoAsync();
        Task<ProductoDTOModel> ObtenerProductoPorIdAsync(int id);
        Task<ProductoDTOModel> CrearProductoAsync(ProductoDTOModel productoDto);
        Task<bool> ActualizarProductoAsync(int id, ProductoDTOModel productoDto);
        Task<bool> EliminarProductoAsync(int id);
    }
}
