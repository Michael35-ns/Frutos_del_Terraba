using Frutos_del_Terraba.Models;

namespace Frutos_del_Terraba.Helpers.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObtenerTodasCategoriasAsync();
        Task<Categoria?> ObtenerCategoriaPorIdAsync(int id);
        Task<Categoria> CrearCategoriaAsync(Categoria categoria);
        Task<Categoria> ActualizarCategoriaAsync(int id, Categoria categoria);
        Task<bool> EliminarCategoriaAsync(int id);
    }
}
