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
    public class CN_PROVEEDOR
    {
        private readonly CD_PROVEEDOR _cd;

        public CN_PROVEEDOR(IConfiguration configuration)
        {
            _cd = new CD_PROVEEDOR(configuration);
        }

        public void InsertarDesdeDTO(DTO_ProveedorInsertar dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre_Proveedor))
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.RUC))
                throw new ArgumentException("El RUC es obligatorio.");

            var entidad = new CE_PROVEEDOR
            {
                Nombre_Proveedor = dto.Nombre_Proveedor,
                RUC = dto.RUC,
                Id_Estado = dto.Id_Estado ?? 0
            };

            _cd.INSERTAR(entidad);
        }


        public void ACTUALIZAR(CE_PROVEEDOR obj)
        {
            if (obj.Id_Proveedor == null || obj.Id_Proveedor == 0)
                throw new ArgumentException("Se requiere un Id_Proveedor válido para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CE_PROVEEDOR> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_PROVEEDOR> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int idProveedor)
        {
            _cd.ELIMINAR(idProveedor);
        }

        public void RESTAURAR(int idProveedor)
        {
            _cd.RESTAURAR(idProveedor);
        }
    }

}
