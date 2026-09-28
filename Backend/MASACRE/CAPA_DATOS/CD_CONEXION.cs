using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CAPA_DATOS
{
    public class CD_CONEXION
    {
        // Cadena de conexión obtenida desde appsettings.json u otro proveedor
        private readonly string _conexionString;

        // Objeto de conexión a SQL Server
        private SqlConnection _connection;

        // Constructor que recibe la configuración inyectada
        public CD_CONEXION(IConfiguration configuration)
        {
            _conexionString = configuration.GetConnectionString("CnxSql");
            _connection = new SqlConnection(_conexionString);
        }

        // Método para abrir la conexión
        public SqlConnection AbrirConexion()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            return _connection;
        }

        // Método para cerrar la conexión
        public SqlConnection CerrarConexion()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

            return _connection;
        }
    }
}