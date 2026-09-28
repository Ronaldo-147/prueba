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
    public class CD_CLIENTE
    {
        private readonly CD_CONEXION _conexion;

        public CD_CLIENTE(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public string INSERTAR(CE_CLIENTE obj)
        {
            string mensaje = string.Empty;

            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Id_Persona", obj.Id_Persona ?? 0);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mensaje = reader["Mensaje"] as string;
                    }
                }

                _conexion.CerrarConexion();
            }

            return mensaje;
        }

        public void ACTUALIZAR(CE_CLIENTE obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Cliente", obj.Id_Cliente ?? 0);
                cmd.Parameters.AddWithValue("@Id_Persona", obj.Id_Persona ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_CLIENTE> LISTAR()
        {
            var lista = new List<CE_CLIENTE>();

            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_CLIENTE
                        {
                            Id_Cliente = reader["Id_Cliente"] as int?,
                            Id_Persona = reader["Id_Persona"] as int?,
                            Id_Estado = reader["Id_Estado"] as int?,
                            //Estado = reader["Estado_Registro"] as string,
                            Nombre_Completo = reader["Nombre_Cliente"] as string,
                            ////TextoBusqueda = reader["DNI"] as string




                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_CLIENTE> FILTRAR(string texto)
        {
            var lista = new List<CE_CLIENTE>();

            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? string.Empty);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new CE_CLIENTE
                        {
                            Id_Cliente = reader["Id_Cliente"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Id_Cliente"]) : null,
                            Id_Persona = reader["Id_Persona"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Id_Persona"]) : null,
                            Id_Estado = reader["Id_Estado"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Id_Estado"]) : null,
                            Nombre_Completo = reader["Nombre_Cliente"]?.ToString()
                        };

                        lista.Add(cliente);
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Cliente", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Cliente", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Cliente", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
