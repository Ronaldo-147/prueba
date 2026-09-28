using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatosPersonalesController : ControllerBase
    {
        private readonly CN_DATOS_PERSONALES _logica;

        public DatosPersonalesController(IConfiguration configuration)
        {
            _logica = new CN_DATOS_PERSONALES(configuration);
        }
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var resultado = _logica.LISTAR();

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(p => new
            {
                p.Id_Persona,
                p.Primer_Nombre,
                p.Segundo_Nombre,
                p.Primer_Apellido,
                p.Segundo_Apellido,
                p.Id_Tipo_DNI,
                p.Tipo_DNI,
                p.DNI,
                p.Id_Genero,
                p.Genero,
                p.Id_Nacionalidad,
                p.Nacionalidad,
                p.Id_Estado,
                p.Estado_Registro,
                Fecha_Creacion = p.Fecha_Creacion?.ToString("yyyy-MM-dd HH:mm:ss"),
                Fecha_Modificacion = p.Fecha_Modificacion?.ToString("yyyy-MM-dd HH:mm:ss")
            });

            return Ok(salida);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string texto)
        {
            var resultado = _logica.FILTRAR(texto);

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            return Ok(resultado);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_InsertarDatosPersonales datos)
        {
            var entidad = new CE_DATOS_PERSONALES
            {
                Primer_Nombre = datos.Primer_Nombre,
                Segundo_Nombre = datos.Segundo_Nombre,
                Primer_Apellido = datos.Primer_Apellido,
                Segundo_Apellido = datos.Segundo_Apellido,
                Id_Tipo_DNI = datos.Id_Tipo_DNI,
                DNI = datos.DNI,
                Id_Genero = datos.Id_Genero,
                Id_Nacionalidad = datos.Id_Nacionalidad,
                Id_Estado = datos.Id_Estado
            };

            var resultado = _logica.INSERTAR(entidad);
            return Ok(resultado);
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_DATOS_PERSONALES persona)
        {
            try
            {
                _logica.ACTUALIZAR(persona);
                return Ok(new { mensaje = "✅ Registro actualizado correctamente." });
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