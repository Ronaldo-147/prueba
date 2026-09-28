namespace CAPA_ENTIDADES
{
    public class CE_CLIENTE
    {
        public int? Id_Cliente { get; set; }
        public int? Id_Persona { get; set; }
        public int? Id_Estado { get; set; }

        // Opcionales para visualización
        //public string Estado { get; set; }
        public string? Nombre_Completo { get; set; }

        //public string TextoBusqueda { get; set; }
    }
}
