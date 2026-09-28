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
    public class CD_TIPO_CATALOGO
    {
        private readonly CD_CONEXION _conexion;

        public CD_TIPO_CATALOGO(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_TIPO_CATALOGO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Tipo_Catalogo_Nombre", obj.Tipo_Catalogo ?? "");
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_TIPO_CATALOGO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Tipo_Catalogo", obj.Id_Tipo_Catalogo ?? 0);
                cmd.Parameters.AddWithValue("@Tipo_Catalogo_Nombre", obj.Tipo_Catalogo ?? "");
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_TIPO_CATALOGO> LISTAR()
        {
            var lista = new List<CE_TIPO_CATALOGO>();

            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_TIPO_CATALOGO
                        {
                            Id_Tipo_Catalogo = reader["Id_Tipo_Catalogo"] as int?,
                            Tipo_Catalogo = reader["Tipo_Catalogo"] as string,
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

        public List<CE_TIPO_CATALOGO> FILTRAR(string texto, int porID, int porEstado)
        {
            var lista = new List<CE_TIPO_CATALOGO>();

            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_TIPO_CATALOGO
                        {
                            Id_Tipo_Catalogo = reader["Id_Tipo_Catalogo"] as int?,
                            Tipo_Catalogo = reader["Tipo_Catalogo"] as string,
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
            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Tipo_Catalogo", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Tipo_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Tipo_Catalogo", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
