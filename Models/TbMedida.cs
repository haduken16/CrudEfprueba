using System;
using System.Collections.Generic;

namespace CRUDWebEf.Models
{
    public partial class TbMedida
    {
        public TbMedida()
        {
            TbProductos = new HashSet<TbProducto>();
        }

        public int IdMedida { get; set; }
        public string? DescripcionMe { get; set; }
        public bool? ActivoMe { get; set; }

        public virtual ICollection<TbProducto> TbProductos { get; set; }
    }
}
