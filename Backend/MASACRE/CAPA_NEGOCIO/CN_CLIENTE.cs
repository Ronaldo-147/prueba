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
    public class CN_CLIENTE
    {
        private readonly CD_CLIENTE _cd;

        public CN_CLIENTE(IConfiguration configuration)
        {
            _cd = new CD_CLIENTE(configuration);
        }

        public string INSERTAR(CE_CLIENTE obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "⚠️ El objeto cliente no puede ser nulo.");

            if (obj.Id_Persona == null || obj.Id_Persona == 0)
                throw new ArgumentException("⚠️ El Id de la persona es obligatorio para insertar un cliente.");

            return _cd.INSERTAR(obj);
        }

        public void ACTUALIZAR(CE_CLIENTE obj)
        {
            if (obj.Id_Cliente == null || obj.Id_Cliente == 0)
                throw new ArgumentException("Se requiere un Id_Cliente válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_CLIENTE> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_CLIENTE> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idCliente)
        {
            _cd.ELIMINAR(idCliente);
        }

        public void RESTAURAR(int idCliente)
        {
            _cd.RESTAURAR(idCliente);
        }
    }

}
