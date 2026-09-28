using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly CN_CATALOGO _logica;

        public CatalogoController(IConfiguration configuration)
        {
            _logica = new CN_CATALOGO(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<CE_CATALOGO>> Listar()
        {
            var resultado = _logica.LISTAR();
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("Filtrar")]
        public ActionResult<List<CE_CATALOGO>> Filtrar([FromQuery] string texto)
        {
            var resultado = _logica.FILTRAR(texto);
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CE_CatalogoRequestDto dto)
        {
            var entidad = new CE_CATALOGO
            {
                Id_Tipo_Catalogo = dto.Id_Tipo_Catalogo,
                Catalogo = dto.Catalogo
            };

            _logica.INSERTAR(dto);
            return Ok(new { mensaje = "✅ Catálogo insertado correctamente." });
        }
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_CATALOGO obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Catálogo actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al actualizar catálogo: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _logica.ELIMINAR(id);
                return Ok(new { mensaje = $"🗑 Catálogo {id} desactivado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al eliminar catálogo: {ex.Message}");
            }
        }

        [HttpPut("Restaurar/{id}")]
        public IActionResult Restaurar(int id)
        {
            try
            {
                _logica.RESTAURAR(id);
                return Ok(new { mensaje = $"🔄 Catálogo {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar catálogo: {ex.Message}");
            }
        }
    }
}