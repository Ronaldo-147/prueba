public class CE_USUARIO
{
    public int? Id_Usuario { get; set; }
    public int? Id_Persona { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    public int? Id_Rol { get; set; }
    public int? Id_Estado { get; set; }
    public DateTime? Fecha_Creacion { get; set; }
    public DateTime? Fecha_Modificacion { get; set; }

    // Datos descriptivos
    public string Nombre_Completo { get; set; }
    public string Rol { get; set; }
    public string Estado_Registro { get; set; }

    // Opcionales para extensiones futuras
    // public string Email { get; set; }
    // public string AvatarUrl { get; set; }
    // public string TextoBusqueda { get; set; }
}