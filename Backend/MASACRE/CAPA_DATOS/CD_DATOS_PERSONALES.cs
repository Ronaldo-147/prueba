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
    public class CD_DATOS_PERSONALES
    {
        private readonly CD_CONEXION _conexion;

        public CD_DATOS_PERSONALES(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public CE_DATOS_PERSONALES INSERTAR(CE_DATOS_PERSONALES obj)
        {
            CE_DATOS_PERSONALES nuevo = null;

            using (var cmd = new SqlCommand("SP_Datos_Personales", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
                cmd.Parameters.AddWithValue("@Primer_Nombre", obj.Primer_Nombre ?? "");
                cmd.Parameters.AddWithValue("@Segundo_Nombre", obj.Segundo_Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Primer_Apellido", obj.Primer_Apellido ?? "");
                cmd.Parameters.AddWithValue("@Segundo_Apellido", obj.Segundo_Apellido ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Tipo_DNI", obj.Id_Tipo_DNI ?? 0);
                cmd.Parameters.AddWithValue("@DNI", obj.DNI ?? "");
                cmd.Parameters.AddWithValue("@Id_Genero", obj.Id_Genero ?? 0);
                cmd.Parameters.AddWithValue("@Id_Nacionalidad", obj.Id_Nacionalidad ?? 0);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nuevo = new CE_DATOS_PERSONALES
                        {
                            Id_Persona = reader["Id_Persona"] as int?,
                            Primer_Nombre = reader["Primer_Nombre"]?.ToString(),
                            Segundo_Nombre = reader["Segundo_Nombre"]?.ToString(),
                            Primer_Apellido = reader["Primer_Apellido"]?.ToString(),
                            Segundo_Apellido = reader["Segundo_Apellido"]?.ToString(),
                            DNI = reader["DNI"]?.ToString(),
                            Id_Tipo_DNI = reader["Id_Tipo_DNI"] as int?,
                            Tipo_DNI = reader["Tipo_DNI"]?.ToString(),
                            Id_Genero = reader["Id_Genero"] as int?,
                            Genero = reader["Genero"]?.ToString(),
                            Id_Nacionalidad = reader["Id_Nacionalidad"] as int?,
                            Nacionalidad = reader["Nacionalidad"]?.ToString(),
                            Id_Estado = reader["Id_Estado"] as int?,
                            Estado_Registro = reader["Estado_Registro"]?.ToString(),
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?
                        };
                    }
                }

                _conexion.CerrarConexion();
            }

            return nuevo;
        }

        public void ACTUALIZAR(CE_DATOS_PERSONALES obj)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Datos_Personales", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
                cmd.Parameters.AddWithValue("@Id_Persona", obj.Id_Persona ?? 0);
                cmd.Parameters.AddWithValue("@Primer_Nombre", obj.Primer_Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Segundo_Nombre", obj.Segundo_Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Primer_Apellido", obj.Primer_Apellido ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Segundo_Apellido", obj.Segundo_Apellido ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Tipo_DNI", obj.Id_Tipo_DNI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DNI", obj.DNI ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Genero", obj.Id_Genero ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Nacionalidad", obj.Id_Nacionalidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public List<CE_DATOS_PERSONALES> LISTAR()
        {
            var lista = new List<CE_DATOS_PERSONALES>();

            using (var conexion = _conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_Datos_Personales", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "LISTAR");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_DATOS_PERSONALES
                        {
                            Id_Persona = reader["Id_Persona"] != DBNull.Value ? Convert.ToInt32(reader["Id_Persona"]) : (int?)null,
                            Primer_Nombre = reader["Primer_Nombre"]?.ToString(),
                            Segundo_Nombre = reader["Segundo_Nombre"]?.ToString(),
                            Primer_Apellido = reader["Primer_Apellido"]?.ToString(),
                            Segundo_Apellido = reader["Segundo_Apellido"]?.ToString(),

                            Id_Tipo_DNI = reader["Id_Tipo_DNI"] != DBNull.Value ? Convert.ToInt32(reader["Id_Tipo_DNI"]) : (int?)null,
                            Tipo_DNI = reader["Tipo_DNI"]?.ToString(),

                            DNI = reader["DNI"]?.ToString(),

                            Id_Genero = reader["Id_Genero"] != DBNull.Value ? Convert.ToInt32(reader["Id_Genero"]) : (int?)null,
                            Genero = reader["Genero"]?.ToString(),

                            Id_Nacionalidad = reader["Id_Nacionalidad"] != DBNull.Value ? Convert.ToInt32(reader["Id_Nacionalidad"]) : (int?)null,
                            Nacionalidad = reader["Nacionalidad"]?.ToString(),

                            Id_Estado = reader["Id_Estado"] != DBNull.Value ? Convert.ToInt32(reader["Id_Estado"]) : (int?)null,
                            Estado_Registro = reader["Estado_Registro"]?.ToString(),

                            Fecha_Creacion = reader["Fecha_Creacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Creacion"]) : (DateTime?)null,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null
                        });
                    }
                }
            }

            return lista;
        }

        public List<CE_DATOS_PERSONALES> FILTRAR(string texto)
        {
            var lista = new List<CE_DATOS_PERSONALES>();

            using (var conexion = _conexion.AbrirConexion())
            using (var cmd = new SqlCommand("SP_Datos_Personales", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@TextoBusqueda", texto ?? "");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CE_DATOS_PERSONALES
                        {
                            Id_Persona = reader["Id_Persona"] as int?,
                            Primer_Nombre = reader["Primer_Nombre"]?.ToString(),
                            Segundo_Nombre = reader["Segundo_Nombre"]?.ToString(),
                            Primer_Apellido = reader["Primer_Apellido"]?.ToString(),
                            Segundo_Apellido = reader["Segundo_Apellido"]?.ToString(),
                            Id_Tipo_DNI = reader["Id_Tipo_DNI"] as int?,
                            Tipo_DNI = reader["Tipo_DNI"]?.ToString(),
                            DNI = reader["DNI"]?.ToString(),
                            Id_Genero = reader["Id_Genero"] as int?,
                            Genero = reader["Genero"]?.ToString(),
                            Id_Nacionalidad = reader["Id_Nacionalidad"] as int?,
                            Nacionalidad = reader["Nacionalidad"]?.ToString(),
                            Id_Estado = reader["Id_Estado"] as int?,
                            Estado_Registro = reader["Estado_Registro"]?.ToString(),
                            Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?
                        });
                    }
                }
            }

            return lista;
        }


        public void ELIMINAR(int idPersona)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Datos_Personales", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
                cmd.Parameters.AddWithValue("@Id_Persona", idPersona);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }

        public void RESTAURAR(int idPersona)
        {
            using (SqlCommand cmd = new SqlCommand("SP_Datos_Personales", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
                cmd.Parameters.AddWithValue("@Id_Persona", idPersona);
                cmd.ExecuteNonQuery();
                _conexion.CerrarConexion();
            }
        }
    }

}
