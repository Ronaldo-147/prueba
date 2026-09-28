using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly CN_VENTA _logica;

        public VentaController(IConfiguration configuration)
        {
            _logica = new CN_VENTA(configuration);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_VentaInsertar dto)
        {
            try
            {
                int idVenta = _logica.INSERTAR(dto);
                return Ok(new { mensaje = "✅ Venta registrada correctamente", IdVenta = idVenta });
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

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var resultado = _logica.LISTAR();
            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(v => new
            {
                v.Id_Venta,
                v.Fecha,
                v.Id_Cliente,
                v.Nombre_Cliente,
                v.DNI_Cliente,
                v.Total
            });

            return Ok(salida);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string texto = "")
        {
            var resultado = _logica.FILTRAR(texto);

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(v => new
            {
                v.Id_Venta,
                v.Fecha,
                v.Id_Cliente,
                v.Nombre_Cliente,
                v.DNI_Cliente,
                v.Total
            });

            return Ok(salida);
        }
    }
}