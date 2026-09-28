using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionController : ControllerBase
    {
        private readonly CN_DIRECCION _logica;

        public DireccionController(IConfiguration configuration)
        {
            _logica = new CN_DIRECCION(configuration);
        }
        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var lista = _logica.LISTAR();

            if (lista == null || lista.Count == 0)
                return NoContent();

            var salida = lista.Select(d => new {
                d.Id_Direccion,
                d.Direccion_Completa,
                d.Id_Tipo_Direccion,
                d.Tipo_Direccion,
                d.Fecha_Creacion,
                d.Fecha_Modificacion,
                Activo = d.Activo == true ? 1 : 0
            });

            return Ok(salida);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string texto = "")
        {
            var lista = _logica.FILTRAR(texto);

            if (lista == null || lista.Count == 0)
                return NoContent();

            var salida = lista.Select(d => new {
                d.Id_Direccion,
                d.Direccion_Completa,
                d.Id_Tipo_Direccion,
                d.Tipo_Direccion,
                d.Fecha_Creacion,
                d.Fecha_Modificacion,
                Activo = d.Activo == true ? 1 : 0
            });

            return Ok(salida);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_DireccionInsertar dto)
        {
            _logica.InsertarDesdeDTO(dto);

            return Ok(new { Mensaje = "Dirección registrada con éxito." });
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CE_DIRECCION obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Dirección actualizada correctamente." });
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
                return Ok(new { mensaje = $"🗑 Dirección {id} inactivada correctamente." });
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
                return Ok(new { mensaje = $"🔄 Dirección {id} restaurada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}
