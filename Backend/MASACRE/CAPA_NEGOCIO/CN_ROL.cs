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
    public class CN_ROL
    {
        private readonly CD_ROL _cd;

        public CN_ROL(IConfiguration configuration)
        {
            _cd = new CD_ROL(configuration);
        }

        public void INSERTAR(CE_ROL obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Rol))
                throw new ArgumentException("El nombre del rol no puede estar vacío.");

            _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_ROL obj)
        {
            if (obj.Id_Rol == null || obj.Id_Rol == 0)
                throw new ArgumentException("Se requiere un Id_Rol válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_ROL> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_ROL> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idRol)
        {
            _cd.ELIMINAR(idRol);
        }

        public void RESTAURAR(int idRol)
        {
            _cd.RESTAURAR(idRol);
        }
    }

}
