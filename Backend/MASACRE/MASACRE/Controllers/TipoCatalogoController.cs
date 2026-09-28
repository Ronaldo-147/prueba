using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoCatalogoController : ControllerBase
    {
        private readonly CN_TIPO_CATALOGO _logica;

        public TipoCatalogoController(IConfiguration configuration)
        {
            _logica = new CN_TIPO_CATALOGO(configuration);
        }

        [HttpGet("Listar")]
        public ActionResult<List<CE_TIPO_CATALOGO>> Listar()
        {
            var resultado = _logica.LISTAR();

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(x => new CE_TIPO_CATALOGO
            {
                Id_Tipo_Catalogo = x.Id_Tipo_Catalogo,
                Tipo_Catalogo = x.Tipo_Catalogo,
                Fecha_Creacion = x.Fecha_Creacion,
                Fecha_Modificacion = x.Fecha_Modificacion,
                Activo = x.Activo
            }).ToList();

            return Ok(salida);
        }

[HttpGet("Filtrar")]
public ActionResult<List<DTO_TipoCatalogoFiltrado>> Filtrar(
    [FromQuery] string texto = "",
    [FromQuery] int porID = 1,
    [FromQuery] int porEstado = 1)
{
    var resultado = _logica.FILTRAR(texto, porID, porEstado);

    if (resultado == null || resultado.Count == 0)
        return Ok(new { mensaje = "SIN RESULTADOS" });

    var salida = resultado.Select(x => new DTO_TipoCatalogoFiltrado
    {
        Id_Tipo_Catalogo = x.Id_Tipo_Catalogo,
        Tipo_Catalogo = x.Tipo_Catalogo,
        Fecha_Creacion = x.Fecha_Creacion,
        Fecha_Modificacion = x.Fecha_Modificacion,
        Activo = x.Activo
    }).ToList();

    return Ok(salida);
}

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CE_TIPO_CATALOGO obj)
        {
            try
            {
                _logica.INSERTAR(obj);
                return Ok(new { mensaje = "✅ Tipo de catálogo insertado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al insertar: {ex.Message}");
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_TIPO_CATALOGO obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Tipo de catálogo actualizado correctamente." });
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
                return Ok(new { mensaje = $"🗑 Tipo de catálogo {id} desactivado correctamente." });
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
                return Ok(new { mensaje = $"🔄 Tipo de catálogo {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}