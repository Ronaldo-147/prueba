using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_DETALLE_VENTA
    {
        public int? Id_Detalle { get; set; }
        public int? Id_Venta { get; set; }
        public int? Id_Producto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio_Unitario_Al_Momento_Venta { get; set; }
        public decimal? Subtotal { get; set; }

        // Visualización
        public string Nombre_Producto { get; set; }
        public string Codigo_Producto { get; set; }
       // public string TextoBusqueda { get; set; }
    }
}
