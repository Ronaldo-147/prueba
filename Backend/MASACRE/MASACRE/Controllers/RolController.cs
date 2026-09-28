using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly CN_ROL _logica;

        public RolController(IConfiguration configuration)
        {
            _logica = new CN_ROL(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<CE_ROL>> Listar()
        {
            var resultado = _logica.LISTAR();
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("Filtrar")]
        public ActionResult<List<CE_ROL>> Filtrar([FromQuery] string texto)
        {
            var resultado = _logica.FILTRAR(texto);
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_RolInsertar dto)
        {
            try
            {
                var rol = new CE_ROL
                {
                    Rol = dto.Rol,
                    Id_Estado = dto.Id_Estado ?? 0 // Asignar 'Activo' si no envían valor
                };

                _logica.INSERTAR(rol);
                return Ok(new { mensaje = "✅ Rol insertado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "❌ Error inesperado: " + ex.Message });
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_ROL obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Rol actualizado correctamente." });
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
                return Ok(new { mensaje = $"🗑 Rol {id} inactivado correctamente." });
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
                return Ok(new { mensaje = $"🔄 Rol {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}