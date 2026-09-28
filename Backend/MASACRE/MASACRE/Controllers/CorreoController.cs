using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorreoController : ControllerBase
    {
        private readonly CN_CORREO _logica;

        public CorreoController(IConfiguration configuration)
        {
            _logica = new CN_CORREO(configuration);
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var correos = _logica.LISTAR();

            if (correos == null || correos.Count == 0)
                return NoContent();

            var resultado = correos.Select(c => new
            {
                c.Id_Correo,
                c.Direccion_Correo,
                c.Tipo_Correo, // ✅ Se incluye siempre si tiene valor

                // Convertimos fechas para un formato legible
                Fecha_Creacion = c.Fecha_Creacion?.ToString("yyyy-MM-dd HH:mm:ss"),
                Fecha_Modificacion = c.Fecha_Modificacion?.ToString("yyyy-MM-dd HH:mm:ss"),

                // Solo se muestra si Activo es false
                c.Activo

            });

            return Ok(resultado);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string texto)
        {
            var correos = _logica.FILTRAR(texto);

            if (correos == null || correos.Count == 0)
                return NoContent();

            var resultado = correos.Select(c => new
            {
                c.Id_Correo,
                c.Direccion_Correo,
                c.Tipo_Correo,
                Fecha_Creacion = c.Fecha_Creacion?.ToString("yyyy-MM-dd HH:mm:ss"),
                Fecha_Modificacion = c.Fecha_Modificacion?.ToString("yyyy-MM-dd HH:mm:ss"),
                Activo = c.Activo

            });

            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CE_CORREO obj)
        {
            try
            {
                _logica.INSERTAR(obj);
                return Ok(new { mensaje = "✅ Correo insertado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al insertar: {ex.Message}");
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_CORREO obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Correo actualizado correctamente." });
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
                return Ok(new { mensaje = $"🗑 Correo {id} inactivado correctamente." });
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
                return Ok(new { mensaje = $"🔄 Correo {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}
