using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;
using static CAPA_ENTIDADES.CE_BODEGA;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodegaController : ControllerBase
    {
        private readonly CN_BODEGA _logica;

        public BodegaController(IConfiguration configuration)
        {
            _logica = new CN_BODEGA(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<BodegaResponseDto>> Listar()
        {
            var resultado = _logica.LISTAR();
            return resultado.Count == 0 ? NoContent() : Ok(resultado);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string textoBusqueda)
        {
            var resultado = _logica.FILTRAR(new BodegaFiltroDto { TextoBusqueda = textoBusqueda });
            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CE_BODEGA dto)
        {
            try
            {
                _logica.INSERTAR(dto);
                return Ok(new { mensaje = "✅ Registro insertado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al insertar: {ex.Message}");
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_BODEGA obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Registro de bodega actualizado correctamente." });
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
                return Ok(new { mensaje = $"🗑 Registro {id} inactivado correctamente." });
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
                return Ok(new { mensaje = $"🔄 Registro {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}