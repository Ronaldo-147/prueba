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
    public class CN_CORREO
    {
        private readonly CD_CORREO _cd;

        public CN_CORREO(IConfiguration configuration)
        {
            _cd = new CD_CORREO(configuration);
        }

        public void INSERTAR(CE_CORREO obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Direccion_Correo))
                throw new ArgumentException("La dirección de correo no puede estar vacía.");

            if (obj.Id_Tipo_Correo == null || obj.Id_Tipo_Correo == 0)
                throw new ArgumentException("Debe seleccionar un tipo de correo válido.");

            _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_CORREO obj)
        {
            if (obj.Id_Correo == null || obj.Id_Correo == 0)
                throw new ArgumentException("Se requiere un Id_Correo válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_CORREO> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_CORREO> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idCorreo)
        {
            _cd.ELIMINAR(idCorreo);
        }

        public void RESTAURAR(int idCorreo)
        {
            _cd.RESTAURAR(idCorreo);
        }
    }

}
