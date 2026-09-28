using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_RolInsertar
    {
        public string? Rol { get; set; }
        public int? Id_Estado { get; set; } // Opcional, por defecto 'Activo'
    }
}
