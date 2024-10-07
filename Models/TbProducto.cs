using System;
using System.Collections.Generic;

namespace CRUDWebEf.Models
{
    public partial class TbProducto
    {
        public int IdProducto { get; set; }
        public string? DescripcionPr { get; set; }
        public string? Marca { get; set; }
        public int? IdMedida { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Stock { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool? ActivoPr { get; set; }
        public decimal? Precio { get; set; }

        public virtual TbCategoria? oCategoria { get; set; }
        public virtual TbMedida? oMedida { get; set; }
    }
}
