using System;
using System.Collections.Generic;
using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_VENTA
    {
        private readonly CD_VENTA _cd;

        public CN_VENTA(IConfiguration configuration)
        {
            _cd = new CD_VENTA(configuration);
        }

        public int INSERTAR(DTO_VentaInsertar dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.Id_Cliente <= 0)
                throw new ArgumentException("Cliente inválido.");
            if (dto.Total <= 0)
                throw new ArgumentException("El total debe ser mayor a cero.");

            return _cd.INSERTAR(dto);
        }

        public List<CE_VENTA> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<CE_VENTA> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto?.Trim() ?? "");
        }

        // 🧠 Separar validación para reutilizarla y mantener limpio el método INSERTAR
        private void ValidarVenta(CE_VENTA obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj), "La venta no puede ser nula.");

            if (obj.Id_Cliente is null or <= 0)
                throw new ArgumentException("Debe seleccionar un cliente válido.");

            if (obj.Total is null or <= 0)
                throw new ArgumentException("El total de la venta debe ser mayor que cero.");
        }
    }
}