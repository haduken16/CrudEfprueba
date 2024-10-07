using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDWebEf.Models.ViewsModel
{
    public class ProductoVM
    {
        public TbProducto oProducto { get; set; }
        public List<SelectListItem> olistaMedidas { get; set; }
        public List<SelectListItem> olistaCategorias { get; set; }
    }
}
