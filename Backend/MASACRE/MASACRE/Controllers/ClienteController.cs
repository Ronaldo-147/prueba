using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using Microsoft.AspNetCore.Mvc;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly CN_CLIENTE _logica;

        public ClienteController(IConfiguration configuration)
        {
            _logica = new CN_CLIENTE(configuration);
        }

        [HttpGet("Listar")]
        [ProducesResponseType(typeof(List<CE_CLIENTE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CE_CLIENTE>> Listar()
        {
            var resultado = _logica.LISTAR();

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("Filtrar")]
        [ProducesResponseType(typeof(List<CE_CLIENTE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CE_CLIENTE>> Filtrar([FromQuery] string texto)
        {
            var resultado = _logica.FILTRAR(texto);

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public IActionResult Insertar([FromBody] CE_CLIENTE obj)
        {
            try
            {
                string mensaje = _logica.INSERTAR(obj);

                if (mensaje.Contains("Ya existe"))
                    return Conflict(new { mensaje = $"⚠️ {mensaje}" });

                return Ok(new { mensaje = $"✅ {mensaje}" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = $"⚠️ {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"❌ Error al insertar cliente: {ex.Message}" });
            }
        }

        [HttpPut("Actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Actualizar([FromBody] CE_CLIENTE obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Cliente actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al actualizar: {ex.Message}");
            }
        }

        [HttpDelete("Eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Eliminar(int id)
        {
            try
            {
                _logica.ELIMINAR(id);
                return Ok(new { mensaje = $"🗑 Cliente {id} inactivado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al eliminar: {ex.Message}");
            }
        }

        [HttpPut("Restaurar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Restaurar(int id)
        {
            try
            {
                _logica.RESTAURAR(id);
                return Ok(new { mensaje = $"🔄 Cliente {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}