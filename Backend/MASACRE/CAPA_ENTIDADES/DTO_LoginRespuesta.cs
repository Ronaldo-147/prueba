using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_LoginRespuesta
    {
        public string Token { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime Expira { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
