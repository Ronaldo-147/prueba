using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class BodegaResponseDto
    {
        public int Id_Bodega { get; set; }
        public string Nombre_Producto { get; set; }
        public string Codigo_Producto { get; set; }
        public int? Cantidad { get; set; }
        public string Estado { get; set; }
    }

}
