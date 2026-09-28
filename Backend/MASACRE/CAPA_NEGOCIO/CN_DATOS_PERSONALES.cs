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
    public class CN_DATOS_PERSONALES
    {
        private readonly CD_DATOS_PERSONALES _cd;

        public CN_DATOS_PERSONALES(IConfiguration configuration)
        {
            _cd = new CD_DATOS_PERSONALES(configuration);
        }

        public CE_DATOS_PERSONALES INSERTAR(CE_DATOS_PERSONALES obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Primer_Nombre))
                throw new ArgumentException("El primer nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(obj.Primer_Apellido))
                throw new ArgumentException("El primer apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(obj.DNI))
                throw new ArgumentException("El número de documento (DNI) es obligatorio.");

            if (obj.Id_Tipo_DNI == null || obj.Id_Tipo_DNI == 0)
                throw new ArgumentException("Debe seleccionar un tipo de documento válido.");

            if (obj.Id_Genero == null || obj.Id_Genero == 0)
                throw new ArgumentException("Debe seleccionar un género.");

            if (obj.Id_Nacionalidad == null || obj.Id_Nacionalidad == 0)
                throw new ArgumentException("Debe seleccionar una nacionalidad.");

            // 🔧 Este return es esencial para que el compilador esté feliz
            return _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_DATOS_PERSONALES obj)
        {
            if (obj.Id_Persona == null || obj.Id_Persona == 0)
                throw new ArgumentException("Se requiere un Id_Persona válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_DATOS_PERSONALES> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_DATOS_PERSONALES> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idPersona)
        {
            _cd.ELIMINAR(idPersona);
        }

        public void RESTAURAR(int idPersona)
        {
            _cd.RESTAURAR(idPersona);
        }
    }

}
