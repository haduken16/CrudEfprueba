using System;
using System.Collections.Generic;

namespace CRUDWebEf.Models
{
    public partial class TbCategoria
    {
        public TbCategoria()
        {
            TbProductos = new HashSet<TbProducto>();
        }

        public int IdCategoria { get; set; }
        public string? DescripcionCa { get; set; }
        public bool? ActivoCa { get; set; }

        public virtual ICollection<TbProducto> TbProductos { get; set; }
    }
}
