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
    public class CD_PRODUCTO
    {
        private readonly CD_CONEXION _conexion;

        public CD_PRODUCTO(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public bool INSERTAR(CE_PRODUCTO obj)
        {
            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo ?? "");
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre ?? "");
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion ?? "");
                    cmd.Parameters.AddWithValue("@Presentacion", obj.Presentacion ?? "");
                    cmd.Parameters.AddWithValue("@Costo_Unitario", obj.Costo_Unitario ?? 0);
                    cmd.Parameters.AddWithValue("@Precio_Venta", obj.Precio_Venta ?? 0);
                    cmd.Parameters.AddWithValue("@Id_Proveedor", obj.Id_Proveedor ?? 0);
                    cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"❌ Error SQL al insertar producto: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error inesperado al insertar producto: {ex.Message}");
                return false;
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }

        public bool ACTUALIZAR(CE_PRODUCTO obj)
        {
            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                    cmd.Parameters.AddWithValue("@Id_Producto", obj.Id_Producto ?? 0);
                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Presentacion", obj.Presentacion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Costo_Unitario", obj.Costo_Unitario ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio_Venta", obj.Precio_Venta ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id_Proveedor", obj.Id_Proveedor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar producto: {ex.Message}");
                return false;
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }

        public List<CE_PRODUCTO> LISTAR()
        {
            var lista = new List<CE_PRODUCTO>();

            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CE_PRODUCTO
                            {
                                Id_Producto = reader["Id_Producto"] as int?,
                                Codigo = reader["Codigo"] as string,
                                Nombre = reader["Nombre"] as string,
                                Descripcion = reader["Descripcion"] as string,
                                Presentacion = reader["Presentacion"] as string,
                                Costo_Unitario = reader["Costo_Unitario"] as decimal?,
                                Precio_Venta = reader["Precio_Venta"] as decimal?,
                                Id_Proveedor = reader["Id_Proveedor"] != DBNull.Value ? Convert.ToInt32(reader["Id_Proveedor"]) : (int?)null,
                                Id_Estado = reader["Id_Estado"] != DBNull.Value ? Convert.ToInt32(reader["Id_Estado"]) : (int?)null,
                                Nombre_Proveedor = reader["Proveedor"] as string,
                                Estado = reader["Estado_Registro"] as string
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al listar productos: {ex.Message}");
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_PRODUCTO> FILTRAR(string texto)
        {
            var lista = new List<CE_PRODUCTO>();

            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                    cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CE_PRODUCTO
                            {
                                Id_Producto = reader["Id_Producto"] as int?,
                                Codigo = reader["Codigo"] as string,
                                Nombre = reader["Nombre"] as string,
                                Descripcion = reader["Descripcion"] as string,
                                Presentacion = reader["Presentacion"] as string,
                                Costo_Unitario = reader["Costo_Unitario"] as decimal?,
                                Precio_Venta = reader["Precio_Venta"] as decimal?,
                                Nombre_Proveedor = reader["Proveedor"] as string,
                                Estado = reader["Estado_Registro"] as string
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al filtrar productos: {ex.Message}");
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return lista;
        }

        public bool ELIMINAR(int id)
        {
            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                    cmd.Parameters.AddWithValue("@Id_Producto", id);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar producto: {ex.Message}");
                return false;
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }

        public bool RESTAURAR(int id)
        {
            try
            {
                using (var conn = _conexion.AbrirConexion())
                using (var cmd = new SqlCommand("SP_Producto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                    cmd.Parameters.AddWithValue("@Id_Producto", id);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al restaurar producto: {ex.Message}");
                return false;
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }
    }
}