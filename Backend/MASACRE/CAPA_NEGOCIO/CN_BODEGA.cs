using System;
using System.Collections.Generic;
using System.Linq;
using CAPA_DATOS;
using CAPA_ENTIDADES;

using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_BODEGA
    {
        private readonly CD_BODEGA _cd;

        public CN_BODEGA(IConfiguration configuration)
        {
            _cd = new CD_BODEGA(configuration);
        }

        public void INSERTAR(CE_BODEGA dto)
        {
            if (dto.Id_Producto == null || dto.Id_Producto == 0)
                throw new ArgumentException("Debe asociar un producto válido.");

            if (dto.Cantidad == null || dto.Cantidad < 0)
                throw new ArgumentException("La cantidad debe ser cero o mayor.");

            var entidad = new CE_BODEGA
            {
                Id_Producto = dto.Id_Producto,
                Cantidad = dto.Cantidad,
                Id_Estado = dto.Id_Estado
            };

            _cd.INSERTAR(entidad);
        }

        public void ACTUALIZAR(CE_BODEGA dto)
        {
            if (dto.Id_Bodega == null || dto.Id_Bodega == 0)
                throw new ArgumentException("Se requiere un Id_Bodega válido para actualizar.");

            var entidad = new CE_BODEGA
            {
                Id_Bodega = dto.Id_Bodega,
                Id_Producto = dto.Id_Producto,
                Cantidad = dto.Cantidad,
                Id_Estado = dto.Id_Estado
            };

            _cd.ACTUALIZAR(entidad);
        }

        public List<BodegaResponseDto> LISTAR()
        {
            return _cd.LISTAR(); // ya devuelve List<BodegaResponseDto>
        }

        public List<BodegaResponseDto> FILTRAR(BodegaFiltroDto filtro)
        {
            string texto = filtro.TextoBusqueda ?? string.Empty;
            return _cd.FILTRAR(texto);
        }
        public void ELIMINAR(int idBodega)
        {
            _cd.ELIMINAR(idBodega);
        }

        public void RESTAURAR(int idBodega)
        {
            _cd.RESTAURAR(idBodega);
        }
    }
}