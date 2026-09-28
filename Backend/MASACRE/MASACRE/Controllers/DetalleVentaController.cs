using Microsoft.AspNetCore.Mvc;
using CAPA_ENTIDADES;
using CAPA_NEGOCIO;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleVentaController : ControllerBase
    {
        private readonly CN_DETALLE_VENTA _logica;

        public DetalleVentaController(IConfiguration configuration)
        {
            _logica = new CN_DETALLE_VENTA(configuration);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_InsertarDetalleVenta dto)
        {
            var detalle = new CE_DETALLE_VENTA
            {
                Id_Venta = dto.Id_Venta,
                Id_Producto = dto.Id_Producto,
                Cantidad = dto.Cantidad,
                Precio_Unitario_Al_Momento_Venta = dto.Precio_Unitario_Al_Momento_Venta,
                Subtotal = dto.Subtotal
            };

            var mensaje = _logica.INSERTAR(detalle);
            return Ok(new { mensaje });
        }


        [HttpGet("Filtrar")]
        public IActionResult Filtrar([FromQuery] int? idVenta, [FromQuery] string texto = "")
        {
            var resultado = _logica.FILTRAR(idVenta, texto);

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(x => new
            {
                x.Id_Detalle,
                x.Id_Venta,
                x.Id_Producto,
                x.Nombre_Producto,
                x.Codigo_Producto,
                x.Cantidad,
                x.Precio_Unitario_Al_Momento_Venta,
                x.Subtotal
                // ❌ No incluís TextoBusqueda aquí, por lo tanto no aparece en la salida
            });

            return Ok(salida);
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var resultado = _logica.LISTAR();

            if (resultado == null || resultado.Count == 0)
                return NoContent();

            var salida = resultado.Select(d => new
            {
                d.Id_Detalle,
                d.Id_Venta,
                d.Id_Producto,
                d.Nombre_Producto,
                d.Codigo_Producto,
                d.Cantidad,
                d.Precio_Unitario_Al_Momento_Venta,
                d.Subtotal
            });

            return Ok(salida);
        }
    }
}
