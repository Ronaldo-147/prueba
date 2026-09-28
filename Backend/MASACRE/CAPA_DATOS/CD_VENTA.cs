using System;
using System.Collections.Generic;
using System.Data;
using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CAPA_DATOS
{
    public class CD_VENTA
    {
        private readonly CD_CONEXION _conexion;

        public CD_VENTA(IConfiguration configuration)
        {
            _conexion = new CD_CONEXION(configuration);
        }

        public int INSERTAR(DTO_VentaInsertar dto)
        {
            using var conexion = _conexion.AbrirConexion(); // Ya está abierta
            using SqlCommand cmd = new SqlCommand("SP_Venta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
            cmd.Parameters.AddWithValue("@Fecha", dto.Fecha ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@Id_Cliente", dto.Id_Cliente);
            cmd.Parameters.AddWithValue("@Total", dto.Total);

            var resultado = cmd.ExecuteScalar();
            if (resultado == null || !int.TryParse(resultado.ToString(), out int idVenta))
                throw new Exception("La inserción falló o el procedimiento no devolvió un Id válido.");

            return idVenta;
        }   
        public List<CE_VENTA> LISTAR()
        {
            var lista = new List<CE_VENTA>();

            using var conexion = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Venta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Accion", "LISTAR");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new CE_VENTA
                {
                    Id_Venta = reader["Id_Venta"] as int?,
                    Fecha = reader["Fecha"] as DateTime?,
                    Id_Cliente = reader["Id_Cliente"] as int?, // ✅ Añadido
                    Nombre_Cliente = reader["Nombre_Cliente"]?.ToString(),
                    DNI_Cliente = reader["DNI_Cliente"]?.ToString(),
                    Total = reader["Total"] as decimal?
                });
            }

            return lista;
        }

        public List<CE_VENTA> FILTRAR(string texto)
        {
            var lista = new List<CE_VENTA>();

            using var conexion = _conexion.AbrirConexion();
            using var cmd = new SqlCommand("SP_Venta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
            cmd.Parameters.AddWithValue("@Busqueda", texto?.Trim() ?? "");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new CE_VENTA
                {
                    Id_Venta = reader["Id_Venta"] as int?,
                    Fecha = reader["Fecha"] as DateTime?,
                    Id_Cliente = reader["Id_Cliente"] as int?, // ✅ Añadido también acá
                    Nombre_Cliente = reader["Nombre_Cliente"]?.ToString(),
                    DNI_Cliente = reader["DNI_Cliente"]?.ToString(),
                    Total = reader["Total"] as decimal?
                });
            }

            return lista;
        }
    }
}