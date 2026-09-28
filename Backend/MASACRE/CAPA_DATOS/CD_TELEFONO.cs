using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_ENTIDADES.CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CAPA_DATOS
{
    public class CD_TELEFONO
    {
        private readonly CD_CONEXION _conexion;

        public CD_TELEFONO(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_TELEFONO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Numero_Telefono", obj.Numero_Telefono ?? "");
                cmd.Parameters.AddWithValue("@Id_Tipo_Telefono", obj.Id_Tipo_Telefono ?? 0);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_TELEFONO obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Telefono", obj.Id_Telefono ?? 0);
                cmd.Parameters.AddWithValue("@Numero_Telefono", obj.Numero_Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Tipo_Telefono", obj.Id_Tipo_Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_TELEFONO> LISTAR()
        {
            var lista = new List<CE_TELEFONO>();

            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_TELEFONO
                        {
                            Id_Telefono = reader["Id_Telefono"] as int?,
                            Numero_Telefono = reader["Numero_Telefono"] as string,
                            Id_Tipo_Telefono = reader["Id_Tipo_Telefono"] != DBNull.Value ? Convert.ToInt32(reader["Id_Tipo_Telefono"]) : (int?)null,
                            Tipo_Telefono = reader["Tipo_Telefono"] as string,
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

        public List<CE_TELEFONO> FILTRAR(string texto, int filtrarPorNumero, int filtrarPorTipo)
        {
            var lista = new List<CE_TELEFONO>();

            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto);
                cmd.Parameters.AddWithValue("@FiltrarPorNumero", filtrarPorNumero); // 👈 como int
                cmd.Parameters.AddWithValue("@FiltrarPorTipo", filtrarPorTipo);     // 👈 como int

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_TELEFONO
                        {
                            Id_Telefono = reader["Id_Telefono"] as int?,
                            Numero_Telefono = reader["Numero_Telefono"] as string,
                            Id_Tipo_Telefono = reader["Id_Tipo_Telefono"] != DBNull.Value ? Convert.ToInt32(reader["Id_Tipo_Telefono"]) : (int?)null,
                            Tipo_Telefono = reader["Tipo_Telefono"] as string,
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
            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Telefono", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Telefono", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Telefono", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
