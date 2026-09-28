using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_PROVEEDOR
    {
        public int? Id_Proveedor { get; set; }
        public string Nombre_Proveedor { get; set; }
        public string RUC { get; set; }
        public int? Id_Estado { get; set; }

        // Visualización y filtros
        public string Estado { get; set; }
        //public string TextoBusqueda { get; set; }
    }
}
