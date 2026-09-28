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
    public class CN_ESTADO
    {
        private readonly CD_ESTADO _cdEstado;

        public CN_ESTADO(IConfiguration configuration)
        {
            _cdEstado = new CD_ESTADO(configuration);
        }

        public void InsertarDesdeDTO(DTO_EstadoInsertar dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Estado))
                throw new ArgumentException("El nombre del estado no puede estar vacío.");

            var entidad = new CE_ESTADO
            {
                Estado = dto.Estado,
                Activo = dto.Activo ?? 0
            };

            _cdEstado.INSERTAR_ESTADO(entidad);
        }

        //public void ACTUALIZAR_ESTADO(CE_ESTADO estado)
        //{
        //    if (estado.Id_Estado == null || estado.Id_Estado == 0)
        //        throw new System.ArgumentException("El Id del estado es obligatorio para actualizar.");

        //    _cdEstado.ACTUALIZAR_ESTADO(estado);
        //}
        
        public List<CE_ESTADO> CN_LISTAR_ESTADOS()
        {
            return _cdEstado.LISTAR_ESTADOS();
        }

        public List<CE_ESTADO> CN_FILTRAR_ESTADOS(string texto)
        {
            return _cdEstado.FILTRAR_ESTADOS(texto);
        }
    }

}
