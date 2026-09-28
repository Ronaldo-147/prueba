using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_UsuarioRespuesta
    {
        public int Id_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre_Completo { get; set; }
        public string Rol { get; set; }
        public string Estado_Registro { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }
    }
}
