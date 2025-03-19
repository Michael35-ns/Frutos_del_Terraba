using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Frutos_del_Terraba_Api.Controllers
{
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProveedorController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Obtener todos los proveedores
        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            try
            {
                var Proveedores = await _context.Proveedores.ToListAsync();
                if (Proveedores.Any())
                    return Ok(Proveedores);
                else
                    return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener los proveedores.", error = ex.Message });
            }
        }
        #endregion

        #region Obtener Proveedor por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound(new { message = "Proveedor no encontrada" });

            return Ok(proveedor);
        }
        #endregion

        #region Crear un nuevo Proveedor
        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorDTOModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var proveedor = new Proveedor
            {
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Telefono = model.Telefono,
                Email = model.Email
            };

            await _context.Proveedores.AddAsync(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id_proveedor }, proveedor);
        }

        #endregion

        #region Actualizar Proveedor
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] ProveedorDTOModel model)
        {
            if (id != model.Id_proveedor)
                return BadRequest(new { message = "El ID del proveedor no coincide" });

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound(new { message = "Proveedor no encontrado" });

            proveedor.Nombre = model.Nombre ?? proveedor.Nombre;
            proveedor.Apellidos = model.Apellidos ?? proveedor.Apellidos;
            proveedor.Telefono = model.Telefono ?? proveedor.Telefono;
            proveedor.Email = model.Email ?? proveedor.Email;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Proveedor actualizado exitosamente", proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al actualizar el proveedor", error = ex.Message });
            }
        }
        #endregion

        #region Eliminar Proveedor
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound();
            _context.Proveedores.Remove(proveedor);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Proveedor eliminado exitosamente", proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor, al eliminar el proveedor", error = ex.Message });
            }
        }
        #endregion

        #region Obtener historial de Pedidos realizados y Proveedor por Id
        [HttpGet("historial/{id}")]
        public async Task<IActionResult> GetHistorialPedidos_Y_Proveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound(new { message = "Proveedor no encontrado" });

            var historialPedidos = await _context.Pedidos
                .Where(p => p.Id_proveedor == id)
                .Include(p => p.Usuario)
                .Include(p => p.DetallesPedidos)
                    .ThenInclude(d => d.Producto) 
                .Select(p => new {
                    Id_pedido = p.Id_pedido,
                    Fecha = p.Fecha,
                    Usuario = new
                    {
                        Id = p.UserId,
                        UserName = p.Usuario.UserName
                    },
                    DetallesPedidos = p.DetallesPedidos.Select(d => new {
                        Id_detalle = d.Id_detalle,
                        Cantidad = d.Cantidad,
                        Observaciones = d.Observaciones,
                        Id_producto = d.Id_producto,
                        NombreProducto = d.Producto.Nombre 
                    })
                })
                .ToListAsync();

            return Ok(new
            {
                Proveedor = proveedor,
                HistorialPedidos = historialPedidos
            });
        }
        #endregion
    }
}
