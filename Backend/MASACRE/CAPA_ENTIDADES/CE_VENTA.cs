using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_VENTA
    {
        public int? Id_Venta { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Id_Cliente { get; set; }
        public decimal? Total { get; set; }

        // Para visualización
        public string Nombre_Cliente { get; set; }
        public string DNI_Cliente { get; set; }
        public string TextoBusqueda { get; set; }
    }
}
