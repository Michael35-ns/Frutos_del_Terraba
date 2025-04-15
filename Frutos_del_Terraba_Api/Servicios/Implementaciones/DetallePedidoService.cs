using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Servicios.Implementaciones
{
    public class DetallePedidoService : IDetallesPedidoService
    {
        private readonly ApplicationDbContext _context;

        public DetallePedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id)
        {
            var detalles = await _context.DetallesPedidos
                            .Where(d => d.Id_pedido == id)
                            .Include(d => d.Producto)
                            .Include(d => d.Pedido)
                            .ToListAsync();

            return detalles.Select(d => new DetallesPedidoDTOModel
            {
                Id_detalle = d.Id_detalle,
                Id_pedido = d.Id_pedido,
                Cantidad = d.Cantidad,
                Id_producto = d.Id_producto,
                Observaciones = d.Observaciones
            }).ToList();
        }


        public async Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel detalles)
        {
            if (detalles == null || detalles.Id_pedido == 0)
            {
                throw new ArgumentException("El ID del pedido es obligatorio.");
            }

            var detallePedido = new DetallesPedido
            {
                Id_pedido = detalles.Id_pedido,
                Id_producto = detalles.Id_producto,
                Cantidad = detalles.Cantidad,
                Observaciones = detalles.Observaciones ?? "" // Evita nulos en la columna Observaciones
            };

            _context.DetallesPedidos.Add(detallePedido);
            await _context.SaveChangesAsync();

            return new DetallesPedidoDTOModel
            {
                Id_pedido = detallePedido.Id_pedido,
                Cantidad = detallePedido.Cantidad,
                Id_producto = detallePedido.Id_producto
            };
        }

        public async Task<DetallesPedidoDTOModel> ObtenerDetallesPedidoPorId(int id)
        {
            var detalle = await _context.DetallesPedidos
                        .Where(d => d.Id_detalle == id)
                        .Select(d => new DetallesPedidoDTOModel
                        {
                            Id_detalle = d.Id_detalle,
                            Cantidad = d.Cantidad,
                            Id_producto = d.Id_producto,
                            Id_pedido = d.Id_pedido,
                            Observaciones = d.Observaciones
                        }).FirstOrDefaultAsync();  // Esto debería devolver solo un objeto o null
            return detalle;
        }


        public async Task<bool> ActualizarDetallesPedido(int id, DetallesPedidoDTOModel detalles)
        {
            var detalle = await _context.DetallesPedidos.FindAsync(id);
            if(detalle == null)
            {
                Console.WriteLine($"No se encontró el detalle con ID {id}.");
                return false;
            }

            detalle.Cantidad = detalles.Cantidad;
            detalle.Observaciones = detalles.Observaciones;

            await _context.SaveChangesAsync();
            Console.WriteLine($"Detalle con ID {id} actualizado correctamente.");
            return true;

        }

        public async Task<bool> EliminarDetallesPedido(int id)
        {
            var detalle = _context.DetallesPedidos.Find(id);
            if(detalle == null)
            {
                Console.WriteLine($"No se encontró el detalle con ID {id}.");
                return false;
            }
            _context.DetallesPedidos.Remove(detalle);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
