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
    
        public class CD_ESTADO
        {
            private readonly CD_CONEXION _conexion;

            public CD_ESTADO(IConfiguration configuration)
            {
                _conexion = new CD_CONEXION(configuration);
            }

        public void INSERTAR_ESTADO(CE_ESTADO estado)
        {
            using var conexion = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_EstadoS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
            cmd.Parameters.AddWithValue("@Estado_Nombre", estado.Estado ?? "");
            cmd.Parameters.AddWithValue("@Activo", estado.Activo); // Enviado como int, compatible con BIT

            cmd.ExecuteNonQuery();
        }


        public void ACTUALIZAR_ESTADO(CE_ESTADO estado)
            {
                using (SqlCommand cmd = new SqlCommand("SP_Estado", _conexion.AbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                    cmd.Parameters.AddWithValue("@Id_Estado", estado.Id_Estado ?? 0);
                    cmd.Parameters.AddWithValue("@Estado_Nombre", estado.Estado ?? "");
                    cmd.Parameters.AddWithValue("@Activo", estado.Activo );

                    cmd.ExecuteNonQuery();
                }

                _conexion.CerrarConexion();
            }

            public List<CE_ESTADO> LISTAR_ESTADOS()
            {
                List<CE_ESTADO> lista = new List<CE_ESTADO>();

                using (SqlCommand cmd = new SqlCommand("SP_EstadoS", _conexion.AbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CE_ESTADO
                            {
                                Id_Estado = reader["Id_Estado"] as int?,
                                Estado = reader["Estado"] as string,
                                Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                                Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                                Activo = Convert.ToInt32(reader["Activo"])
                            });
                        }
                    }
                }

                _conexion.CerrarConexion();
                return lista;
            }

            public List<CE_ESTADO> FILTRAR_ESTADOS(string texto)
            {
                List<CE_ESTADO> lista = new List<CE_ESTADO>();

                using (SqlCommand cmd = new SqlCommand("SP_EstadoS", _conexion.AbrirConexion()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                    cmd.Parameters.AddWithValue("@Busqueda", texto ?? "");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CE_ESTADO
                            {
                                Id_Estado = reader["Id_Estado"] as int?,
                                Estado = reader["Estado"] as string,
                                Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                                Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                                Activo = Convert.ToInt32(reader["Activo"])
                            });
                        }
                    }
                }

                _conexion.CerrarConexion();
                return lista;
            }

        }
}
