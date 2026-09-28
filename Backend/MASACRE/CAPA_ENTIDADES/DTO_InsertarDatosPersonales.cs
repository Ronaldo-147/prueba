using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_InsertarDatosPersonales
    {
        public string Primer_Nombre { get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public int Id_Tipo_DNI { get; set; }
        public string DNI { get; set; }
        public int Id_Genero { get; set; }
        public int Id_Nacionalidad { get; set; }
        public int? Id_Estado { get; set; }
    }
}