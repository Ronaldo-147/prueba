using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    namespace CAPA_ENTIDADES
    {
        public class CE_TELEFONO
        {
            public int? Id_Telefono { get; set; }
            public string Numero_Telefono { get; set; }
            public int? Id_Tipo_Telefono { get; set; }
            public DateTime? Fecha_Creacion { get; set; }
            public DateTime? Fecha_Modificacion { get; set; }
            public int? Activo { get; set; }

            // Para mostrar tipo en filtros o combos
           public string Tipo_Telefono { get; set; }

            // Para búsquedas con texto libre
            //public string TextoBusqueda { get; set; }
        }
    }
}
