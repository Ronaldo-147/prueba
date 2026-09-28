using System.Data;
using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CAPA_DATOS
{
    public class CD_CATALOGO
    {
        private readonly CD_CONEXION _conexion;

        public CD_CATALOGO(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public void INSERTAR(CE_CatalogoRequestDto obj)
        {
            using var connection = _conexion.AbrirConexion();

            using var cmd = new SqlCommand("SP_Catalogo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
            cmd.Parameters.AddWithValue("@Id_Tipo_Catalogo", obj.Id_Tipo_Catalogo);
            cmd.Parameters.AddWithValue("@Catalogo_Nombre", obj.Catalogo?.Trim() ?? string.Empty);

            // Evitás errores si Activo es null
            //cmd.Parameters.AddWithValue("@Activo", obj.Activo ?? true);

            cmd.ExecuteNonQuery();

            _conexion.CerrarConexion(); // Opcional si AbrirConexion() no crea conexión persistente
        }

        public void ACTUALIZAR(CE_CATALOGO obj)
        {
            using var connection = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Catalogo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
            cmd.Parameters.AddWithValue("@Id_Catalogo", obj.Id_Catalogo ?? 0);
            cmd.Parameters.AddWithValue("@Id_Tipo_Catalogo", obj.Id_Tipo_Catalogo ?? 0);
            cmd.Parameters.AddWithValue("@Catalogo_Nombre", obj.Catalogo ?? string.Empty);
            cmd.Parameters.AddWithValue("@Activo", obj.Activo == 1); // si Activo es int?

            cmd.ExecuteNonQuery();
            _conexion.CerrarConexion();
        }

        public List<CE_CATALOGO> LISTAR()
        {
            var lista = new List<CE_CATALOGO>();

            using var connection = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Catalogo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Accion", "LISTAR");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new CE_CATALOGO
                {
                    Id_Catalogo = reader["Id_Catalogo"] != DBNull.Value
                        ? Convert.ToInt32(reader["Id_Catalogo"])
                        : (int?)null,

                    Id_Tipo_Catalogo = reader["Id_Tipo_Catalogo"] != DBNull.Value
                        ? Convert.ToInt32(reader["Id_Tipo_Catalogo"])
                        : (int?)null,

                    Catalogo = reader["Catalogo"]?.ToString()?.Trim() ?? "[Sin nombre]",

                    Fecha_Creacion = reader["Fecha_Creacion"] != DBNull.Value
                        ? Convert.ToDateTime(reader["Fecha_Creacion"])
                        : (DateTime?)null,

                    Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value
                        ? Convert.ToDateTime(reader["Fecha_Modificacion"])
                        : (DateTime?)null,

                    Activo = reader["Activo"] != DBNull.Value
                        ? (Convert.ToBoolean(reader["Activo"]) ? 1 : 0)
                        : 0
                });
            }

            _conexion.CerrarConexion();
            return lista;
        }

        public List<CE_CATALOGO> FILTRAR(string texto)
        {
            var lista = new List<CE_CATALOGO>();

            using (SqlCommand cmd = new SqlCommand("SP_Catalogo", _conexion.AbrirConexion()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
                cmd.Parameters.AddWithValue("@Busqueda", texto ?? string.Empty);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var catalogo = new CE_CATALOGO
                        {
                            Id_Catalogo = reader["Id_Catalogo"] != DBNull.Value ? Convert.ToInt32(reader["Id_Catalogo"]) : (int?)null,
                            Id_Tipo_Catalogo = reader["Id_Tipo_Catalogo"] != DBNull.Value ? Convert.ToInt32(reader["Id_Tipo_Catalogo"]) : (int?)null,
                            Catalogo = reader["Catalogo"]?.ToString()?.Trim() ?? "[Sin nombre]",
                            Fecha_Creacion = reader["Fecha_Creacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Creacion"]) : (DateTime?)null,
                            Fecha_Modificacion = reader["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Modificacion"]) : (DateTime?)null,
                            Activo = reader["Activo"] != DBNull.Value ? (Convert.ToBoolean(reader["Activo"]) ? 1 : 0) : 0
                        };

                        lista.Add(catalogo);
                    }
                }

                _conexion.CerrarConexion();
            }

            return lista;
        }

        public void ELIMINAR(int id)
        {
            using var connection = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Catalogo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
            cmd.Parameters.AddWithValue("@Id_Catalogo", id);

            cmd.ExecuteNonQuery();
            _conexion.CerrarConexion();
        }

        public void RESTAURAR(int id)
        {
            using var connection = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Catalogo", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
            cmd.Parameters.AddWithValue("@Id_Catalogo", id);

            cmd.ExecuteNonQuery();
            _conexion.CerrarConexion();
        }
    }
}