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
    public class CN_DIRECCION
    {
        private readonly CD_DIRECCION _cd;

        public CN_DIRECCION(IConfiguration configuration)
        {
            _cd = new CD_DIRECCION(configuration);
        }

        public void InsertarDesdeDTO(DTO_DireccionInsertar dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Direccion_Completa))
                throw new ArgumentException("La dirección no puede estar vacía.");

            if (dto.Id_Tipo_Direccion <= 0)
                throw new ArgumentException("Tipo de dirección inválido.");

            if (dto.Activo != 0 && dto.Activo != 1)
                throw new ArgumentException("Activo debe ser 0 o 1.");

            var entidad = new CE_DIRECCION
            {
                Direccion_Completa = dto.Direccion_Completa,
                Id_Tipo_Direccion = dto.Id_Tipo_Direccion,
                Activo = dto.Activo == 1
            };

            _cd.INSERTAR(entidad);
        }

        public void ACTUALIZAR(CE_DIRECCION obj)
        {
            if (obj.Id_Direccion == null || obj.Id_Direccion == 0)
                throw new ArgumentException("Se requiere el Id_Direccion para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_DIRECCION> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_DIRECCION> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idDireccion)
        {
            _cd.ELIMINAR(idDireccion);
        }

        public void RESTAURAR(int idDireccion)
        {
            _cd.RESTAURAR(idDireccion);
        }
    }

}
