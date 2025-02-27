using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using System.Text;
using System.Text.Json;

namespace Frutos_del_Terraba.Helpers.Implementaciones
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoriaService _categoriaService;
        private readonly string _baseUrl = "https://localhost:7240/api/producto";

        public ProductoService(HttpClient httpClient, ICategoriaService categoriaService)
        {
            _httpClient = httpClient;
            _categoriaService = categoriaService;
        }

        #region Obtener todos los productos
        public async Task<IEnumerable<ProductoViewModel>> ObtenerTodosProductosAsync()
        {
            var productos = await _httpClient.GetFromJsonAsync<IEnumerable<ProductoViewModel>>(_baseUrl) ?? new List<ProductoViewModel>();
            foreach (var producto in productos)
            {
                var categoria = await _categoriaService.ObtenerCategoriaPorIdAsync(producto.Id_categoria);
                producto.NombreCategoria = categoria?.Nombre ?? "Desconocida";
            }
            return productos;
        }
        #endregion

        #region Obtener producto por Id
        public async Task<ProductoViewModel?> ObtenerProductoPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoViewModel>($"{_baseUrl}/{id}");
        }
        #endregion

        #region Crear nuevo producto
        public async Task<ProductoViewModel> CrearProductoAsync(ProductoViewModel producto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, producto);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la API: {response.StatusCode} - {responseContent}");
            }

            return await response.Content.ReadFromJsonAsync<ProductoViewModel>()
                ?? throw new Exception("Error al deserializar la respuesta de la API.");
        }
        #endregion

        #region Actualizar producto
        public async Task<ProductoViewModel> ActualizarProductoAsync(int id, ProductoViewModel producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", producto);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return producto;
            }

            return await response.Content.ReadFromJsonAsync<ProductoViewModel>() ?? producto;
        }

        #endregion

        #region Eliminar producto
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
