using Frutos_del_Terraba_Api.Controllers;
using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Servicios.Implementaciones
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Actualizar Producto
        public async Task<bool> ActualizarProductoAsync(int id, ProductoDTOModel productoDto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            producto.Nombre = productoDto.Nombre;
            producto.Stock = productoDto.Stock;
            producto.Id_categoria = productoDto.Id_categoria;

            await _context.SaveChangesAsync(); 
            return true;
        }

        #endregion


        #region Crear Producto
        public async Task<ProductoDTOModel> CrearProductoAsync(ProductoDTOModel productoDto)
        {
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Stock = productoDto.Stock,
                Id_categoria = productoDto.Id_categoria
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return new ProductoDTOModel
            {
                IdProducto = producto.Id_producto, 
                Nombre = producto.Nombre,
                Stock = producto.Stock,
                Id_categoria = producto.Id_categoria
            };
        }

        #endregion


        #region Eliminar Producto
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        #region Obtener Producto por Id
        public async Task<ProductoDTOModel> ObtenerProductoPorIdAsync(int id)
        {
            var producto = await _context.Productos
        .Include(p => p.Categoria)
        .FirstOrDefaultAsync(p => p.Id_producto == id);

            if (producto == null)
                throw new KeyNotFoundException($"No se encontró el producto con ID {id}.");

            return new ProductoDTOModel
            {
                IdProducto = producto.Id_producto,
                Nombre = producto.Nombre,
                Stock = producto.Stock,
                Id_categoria = producto.Id_categoria
            };
        }

        #endregion


        #region Obtener todos los productos

        public async Task<IEnumerable<ProductoDTOModel>> ObtenerTodosProductoAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();

            return productos.Select(p => new ProductoDTOModel
            {
                IdProducto = p.Id_producto,
                Nombre = p.Nombre,
                Stock = p.Stock,
                Id_categoria = p.Id_categoria
            }).ToList();
        }

        #endregion
    }
}
