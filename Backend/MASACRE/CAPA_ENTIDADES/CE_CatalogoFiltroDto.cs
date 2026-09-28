using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CatalogoFiltroDto
    {
        public string Accion { get; set; } /*= "FILTRAR";*/
        public string TextoBusqueda { get; set; } = string.Empty;
    }
}
