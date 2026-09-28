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
    public class CD_PROVEEDOR
    {
        private readonly CD_CONEXION _conexion;

        public CD_PROVEEDOR(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_PROVEEDOR obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Nombre_Proveedor", obj.Nombre_Proveedor ?? "");
                cmd.Parameters.AddWithValue("@RUC", obj.RUC ?? "");
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void ACTUALIZAR(CE_PROVEEDOR obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Proveedor", obj.Id_Proveedor ?? 0);
                cmd.Parameters.AddWithValue("@Nombre_Proveedor", obj.Nombre_Proveedor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RUC", obj.RUC ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_PROVEEDOR> LISTAR()
        {
            var lista = new List<CE_PROVEEDOR>();

            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_PROVEEDOR
                        {
                            Id_Proveedor = reader["Id_Proveedor"] as int?,
                            Nombre_Proveedor = reader["Nombre_Proveedor"] as string,
                            RUC = reader["RUC"] as string,
                            Id_Estado = reader["Id_Estado"] != DBNull.Value ? Convert.ToInt32(reader["Id_Estado"]) : (int?)null,
                            Estado = reader["Estado_Registro"] as string
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public List<CE_PROVEEDOR> FILTRAR(string texto)
        {
            var lista = new List<CE_PROVEEDOR>();

            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@TextoBusqueda", texto ?? "");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_PROVEEDOR
                        {
                            Id_Proveedor = reader["Id_Proveedor"] as int?,
                            Nombre_Proveedor = reader["Nombre_Proveedor"] as string,
                            RUC = reader["RUC"] as string,
                            Estado = reader["Estado_Registro"] as string
                        });
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Proveedor", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int id)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Proveedor", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Proveedor", id);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
