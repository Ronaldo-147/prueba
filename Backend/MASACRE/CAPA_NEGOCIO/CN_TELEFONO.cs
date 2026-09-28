using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_ENTIDADES.CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_TELEFONO
    {
        private readonly CD_TELEFONO _cd;

        public CN_TELEFONO(IConfiguration configuration)
        {
            _cd = new CD_TELEFONO(configuration);
        }

        public void INSERTAR(CE_TELEFONO obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Numero_Telefono))
                throw new ArgumentException("El número de teléfono no puede estar vacío.");

            if (obj.Id_Tipo_Telefono == null || obj.Id_Tipo_Telefono == 0)
                throw new ArgumentException("Debe seleccionar un tipo de teléfono válido.");

            _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_TELEFONO obj)
        {
            if (obj.Id_Telefono == null || obj.Id_Telefono == 0)
                throw new ArgumentException("Se requiere un Id_Telefono válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_TELEFONO> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_TELEFONO> FILTRAR(string texto, int porNumero, int porTipo)
        {
            return _cd.FILTRAR(texto, porNumero, porTipo); // 👈 ya sin conversiones a bool
        }

        public void ELIMINAR(int idTelefono)
        {
            _cd.ELIMINAR(idTelefono);
        }

        public void RESTAURAR(int idTelefono)
        {
            _cd.RESTAURAR(idTelefono);
        }
    }

}
