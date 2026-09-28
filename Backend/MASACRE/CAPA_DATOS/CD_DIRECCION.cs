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
    public class CD_DIRECCION
    {
        private readonly CD_CONEXION _conexion;

        public CD_DIRECCION(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_DIRECCION obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Direccion_Completa", obj.Direccion_Completa ?? "");
                cmd.Parameters.AddWithValue("@Id_Tipo_Direccion", obj.Id_Tipo_Direccion ?? 0);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? true);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_DIRECCION obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Direccion", obj.Id_Direccion ?? 0);
                cmd.Parameters.AddWithValue("@Direccion_Completa", obj.Direccion_Completa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Tipo_Direccion", obj.Id_Tipo_Direccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_DIRECCION> LISTAR()
        {
            var lista = new List<CE_DIRECCION>();

            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_DIRECCION
                        {
                            Id_Direccion = reader["Id_Direccion"] as int?,
                            Direccion_Completa = reader["Direccion_Completa"] as string,
                            Tipo_Direccion = reader["Tipo_Direccion"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                            Activo = reader["Activo"] != DBNull.Value ? (bool?)reader["Activo"] : null,
                            Id_Tipo_Direccion = reader["Id_Tipo_Direccion"] as int?
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_DIRECCION> FILTRAR(string texto)
        {
            var lista = new List<CE_DIRECCION>();

            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_DIRECCION
                        {
                            Id_Direccion = reader["Id_Direccion"] as int?,
                            Direccion_Completa = reader["Direccion_Completa"] as string,
                            Tipo_Direccion = reader["Tipo_Direccion"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                            Activo = reader["Activo"] != DBNull.Value ? (bool?)reader["Activo"] : null,
                            Id_Tipo_Direccion = reader["Id_Tipo_Direccion"] as int?,
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Direccion", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Direccion", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Direccion", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
