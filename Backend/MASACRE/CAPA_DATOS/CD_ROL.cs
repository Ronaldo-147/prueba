using System;
using System.Collections;
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
    public class CD_ROL
    {
        private readonly CD_CONEXION _conexion;

        public CD_ROL(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_ROL obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Rol_Nombre", obj.Rol ?? "");
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_ROL obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Rol", obj.Id_Rol ?? 0);
                cmd.Parameters.AddWithValue("@Rol_Nombre", obj.Rol ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_ROL> LISTAR()
        {
            var lista = new List<CE_ROL>();

            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_ROL
                        {
                            Id_Rol = reader["Id_Rol"] as int?,
                            Rol = reader["Rol"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                            Id_Estado = reader["Id_Estado"] != DBNull.Value ? Convert.ToInt32(reader["Id_Estado"]) : (int?)null,
                            Estado = reader["Estado_Registro"] as string
                            // 🔥 No incluir TextoBusqueda
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_ROL> FILTRAR(string texto)
        {
            var lista = new List<CE_ROL>();

            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_ROL
                        {
                            Id_Rol = reader["Id_Rol"] as int?,
                            Rol = reader["Rol"] as string,
                            Id_Estado = reader["Id_Estado"] != DBNull.Value ? Convert.ToInt32(reader["Id_Estado"]) : (int?)null,
                            Estado = reader["Estado_Registro"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Rol", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Roles", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Rol", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
