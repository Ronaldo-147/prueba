using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_CATALOGO
    {
        public int? Id_Catalogo { get; set; }
        public int? Id_Tipo_Catalogo { get; set; }
        public string Catalogo { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Activo { get; set; }
        //public string TextoBusqueda { get; set; }

        // Propiedad virtual para mostrar el nombre del tipo de catálogo si hacés un JOIN

    }

}
