namespace CAPA_ENTIDADES
{
    public class CE_CORREO
    {
        public int? Id_Correo { get; set; }
        public string Direccion_Correo { get; set; }
        public int? Id_Tipo_Correo { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Activo { get; set; }

        // Visualización legible
        public string? Tipo_Correo { get; set; }

        // Para filtros o login
        // public string TextoBusqueda { get; set; }
    }
}
