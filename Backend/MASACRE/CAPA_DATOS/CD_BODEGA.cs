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
    public class CD_BODEGA
    {
        private readonly CD_CONEXION _conexion;

        public CD_BODEGA(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_BODEGA obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Id_Producto", obj.Id_Producto ?? 0);
                cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad ?? 0);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_BODEGA obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Bodega", obj.Id_Bodega ?? 0);
                cmd.Parameters.AddWithValue("@Id_Producto", obj.Id_Producto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<BodegaResponseDto> LISTAR()
        {
            var lista = new List<BodegaResponseDto>();

            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BodegaResponseDto
                        {
                            Id_Bodega = reader["Id_Bodega"] as int? ?? 0,
                            Nombre_Producto = reader["Nombre_Producto"] as string,
                            Codigo_Producto = reader["Codigo_Producto"] as string,
                            Cantidad = reader["Cantidad"] as int?,
                            Estado = reader["Estado_Registro_Bodega"] as string
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<BodegaResponseDto> FILTRAR(string texto)
        {
            var lista = new List<BodegaResponseDto>();

            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@TextoBusqueda", texto ?? string.Empty);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BodegaResponseDto
                        {
                            Id_Bodega = reader["Id_Bodega"] != DBNull.Value ? Convert.ToInt32(reader["Id_Bodega"]) : 0,
                            Nombre_Producto = reader["Nombre_Producto"]?.ToString(),
                            Codigo_Producto = reader["Codigo_Producto"]?.ToString(),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (int?)null,
                            Estado = reader["Estado_Registro_Bodega"]?.ToString()
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Bodega", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Bodega", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Bodega", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
