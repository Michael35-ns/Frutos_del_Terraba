using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Servicios.Implementaciones
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Listar todas las Categorias
        public async Task<IEnumerable<Categoria>> ObtenerTodasCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }
        #endregion


        #region Obtener Catgoria por Id
        public async Task<Categoria?> ObtenerCategoriaPorId(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }
        #endregion


        #region Cear una nueva Categoria
        public async Task<Categoria> CrearCategoria(CategoriaDTOModel model)
        {
            var categoria = new Categoria
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        #endregion


        #region Actualizar Categoria
        public async Task<bool> ActualizarCategoria(int id, CategoriaDTOModel model)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion


        #region Eliminar Categoria
        public async Task<bool> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}