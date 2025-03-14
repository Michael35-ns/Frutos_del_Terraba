using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Obtener todo el Inventario
        [HttpGet]
        public async Task<IActionResult> GetInventario()
        {
            try
            {
                var inventario = await _context.Inventarios
                    .Include(i => i.Producto)
                        .ThenInclude(p => p.Categoria)
                    .Select(i => new
                    {
                        i.Id_inventario,
                        i.Cantidad,
                        i.Id_producto,
                        Producto = new
                        {
                            i.Producto.Id_producto,
                            i.Producto.Nombre,
                            i.Producto.Id_categoria,
                            Categoria = new
                            {
                                i.Producto.Categoria.Id_categoria,
                                i.Producto.Categoria.Nombre
                            }
                        }
                    })
                    .ToListAsync();

                return inventario.Any() ? Ok(inventario) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener el inventario.", error = ex.Message });
            }
        }
        #endregion

        #region Obtener Inventario por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventario(int id)
        {
            try
            {
                var inventario = await _context.Inventarios
                    .Where(i => i.Id_inventario == id)
                    .Include(i => i.Producto)
                        .ThenInclude(p => p.Categoria)
                    .Select(i => new {
                        i.Id_inventario,
                        i.Cantidad,
                        i.Id_producto,
                        Producto = new
                        {
                            i.Producto.Id_producto,
                            i.Producto.Nombre,
                            i.Producto.Id_categoria,
                            Categoria = new
                            {
                                i.Producto.Categoria.Id_categoria,
                                i.Producto.Categoria.Nombre
                            }
                        }
                    })
                    .FirstOrDefaultAsync(); 

                if (inventario != null)
                    return Ok(inventario);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener el inventario por id.", error = ex.Message });
            }
        }
        #endregion

        #region Actualizar Inventario
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarInventario(int id, [FromBody] InventarioDTOModel model)
        {
            if (id != model.Id_inventario)
                return BadRequest(new { message = "El ID del inventario no coincide" });

            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
                return NotFound(new { message = "Inventario no encontrado" });

            inventario.Cantidad = model.Cantidad;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Inventario actualizado exitosamente", inventario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al actualizar el inventario", error = ex.Message });
            }
        }
        #endregion

        #region Eliminar Inventario
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
                return NotFound();
            _context.Inventarios.Remove(inventario);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Inventario eliminado exitosamente", inventario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al eliminar el inventario", error = ex.Message });
            }
        }
        #endregion

        #region Obtener todo el Inventario Despachado
        [HttpGet]
        public async Task<IActionResult> GetInventarioDespachado()
        {
            try
            {
                var detallesDistribucion = await _context.DetallesDistribuciones
                    .Include(d => d.Distribucion)
                    .Include(d => d.Inventario)
                        .ThenInclude(i => i.Producto)
                            .ThenInclude(p => p.Categoria)
                    .Select(d => new
                    {
                        d.Id_detalle_distribucion,
                        d.Cantidad,
                        d.Id_distribucion,
                        d.Id_inventario,
                        Distribucion = new
                        {
                            d.Distribucion.Id_distribucion,
                            d.Distribucion.Destino
                        },
                        Inventario = new
                        {
                            d.Inventario.Id_inventario,
                            d.Inventario.Id_producto,
                            Producto = new
                            {
                                d.Inventario.Producto.Id_producto,
                                d.Inventario.Producto.Nombre,
                                d.Inventario.Producto.Id_categoria,
                                Categoria = new
                                {
                                    d.Inventario.Producto.Categoria.Id_categoria,
                                    d.Inventario.Producto.Categoria.Nombre
                                }
                            }
                        }
                    })
                    .ToListAsync();

                return detallesDistribucion.Any() ? Ok(detallesDistribucion) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener el inventario despachado.", error = ex.Message });
            }
        }
        #endregion

    }
}
