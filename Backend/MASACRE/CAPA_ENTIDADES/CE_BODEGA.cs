using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_BODEGA
    {
        public int? Id_Bodega { get; set; }
        public int? Id_Producto { get; set; }
        public int? Cantidad { get; set; }
        public int? Id_Estado { get; set; }

        //// Para visualización
        //public string Nombre_Producto { get; set; }
        //public string Estado { get; set; }
       
    }
}
