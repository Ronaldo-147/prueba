using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_TelefonoInsertar
    {
        public string Numero_Telefono { get; set; }
        public int? Id_Tipo_Telefono { get; set; }
        public int? Activo { get; set; } // Opcional, por defecto 1
    }
}
