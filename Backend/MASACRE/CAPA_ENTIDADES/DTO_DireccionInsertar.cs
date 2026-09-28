using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_DireccionInsertar
    {
        public string Direccion_Completa { get; set; }
        public int Id_Tipo_Direccion { get; set; }
        public int? Activo { get; set; }  // Swagger lo mostrará como true/false
    }
}
