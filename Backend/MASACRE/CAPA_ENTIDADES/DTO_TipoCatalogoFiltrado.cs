using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_TipoCatalogoFiltrado
    {
        public int? Id_Tipo_Catalogo { get; set; }
        public string Tipo_Catalogo { get; set; }
        public int? Activo { get; set; }                    // ✅ tipo int, no bool
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
