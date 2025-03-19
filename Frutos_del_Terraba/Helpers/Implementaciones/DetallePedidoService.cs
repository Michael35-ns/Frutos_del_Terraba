using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Frutos_del_Terraba_Api.DTO;

namespace Frutos_del_Terraba.Helpers.Implementaciones
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7240/api/detallepedido";

        public DetallePedidoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DetallesPedidoDTOModel> AgregarDetallesPedido(DetallesPedidoDTOModel detalles)
        {
            if (detalles.Id_pedido == 0)
            {
                throw new ArgumentException("El ID del pedido es obligatorio.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, detalles);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DetallesPedidoDTOModel>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al agregar detalles: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comunicarse con la API: " + ex.Message);
            }
        }



        public async Task<IEnumerable<DetallesPedidoDTOModel>> ObtenerDetallesPedidos(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<DetallesPedidoDTOModel>>($"{_baseUrl}/{id}");
        }
    }
}
