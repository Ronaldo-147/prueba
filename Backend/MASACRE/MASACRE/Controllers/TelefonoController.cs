using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using CAPA_ENTIDADES.CAPA_ENTIDADES;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefonoController : ControllerBase
    {
        private readonly CN_TELEFONO _logica;

        public TelefonoController(IConfiguration configuration)
        {
            _logica = new CN_TELEFONO(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<CE_TELEFONO>> Listar()
        {
            var resultado = _logica.LISTAR();
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }
        [HttpGet("Filtrar")]
        public IActionResult Filtrar(
      [FromQuery] string texto = "",
      [FromQuery] int porNumero = 1,
      [FromQuery] int porTipo = 1)
        {
            var resultado = _logica.FILTRAR(texto, porNumero, porTipo);

            if (resultado == null || resultado.Count == 0)
                return Ok(new { mensaje = "SIN RESULTADOS" });

            return Ok(resultado);
        }


        [HttpPost("Insertar")]
        public IActionResult Insertar(DTO_TelefonoInsertar dto)
        {
            var entidad = new CE_TELEFONO
            {
                Numero_Telefono = dto.Numero_Telefono,
                Id_Tipo_Telefono = dto.Id_Tipo_Telefono,
                Activo = dto.Activo ?? 1 // Por defecto activo
            };

            _logica.INSERTAR(entidad);

            return Ok(new { mensaje = "Teléfono insertado correctamente." });
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_TELEFONO obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Teléfono actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al actualizar: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _logica.ELIMINAR(id);
                return Ok(new { mensaje = $"🗑 Teléfono {id} inactivado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al eliminar: {ex.Message}");
            }
        }

        [HttpPut("Restaurar/{id}")]
        public IActionResult Restaurar(int id)
        {
            try
            {
                _logica.RESTAURAR(id);
                return Ok(new { mensaje = $"🔄 Teléfono {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}
