using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;
using System.Text.Json;

namespace Frutos_del_Terraba.Helpers.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7240/api/pedido";
        private readonly string _proveedorUrl = "https://localhost:7240/api/proveedor";

        public PedidoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PedidoDTOModel>> ObtenerTodosPedidosAsync()
        {
            try
            {
                var pedidos = await _httpClient.GetFromJsonAsync<IEnumerable<PedidoDTOModel>>(_baseUrl);
                return pedidos ?? new List<PedidoDTOModel>();
            }
            catch
            {
                return new List<PedidoDTOModel>();
            }
        }

        public async Task<IEnumerable<ProveedorDTOModel>> ObtenerTodosProveedoresAsync()
        {
            try
            {
                var proveedores = await _httpClient.GetFromJsonAsync<IEnumerable<ProveedorDTOModel>>(_proveedorUrl);
                return proveedores ?? new List<ProveedorDTOModel>();
            }
            catch
            {
                return new List<ProveedorDTOModel>();
            }
        }

        public async Task<PedidoDTOModel?> CrearPedidoAsync(PedidoDTOModel pedido)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, pedido);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PedidoDTOModel>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<PedidoDTOModel> ObtenerPedidoPorIdAsync(int id)
        {
            var pedido = _httpClient.GetFromJsonAsync<PedidoDTOModel>($"{_baseUrl}/{id}");
            return pedido;
        }

        public async Task<PedidoDTOModel> ActualizarPedidoAsync(int id, PedidoDTOModel pedido)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", pedido);

            var content = await response.Content.ReadAsStringAsync();

            var pedidos = await response.Content.ReadFromJsonAsync<PedidoDTOModel>();

            return pedidos;


        }

        public async Task<bool> EliminarPedidoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
