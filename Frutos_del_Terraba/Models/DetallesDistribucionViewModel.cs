using Frutos_del_Terraba_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Frutos_del_Terraba.Models
{
    public class DetallesDistribucionViewModel
    {
        public int Id_detalle_distribucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public int Cantidad { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public int Id_distribucion { get; set; }

        public int Id_inventario { get; set; }

        [JsonIgnore]
        public DistribucionViewModel ? Distribucion { get; set; }

        public InventarioViewModel ? Inventario { get; set; }
    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
    
}
