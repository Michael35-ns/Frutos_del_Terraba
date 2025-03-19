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


    }
}
