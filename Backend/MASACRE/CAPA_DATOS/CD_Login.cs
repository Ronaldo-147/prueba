using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CAPA_ENTIDADES;

namespace CAPA_DATOS
{
    public class CD_Login
    {
        private readonly string _conexion;

        public CD_Login(IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Configuración no proporcionada.");

            _conexion = config.GetConnectionString("CnxSql");
            if (string.IsNullOrWhiteSpace(_conexion))
                throw new InvalidOperationException("Cadena de conexión 'CnxSql' no está definida en appsettings.json.");
        }

        /// <summary>
        /// Autentica al usuario y devuelve su información si es válido.
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasena">Contraseña</param>
        /// <returns>Entidad CE_USUARIO o null si credenciales inválidas</returns>
        public CE_USUARIO ConsultarPorCredenciales(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("Usuario o contraseña vacíos.");

            try
            {
                using var cn = new SqlConnection(_conexion);
                using var cmd = new SqlCommand("SP_Login_ConsultarCompleto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Usuario", usuario.Trim());
                cmd.Parameters.AddWithValue("@Contrasena", contrasena.Trim());

                cn.Open();
                using var reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                return new CE_USUARIO
                {
                    Id_Usuario = reader["Id_Usuario"] as int?,
                    Id_Persona = reader["Id_Persona"] as int?,
                    Usuario = reader["Usuario"]?.ToString(),
                    Id_Rol = reader["Id_Rol"] as int?,
                    Id_Estado = reader["Id_Estado"] as int?,
                    Fecha_Creacion = reader["Fecha_Creacion"] as DateTime?,
                    Fecha_Modificacion = reader["Fecha_Modificacion"] as DateTime?,
                    Nombre_Completo = reader["Nombre_Completo"]?.ToString(),
                    Rol = reader["Rol"]?.ToString(),
                    Estado_Registro = reader["Estado_Registro"]?.ToString()
                };
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"❌ Error SQL en SP_Login_ConsultarCompleto: {sqlEx.Message}");
                // Opcional: logear en BD o archivo
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Error inesperado en ConsultarPorCredenciales: {ex.Message}");
                throw;
            }
        }
    }
}