using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_DIRECCION
    {
        public int? Id_Direccion { get; set; }
        public string Direccion_Completa { get; set; }
        public int? Id_Tipo_Direccion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public bool? Activo { get; set; }

        // Para mostrar el nombre del tipo de dirección
        public string Tipo_Direccion { get; set; }

        // Para filtros
        public string TextoBusqueda { get; set; }
    }
}
