using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_UsuarioInsertar
    {
        public int? Id_Persona { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public int? Id_Rol { get; set; }
        public int? Id_Estado { get; set; } // opcional, por defecto Activo
    }
}
