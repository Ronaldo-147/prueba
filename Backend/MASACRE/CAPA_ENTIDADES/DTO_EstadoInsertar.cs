using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class DTO_EstadoInsertar
    {
        public string? Estado { get; set; }
        public int? Activo { get; set; } // 0 o 1 → luego lo convertís a bool
    }
}
