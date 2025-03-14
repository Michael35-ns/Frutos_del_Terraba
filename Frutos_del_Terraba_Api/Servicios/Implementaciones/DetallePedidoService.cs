using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Servicios.Implementaciones
{
    public class DetallePedidoService : IDetallesPedido
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
                            .Include(d=> d.Pedido)
                            .ToListAsync();

            return detalles.Select(d => new DetallesPedidoDTOModel
            {
                Id_pedido = d.Id_pedido,
                Cantidad = d.Cantidad,
                Id_producto = d.Id_producto,
                NombreProducto = d.Producto?.Nombre
            }).ToList();
        }
    }
}
