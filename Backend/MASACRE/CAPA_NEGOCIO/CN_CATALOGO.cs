using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_CATALOGO
    {
        private readonly CD_CATALOGO _cd;

        public CN_CATALOGO(IConfiguration configuration)
        {
            _cd = new CD_CATALOGO(configuration);
        }

        public void INSERTAR(CE_CatalogoRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Catalogo))
                throw new ArgumentException("El nombre del catálogo no puede estar vacío.");

            if (dto.Id_Tipo_Catalogo == null || dto.Id_Tipo_Catalogo == 0)
                throw new ArgumentException("Debe seleccionar un tipo de catálogo válido.");

            _cd.INSERTAR(dto);
        }

        public void ACTUALIZAR(CE_CATALOGO obj)
        {
            if (obj.Id_Catalogo == null || obj.Id_Catalogo == 0)
                throw new ArgumentException("Se requiere el Id del catálogo para actualizar.");

            _cd.ACTUALIZAR(obj);
        }

        public List<CatalogoResponseDto> LISTAR()
        {
            return _cd.LISTAR().Select(c => new CatalogoResponseDto
            {
                Id_Catalogo = c.Id_Catalogo ?? 0,
                Id_Tipo_Catalogo = c.Id_Tipo_Catalogo ?? 0,
                Catalogo = string.IsNullOrWhiteSpace(c.Catalogo) ? "[Sin nombre]" : c.Catalogo,
                Fecha_Creacion = c.Fecha_Creacion?.ToString("yyyy-MM-dd") ?? "",
                Fecha_Modificacion = c.Fecha_Modificacion?.ToString("yyyy-MM-dd") ?? "",
                Activo = (c.Activo ?? 0) == 1 ? 1 : 0
            }).ToList();
        }



        public List<CE_CATALOGO> FILTRAR(string texto)
        {
            return _cd.FILTRAR(texto);
        }

        public void ELIMINAR(int id)
        {
            _cd.ELIMINAR(id);
        }

        public void RESTAURAR(int id)
        {
            _cd.RESTAURAR(id);
        }
    }

}
