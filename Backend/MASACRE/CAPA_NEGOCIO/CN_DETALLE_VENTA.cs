using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_DETALLE_VENTA
    {
        private readonly CD_DETALLE_VENTA _cd;

        public CN_DETALLE_VENTA(IConfiguration configuration)
        {
            _cd = new CD_DETALLE_VENTA(configuration);
        }

        public string INSERTAR(CE_DETALLE_VENTA obj)
        {
            if (obj.Id_Venta <= 0 || obj.Id_Producto <= 0 || obj.Cantidad <= 0 || obj.Subtotal <= 0)
                throw new ArgumentException("Datos inválidos para el detalle de venta.");

            return _cd.INSERTAR(obj);
        }

        public List<CE_DETALLE_VENTA> FILTRAR(int? idVenta, string texto)
        {
            return _cd.FILTRAR(idVenta, texto);
        }

        public List<CE_DETALLE_VENTA> LISTAR()
        {
            return _cd.LISTAR();
        }
    }

}
