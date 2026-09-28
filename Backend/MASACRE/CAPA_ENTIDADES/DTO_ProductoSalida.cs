using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_ProductoSalida
    {
        public int? Id_Producto { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Presentacion { get; set; }
        public decimal? Costo_Unitario { get; set; }
        public decimal? Precio_Venta { get; set; }
        public string? Proveedor { get; set; }
        public string? Estado_Registro { get; set; }
    }
}
