using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistribucionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DistribucionController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Obtener Distribuciones
        [HttpGet]
        public async Task<IActionResult> GetDistribuciones()
        {
            try
            {
                var distribucion = await _context.Distribuciones
                    .Include(p => p.Usuario)
                    .Select(p => new
                    {
                        Id_distribucion = p.Id_distribucion,
                        Destino = p.Destino,
                        Ubicacion = p.Ubicacion,
                        Usuario = new
                        {
                            Id = p.UserId,
                            UserName = p.Usuario.UserName
                        }
                    }).ToListAsync();

                return distribucion.Any() ? Ok(distribucion) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener la distribucion.", error = ex.Message });
            }
        }
        #endregion

        #region Crear Distribucion
        [HttpPost]
        public async Task<IActionResult> PostDistribucion([FromBody] DistribucionDTOModel distribucionModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var distribucion = new Distribucion
                {
                    Destino = distribucionModel.Destino,
                    Ubicacion = distribucionModel.Ubicacion,
                    Observaciones = distribucionModel.Observaciones,
                    UserId = distribucionModel.UserId
                };

                await _context.Distribuciones.AddAsync(distribucion);
                await _context.SaveChangesAsync();

                if (distribucionModel.DetallesDistribuciones != null && distribucionModel.DetallesDistribuciones.Any())
                {
                    foreach (var detalle in distribucionModel.DetallesDistribuciones)
                    {
                        var detalleDistribucion = new DetallesDistribucion
                        {
                            Cantidad = detalle.Cantidad,
                            Id_distribucion = distribucion.Id_distribucion,
                            Id_inventario = detalle.Id_inventario
                        };

                        await _context.DetallesDistribuciones.AddAsync(detalleDistribucion);

                        var inventario = await _context.Inventarios.FindAsync(detalle.Id_inventario);
                        if (inventario != null)
                        {
                            inventario.Cantidad -= detalle.Cantidad;
                            _context.Inventarios.Update(inventario);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(GetDistribucion), new { id = distribucion.Id_distribucion }, distribucion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener la distribucion.", error = ex.Message });
            }
        }
        #endregion

        #region Obtener Distribucion por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistribucion(int id)
        {
            try
            {
                var distribucion = await _context.Distribuciones
                    .Where(i => i.Id_distribucion == id)
                    .Include(i => i.Usuario)
                    .Include(i => i.DetallesDistribuciones)
                        .ThenInclude(d => d.Inventario)
                        .ThenInclude(inv => inv.Producto)
                        .ThenInclude(p => p.Categoria)
                    .Select(p => new
                    {
                        Id_distribucion = p.Id_distribucion,
                        Destino = p.Destino,
                        Ubicacion = p.Ubicacion,
                        Usuario = new
                        {
                            Id = p.UserId,
                            UserName = p.Usuario.UserName
                        },
                        DetallesDistribucion = p.DetallesDistribuciones.Select(d => new
                        {
                            Id_detalle_distribucion = d.Id_detalle_distribucion,
                            Cantidad = d.Cantidad,
                            Inventario = new
                            {
                                Id_inventario = d.Inventario.Id_inventario,
                                Cantidad = d.Inventario.Cantidad,
                                Producto = new
                                {
                                    Id_producto = d.Inventario.Producto.Id_producto,
                                    Nombre = d.Inventario.Producto.Nombre,
                                    Categoria = new
                                    {
                                        Id_categoria = d.Inventario.Producto.Categoria.Id_categoria,
                                        Nombre = d.Inventario.Producto.Categoria.Nombre
                                    }
                                }
                            }
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return distribucion != null ? Ok(distribucion) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener la distribución por ID.", error = ex.Message });
            }
        }
        #endregion


        #region Eliminar distribucion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribucion(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var distribucion = await _context.Distribuciones
                    .Include(d => d.DetallesDistribuciones)
                        .ThenInclude(dd => dd.Inventario)
                    .FirstOrDefaultAsync(d => d.Id_distribucion == id);

                if (distribucion == null)
                {
                    return NotFound(new { message = "Distribución no encontrada." });
                }

                foreach (var detalle in distribucion.DetallesDistribuciones)
                {
                    if (detalle.Inventario != null)
                    {
                        detalle.Inventario.Cantidad += detalle.Cantidad; 
                        _context.Inventarios.Update(detalle.Inventario);
                    }
                }

                _context.DetallesDistribuciones.RemoveRange(distribucion.DetallesDistribuciones);

                _context.Distribuciones.Remove(distribucion);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync(); 

                return Ok(new { message = "Distribución eliminada y cantidades restauradas al inventario." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); 
                return BadRequest(new { message = "Error al eliminar la distribución.", error = ex.Message });
            }
        }
        #endregion

    }
}
