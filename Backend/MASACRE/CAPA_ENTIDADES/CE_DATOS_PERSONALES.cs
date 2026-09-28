using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_DATOS_PERSONALES
    {
        public int? Id_Persona { get; set; }
        public string Primer_Nombre { get; set; }
        public string Segundo_Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public int? Id_Tipo_DNI { get; set; }
        public string DNI { get; set; }
        public int? Id_Genero { get; set; }
        public int? Id_Nacionalidad { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Estado { get; set; }

        // Para mostrar nombres de catálogo relacionados si hacés JOIN
        public string? Tipo_DNI { get; set; }
        public string? Genero { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Estado_Registro { get; set; }

        // Campo para filtros
        //public string @Busqueda { get; set; }
    }
}