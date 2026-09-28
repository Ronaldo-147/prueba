using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_CatalogoRequestDto
    {
       // public int? Id_Catalogo { get; set; } // Solo para actualización
        public int Id_Tipo_Catalogo { get; set; }
        public string Catalogo { get; set; } = string.Empty;
    }
}
