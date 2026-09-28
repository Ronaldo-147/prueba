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
    public class CD_CORREO
    {
        private readonly CD_CONEXION _conexion;

        public CD_CORREO(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_CORREO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Direccion_Correo", obj.Direccion_Correo ?? "");
                cmd.Parameters.AddWithValue("@Id_Tipo_Correo", obj.Id_Tipo_Correo ?? 0);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? 1);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_CORREO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Correo", obj.Id_Correo ?? 0);
                cmd.Parameters.AddWithValue("@Direccion_Correo", obj.Direccion_Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Tipo_Correo", obj.Id_Tipo_Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_CORREO> LISTAR()
        {
            var lista = new List<CE_CORREO>();

            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_CORREO
                        {
                            Id_Correo = reader["Id_Correo"] as int?,
                            Direccion_Correo = reader["Direccion_Correo"] as string,
                            Tipo_Correo = reader["Tipo_Correo"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                            Activo = reader["Activo"] != DBNull.Value ? Convert.ToInt32(reader["Activo"]) : (int?)null
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_CORREO> FILTRAR(string texto)
        {
            var lista = new List<CE_CORREO>();

            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@TextoBusqueda", texto ?? "");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_CORREO
                        {
                            Id_Correo = reader["Id_Correo"] as int?,
                            Direccion_Correo = reader["Direccion_Correo"] as string,
                            Tipo_Correo = reader["Tipo_Correo"] as string,
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                            Activo = reader["Activo"] != DBNull.Value ? Convert.ToInt32(reader["Activo"]) : (int?)null
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Correo", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Correo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Correo", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
