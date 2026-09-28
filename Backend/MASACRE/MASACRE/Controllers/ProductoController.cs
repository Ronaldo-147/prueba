using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly CN_PRODUCTO _logica;
        CE_PRODUCTO _CEProducto = new CE_PRODUCTO();

        public ProductoController(IConfiguration configuration)
        {
            _logica = new CN_PRODUCTO(configuration);
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var productos = _logica.LISTAR();

            if (productos == null || productos.Count == 0)
                return NoContent();

            return Ok(productos);
        }

        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] string texto)
        {
            var productos = _logica.FILTRAR_PROYECTADO(texto);

            if (productos == null || productos.Count == 0)
                return NoContent();

            return Ok(productos);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_ProductoInsertar dto)
        {
            try
            {
                var producto = new CE_PRODUCTO
                {
                    Codigo = dto.Codigo,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Presentacion = dto.Presentacion,
                    Costo_Unitario = dto.Costo_Unitario,
                    Precio_Venta = dto.Precio_Venta,
                    Id_Proveedor = dto.Id_Proveedor,
                    Id_Estado = dto.Id_Estado ?? 0
                };

                _logica.INSERTAR(producto);

                return Ok(new { mensaje = "✅ Producto insertado correctamente." });
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
        public IActionResult Actualizar([FromBody] CE_PRODUCTO obj)
        {
            try
            {
                _logica.ACTUALIZAR(obj);
                return Ok(new { mensaje = "✅ Producto actualizado correctamente." });
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
                return Ok(new { mensaje = $"🗑 Producto {id} inactivado correctamente." });
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
                return Ok(new { mensaje = $"🔄 Producto {id} restaurado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Error al restaurar: {ex.Message}");
            }
        }
    }
}