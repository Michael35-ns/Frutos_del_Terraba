using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using System.Text.Json;
using System.Text;

namespace Frutos_del_Terraba.Helpers.Implementaciones
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7240/api/categoria";

        public CategoriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
        }

        #region Obtener todas las categorías
        public async Task<IEnumerable<Categoria>> ObtenerTodasCategoriasAsync()
        {
            try
            {
                var categorias = await _httpClient.GetFromJsonAsync<IEnumerable<Categoria>>(_baseUrl);

                if (categorias == null)
                {
                    return new List<Categoria>();
                }
                return categorias;
            }
            catch (Exception ex)
            {
                return new List<Categoria>(); 
            }
        }

        #endregion

        #region Obtener categoría por Id
        public async Task<Categoria?> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Categoria>($"{_baseUrl}/{id}");
        }
        #endregion

        #region Crear nueva categoría
        public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, categoria);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la API: {response.StatusCode} - {responseContent}");
            }

            return await response.Content.ReadFromJsonAsync<Categoria>()
                ?? throw new Exception("Error al deserializar la respuesta de la API.");
        }
        #endregion

        #region Actualizar categoría
        public async Task<Categoria> ActualizarCategoriaAsync(int id, Categoria categoria)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", categoria);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en la API al actualizar categoría. Código: {response.StatusCode}, Respuesta: {responseContent}");
            }

            return await response.Content.ReadFromJsonAsync<Categoria>()
                ?? throw new Exception("Error al deserializar la respuesta de la API.");
        }
        #endregion

        #region Eliminar categoría
        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
