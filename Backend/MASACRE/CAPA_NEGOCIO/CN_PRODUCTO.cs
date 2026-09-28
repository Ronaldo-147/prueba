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
    public class CN_PRODUCTO
    {
        private readonly CD_PRODUCTO _cd;

        public CN_PRODUCTO(IConfiguration configuration)
        {
            _cd = new CD_PRODUCTO(configuration);
        }

        public bool INSERTAR(CE_PRODUCTO obj)
        {
            ValidarProductoParaInsertar(obj);
            return _cd.INSERTAR(obj);
        }

        public bool ACTUALIZAR(CE_PRODUCTO obj)
        {
            if (obj.Id_Producto == null || obj.Id_Producto == 0)
                throw new ArgumentException("Se requiere un Id_Producto válido para actualizar.");

            return _cd.ACTUALIZAR(obj);
        }

        public List<CE_PRODUCTO> LISTAR()
        {
            return _cd.LISTAR();
        }

        public List<DTO_ProductoSalida> FILTRAR_PROYECTADO(string texto)
        {
            var lista = _cd.FILTRAR(texto);

            return lista.Select(p => new DTO_ProductoSalida
            {
                Id_Producto = p.Id_Producto ?? 0,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Presentacion = p.Presentacion,
                Costo_Unitario = p.Costo_Unitario,
                Precio_Venta = p.Precio_Venta,
                Proveedor = p.Nombre_Proveedor,
                Estado_Registro = p.Estado
            }).ToList();
        }

        public bool ELIMINAR(int idProducto)
        {
            return _cd.ELIMINAR(idProducto);
        }

        public bool RESTAURAR(int idProducto)
        {
            return _cd.RESTAURAR(idProducto);
        }

        // 🔍 Validaciones centralizadas
        private void ValidarProductoParaInsertar(CE_PRODUCTO obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Codigo))
                throw new ArgumentException("El código del producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(obj.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
                throw new ArgumentException("La descripción del producto es obligatoria.");

            if (string.IsNullOrWhiteSpace(obj.Presentacion))
                throw new ArgumentException("Debe especificar la presentación del producto.");

            if (obj.Costo_Unitario == null || obj.Costo_Unitario <= 0)
                throw new ArgumentException("El costo unitario debe ser mayor que cero.");

            if (obj.Precio_Venta == null || obj.Precio_Venta <= 0)
                throw new ArgumentException("El precio de venta debe ser mayor que cero.");

            if (obj.Id_Proveedor == null || obj.Id_Proveedor == 0)
                throw new ArgumentException("Debe asignar un proveedor válido.");
        }
    }
}