using Frutos_del_Terraba.Models;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoViewModel>> ObtenerTodosProductosAsync();
        Task<ProductoViewModel?> ObtenerProductoPorIdAsync(int id);
        Task<ProductoViewModel> CrearProductoAsync(ProductoViewModel producto);
        Task<ProductoViewModel> ActualizarProductoAsync(int id, ProductoViewModel producto);
        Task<bool> EliminarProductoAsync(int id);
    }
}
