using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_ROL
    {
        public int? Id_Rol { get; set; }
        public string Rol { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Estado { get; set; }

        // Para mostrar nombre del estado si hacés JOIN
        public string Estado { get; set; }

        // Campo auxiliar para búsquedas opcionales
        //public string TextoBusqueda { get; set; }
    }
}