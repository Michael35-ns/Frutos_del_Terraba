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

        public async Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidos()
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



        
    }
}
