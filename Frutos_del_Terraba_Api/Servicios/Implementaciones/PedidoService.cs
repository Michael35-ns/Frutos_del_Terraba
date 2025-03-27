using Frutos_del_Terraba_Api.DTO;
using Frutos_del_Terraba_Api.Models;
using Frutos_del_Terraba_Api.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Frutos_del_Terraba_Api.Servicios.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoDTOModel> CrearPedidoAsync(PedidoDTOModel pedidoDTO)
        {
            var pedido = new Pedido
            {
                UserId = pedidoDTO.UserId,
                Fecha = pedidoDTO.Fecha,
                Id_proveedor = pedidoDTO.Id_proveedor

            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            pedidoDTO.Id_pedido = pedido.Id_pedido;
            return pedidoDTO;
        }

        public async Task<PedidoDTOModel> ObtenerPedidoPorIdAsync(int id)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Proveedor)
                .Where(p => p.Id_pedido == id)
                .Select(p => new PedidoDTOModel
                {
                    Id_pedido = p.Id_pedido,
                    Fecha = p.Fecha,
                    Id_proveedor = p.Id_proveedor,
                    UserId = p.UserId
                }).FirstOrDefaultAsync();
            return pedidos;
        }

        public async Task<bool> ActualizarPedidoAsync(int id, PedidoDTOModel pedidoDTO)
        {
                var pedido = await _context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    Console.WriteLine($"No se encontró el pedido con ID {id}.");
                    return false;
                }

                pedido.UserId = pedidoDTO.UserId;
                pedido.Fecha = pedidoDTO.Fecha;
                pedido.Id_proveedor = pedidoDTO.Id_proveedor;

                await _context.SaveChangesAsync();
                Console.WriteLine($"Pedido con ID {id} actualizado correctamente.");
                return true;
        }

        public async Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidosAsync()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Proveedor)
                .ToListAsync();

            return pedidos.Select(p => new PedidoDTOModel
            {
                Id_pedido = p.Id_pedido,
                Fecha = p.Fecha,
                Id_proveedor = p.Id_proveedor,
                UserId = p.UserId
            }).ToList();
        }

        public async Task<bool> EliminarPedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                Console.WriteLine($"No se encontró el pedido con ID {id}.");
                return false;
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Pedido con ID {id} eliminado correctamente.");
            return true;
        }

    }
}
