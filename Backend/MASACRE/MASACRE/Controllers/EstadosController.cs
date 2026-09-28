
using Microsoft.AspNetCore.Mvc;
using CAPA_NEGOCIO;
using CAPA_ENTIDADES;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly CN_ESTADO _estadoService;

        public EstadosController(IConfiguration configuration)
        {
            _estadoService = new CN_ESTADO(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<CE_ESTADO>> Listar()
        {
            var resultado = _estadoService.CN_LISTAR_ESTADOS();
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("Filtrar")]
        public ActionResult<List<CE_ESTADO>> Filtrar([FromQuery] string texto)
        {
            var resultado = _estadoService.CN_FILTRAR_ESTADOS(texto);
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_EstadoInsertar dto)
        {
            try
            {
                _estadoService.InsertarDesdeDTO(dto);

                return Ok(new
                {
                    mensaje = "✅ Estado insertado correctamente.",
                    estado = new
                    {
                        estado = dto.Estado,
                        activo = dto.Activo
                    }
                });
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

        //[HttpPut("Actualizar")]
        //public IActionResult Actualizar([FromBody] CE_ESTADO estado)
        //{
        //    try
        //    {
        //        _estadoService.ACTUALIZAR_ESTADO(estado);
        //        return Ok(new { mensaje = "✅ Estado actualizado correctamente." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"❌ Error al actualizar estado: {ex.Message}");
        //    }
        //}
    }
}