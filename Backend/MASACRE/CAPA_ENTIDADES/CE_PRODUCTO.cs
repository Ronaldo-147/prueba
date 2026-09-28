using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_PRODUCTO
    {
        public int? Id_Producto { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Presentacion { get; set; }
        public decimal? Costo_Unitario { get; set; }
        public decimal? Precio_Venta { get; set; }
        public int? Id_Proveedor { get; set; }
        public int? Id_Estado { get; set; }

        // Para visualización y filtros
        public string? Nombre_Proveedor { get; set; }
        public string? Estado { get; set; }
        //public string? TextoBusqueda { get; set; }
    }
}
