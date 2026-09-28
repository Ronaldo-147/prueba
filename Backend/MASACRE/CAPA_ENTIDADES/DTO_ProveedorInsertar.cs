using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_ProveedorInsertar
    {
        public string Nombre_Proveedor { get; set; }
        public string RUC { get; set; }
        public int? Id_Estado { get; set; } // Opcional, default Activo
    }
}
