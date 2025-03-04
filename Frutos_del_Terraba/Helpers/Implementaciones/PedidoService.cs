using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using System.Net.Http;

namespace Frutos_del_Terraba.Helpers.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7240/api/pedido";

        public PedidoService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<IEnumerable<Pedido>> ObtenerPedidosAsync()
        {
            try
            {
                var pedidos = await _httpClient.GetFromJsonAsync<IEnumerable<Pedido>>(_baseUrl);

                if (pedidos == null)
                {
                    return new List<Pedido>();
                }
                return pedidos;
            }
            catch (Exception ex)
            {
                return new List<Pedido>();
            }
        }
    }
}
