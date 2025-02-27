using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;

namespace Frutos_del_Terraba_Api.Servicios.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObtenerTodasCategorias();
        Task<Categoria?> ObtenerCategoriaPorId(int id);
        Task<Categoria> CrearCategoria(CategoriaDTOModel model);
        Task<bool> ActualizarCategoria(int id, CategoriaDTOModel model);
        Task<bool> EliminarCategoria(int id);
    }
}
