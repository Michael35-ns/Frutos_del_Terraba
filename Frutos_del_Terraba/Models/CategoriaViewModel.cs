using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Frutos_del_Terraba.Models
{
        public class CategoriaViewModel
        {
            [BindNever]
            [ValidateNever]
            public List<Categoria> Categorias { get; set; }
            public Categoria NuevaCategoria { get; set; } = new Categoria();
    }
    }

