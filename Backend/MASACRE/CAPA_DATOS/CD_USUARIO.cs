using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CAPA_ENTIDADES;
using CAPA_DATOS;

public class CD_USUARIO
{
    private readonly CD_CONEXION _conexion;

    public CD_USUARIO(IConfiguration config)
    {
        _conexion = new CD_CONEXION(config);
    }

    public string Insertar(CE_USUARIO obj)
    {
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@Accion", "INSERTAR");
        cmd.Parameters.AddWithValue("@Id_Persona", obj.Id_Persona);
        cmd.Parameters.AddWithValue("@Usuario_Login", obj.Usuario);
        cmd.Parameters.AddWithValue("@Id_Rol", obj.Id_Rol);
        cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena);
        cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

        using var dr = cmd.ExecuteReader();
        return dr.Read() && dr["Mensaje"] != DBNull.Value
            ? dr["Mensaje"].ToString()
            : "Sin respuesta del servidor.";
    }

    public List<CE_USUARIO> Listar()
    {
        var lista = new List<CE_USUARIO>();
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Accion", "LISTAR");

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lista.Add(MapearUsuario(dr));
        }

        return lista;
    }

    public List<CE_USUARIO> Filtrar(string filtro)
    {
        var lista = new List<CE_USUARIO>();
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Accion", "FILTRAR");
        cmd.Parameters.AddWithValue("@Busqueda", filtro?.Trim() ?? "");

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lista.Add(MapearUsuario(dr));
        }

        return lista;
    }

    public string Eliminar(int idUsuario)
    {
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Accion", "ELIMINAR");
        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

        using var dr = cmd.ExecuteReader();
        return dr.Read() && dr["Mensaje"] != DBNull.Value
            ? dr["Mensaje"].ToString()
            : "No se pudo eliminar.";
    }

    public string Restaurar(int idUsuario)
    {
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@Accion", "RESTAURAR");
        cmd.Parameters.AddWithValue("@Id_Usuario", idUsuario);

        using var dr = cmd.ExecuteReader();
        return dr.Read() && dr["Mensaje"] != DBNull.Value
            ? dr["Mensaje"].ToString()
            : "No se pudo restaurar.";
    }

    // 🧠 Mapeo centralizado
    private CE_USUARIO MapearUsuario(SqlDataReader dr)
    {
        return new CE_USUARIO
        {
            Id_Usuario = dr["Id_Usuario"] != DBNull.Value ? Convert.ToInt32(dr["Id_Usuario"]) : null,
            Usuario = dr["Usuario"]?.ToString(),
            Nombre_Completo = dr["Nombre_Completo"]?.ToString(),
            Rol = dr["Rol"]?.ToString(),
            Estado_Registro = dr["Estado_Registro"]?.ToString()
        };
    }

    public string Actualizar(CE_USUARIO obj)
    {
        using var cn = _conexion.AbrirConexion();
        using var cmd = new SqlCommand("SP_Usuario", cn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@Accion", "ACTUALIZAR");
        cmd.Parameters.AddWithValue("@Id_Usuario", obj.Id_Usuario);
        cmd.Parameters.AddWithValue("@Id_Persona", obj.Id_Persona ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Usuario_Login", obj.Usuario ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Id_Rol", obj.Id_Rol ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Contrasena", obj.Contrasena ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Id_Estado", obj.Id_Estado ?? (object)DBNull.Value);

        using var dr = cmd.ExecuteReader();
        return dr.Read() && dr["Mensaje"] != DBNull.Value
            ? dr["Mensaje"].ToString()
            : "No se pudo actualizar el usuario.";
    }
}