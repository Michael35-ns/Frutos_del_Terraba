using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReporteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReporteController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Obtener todos los Reportes

        [HttpGet]
        public async Task<IActionResult> GetReportes()
        {
            try
            {
                var reportes = await _context.Reportes
                    .Include(r => r.Producto)
                    .Select(r => new
                    {
                        r.Id_reporte,
                        r.Cantidad_reportada,
                        r.Motivo,
                        r.Fecha,
                        r.Id_producto,
                        Producto = new
                        {
                            r.Producto.Id_producto,
                            r.Producto.Nombre
                        }
                    })
                    .ToListAsync();

                return Ok(reportes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en la API.", error = ex.Message });
            }
        }
        #endregion


        #region Obtener Reporte por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReporte(int id)
        {
            try
            {
                var reporte = await _context.Reportes
                    .Where(r => r.Id_reporte == id)
                    .Include(r => r.Producto)
                    .Select(r => new
                    {
                        r.Id_reporte,
                        r.Cantidad_reportada,
                        r.Motivo,
                        r.Fecha,
                        r.Id_producto,
                        Producto = new
                        {
                            r.Producto.Id_producto,
                            r.Producto.Nombre
                        }
                    })
                    .FirstOrDefaultAsync();

                return reporte != null ? Ok(reporte) : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener el reporte por id.", error = ex.Message });
            }
        }
        #endregion

        #region Crear Reporte
        [HttpPost]
        public async Task<IActionResult> CrearReporte([FromBody] ReporteDTOModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Los datos del reporte no pueden estar vacíos." });

            var reporte = new Reporte
            {
                Cantidad_reportada = model.Cantidad_reportada,
                Motivo = model.Motivo,
                Fecha = model.Fecha,
                Id_producto = model.Id_producto
            };

            _context.Reportes.Add(reporte);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetReporte), new { id = reporte.Id_reporte }, reporte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al crear el reporte", error = ex.Message });
            }
        }
        #endregion

        #region Actualizar Reporte
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarReporte(int id, [FromBody] ReporteDTOModel model)
        {
            if (id != model.Id_reporte)
                return BadRequest(new { message = "El ID del reporte no coincide" });

            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
                return NotFound(new { message = "Reporte no encontrado" });

            reporte.Cantidad_reportada = model.Cantidad_reportada;
            reporte.Motivo = model.Motivo;
            reporte.Fecha = model.Fecha;
            reporte.Id_producto = model.Id_producto;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Reporte actualizado exitosamente", reporte });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al actualizar el reporte", error = ex.Message });
            }
        }
        #endregion

        #region Eliminar Reporte
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
                return NotFound(new { message = "Reporte no encontrado" });

            _context.Reportes.Remove(reporte);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Reporte eliminado exitosamente", reporte });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al eliminar el reporte", error = ex.Message });
            }
        }
        #endregion
    }
}
