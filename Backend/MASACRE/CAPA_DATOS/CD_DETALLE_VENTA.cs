using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CAPA_DATOS
{
    public class CD_DETALLE_VENTA
    {
        private readonly CD_CONEXION _conexion;

        public CD_DETALLE_VENTA(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public string INSERTAR(CE_DETALLE_VENTA obj)
        {
            string mensaje = "";

            using (SqlCommand cmd = new SqlCommand("SP_Detalle_Venta", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Id_Venta", obj.Id_Venta);
                cmd.Parameters.AddWithValue("@Id_Producto", obj.Id_Producto);
                cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad);
                cmd.Parameters.AddWithValue("@Precio_Unitario_Al_Momento_Venta", obj.Precio_Unitario_Al_Momento_Venta);
                cmd.Parameters.AddWithValue("@Subtotal", obj.Subtotal);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        mensaje = reader["Mensaje"]?.ToString();
                }

                _conexion.CerrarConexion();
            }

            return mensaje;
        }

        public List<CE_DETALLE_VENTA> FILTRAR(int? idVenta, string texto)
        {
            var lista = new List<CE_DETALLE_VENTA>();

            using (var conexion = _conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_Detalle_Venta", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Id_Venta", (object?)idVenta ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount == 1 && reader["Mensaje"]?.ToString() == "SIN RESULTADOS")
                            break;

                        lista.Add(new CE_DETALLE_VENTA
                        {
                            Id_Detalle = reader["Id_Detalle"] as int?,
                            Id_Venta = reader["Id_Venta"] as int?,
                            Id_Producto = reader["Id_Producto"] as int?,  // 👈 ¡Este es el clave!
                            Nombre_Producto = reader["Nombre_Producto"]?.ToString(),
                            Codigo_Producto = reader["Codigo_Producto"]?.ToString(),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : 0,
                            Precio_Unitario_Al_Momento_Venta = reader["Precio_Unitario_Al_Momento_Venta"] != DBNull.Value
                                ? Convert.ToDecimal(reader["Precio_Unitario_Al_Momento_Venta"]) : 0,
                            Subtotal = reader["Subtotal"] != DBNull.Value ? Convert.ToDecimal(reader["Subtotal"]) : 0
                        });
                    }
                }
            }

            return lista;
        }

        public List<CE_DETALLE_VENTA> LISTAR()
        {
            var lista = new List<CE_DETALLE_VENTA>();

            using (var conexion = _conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_Detalle_Venta", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_DETALLE_VENTA
                        {
                            Id_Detalle = reader["Id_Detalle"] as int?,
                            Id_Venta = reader["Id_Venta"] as int?,
                            Id_Producto = reader["Id_Producto"] as int?,                     // ✅ Nuevo
                            Nombre_Producto = reader["Nombre_Producto"]?.ToString(),
                            Codigo_Producto = reader["Codigo_Producto"]?.ToString(),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : 0,
                            Precio_Unitario_Al_Momento_Venta = reader["Precio_Unitario_Al_Momento_Venta"] != DBNull.Value
          ? Convert.ToDecimal(reader["Precio_Unitario_Al_Momento_Venta"]) : 0,
                            Subtotal = reader["Subtotal"] != DBNull.Value ? Convert.ToDecimal(reader["Subtotal"]) : 0
                        });
                    }
                }
            }

            return lista;
        }
    }   }