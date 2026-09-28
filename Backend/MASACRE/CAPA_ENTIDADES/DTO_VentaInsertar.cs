using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_VentaInsertar
    {
        public DateTime? Fecha { get; set; }
        public int? Id_Cliente { get; set; }
        public decimal? Total { get; set; }
    }
}
