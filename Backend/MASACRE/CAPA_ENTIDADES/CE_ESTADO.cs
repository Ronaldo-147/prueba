using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_ESTADO
    {
        public int? Id_Estado { get; set; }
        public string Estado { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Activo { get; set; }
       // public string TextoBusqueda { get; set; } // Para búsquedas opcionales desde filtros
    }

}
