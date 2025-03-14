using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                        .ThenInclude(i => i.Inventario)
                        .ThenInclude(i => i.Producto)
                        .ThenInclude(i => i.Categoria)
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

        

    }
}
