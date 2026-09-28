using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CatalogoResponseDto
    {
        public int Id_Catalogo { get; set; }
        public int Id_Tipo_Catalogo { get; set; }
        public string Catalogo { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Fecha_Modificacion { get; set; }
        public int Activo { get; set; }
    }
}
