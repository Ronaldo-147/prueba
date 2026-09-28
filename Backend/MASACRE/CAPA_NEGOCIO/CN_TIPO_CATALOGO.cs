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
    public class CN_TIPO_CATALOGO
    {
        private readonly CD_TIPO_CATALOGO _cd;

        public CN_TIPO_CATALOGO(IConfiguration configuration)
        {
            _cd = new CD_TIPO_CATALOGO(configuration);
        }

        public void INSERTAR(CE_TIPO_CATALOGO obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Tipo_Catalogo))
                throw new ArgumentException("El nombre del tipo de catálogo no puede estar vacío.");

            _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_TIPO_CATALOGO obj)
        {
            if (obj.Id_Tipo_Catalogo == null || obj.Id_Tipo_Catalogo == 0)
                throw new ArgumentException("El Id del tipo de catálogo es obligatorio para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_TIPO_CATALOGO> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_TIPO_CATALOGO> FILTRAR(string texto, int porID, int porEstado)
        {
            return _cd.FILTRAR(texto, porID, porEstado);
        }

        public void ELIMINAR(int id)
        {
            _cd.ELIMINAR(id);
        }

        public void RESTAURAR(int id)
        {
            _cd.RESTAURAR(id);
        }
    }

}
