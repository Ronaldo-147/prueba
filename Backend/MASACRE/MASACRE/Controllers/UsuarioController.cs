using Microsoft.AspNetCore.Mvc;
using CAPA_NEGOCIO;
using CAPA_ENTIDADES;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly CN_USUARIO _logica;

        public UsuarioController(IConfiguration config)
        {
            _logica = new CN_USUARIO(config);
        }

        /// 🟢 Insertar nuevo usuario
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] DTO_UsuarioInsertar dto)
        {
            var entidad = new CE_USUARIO
            {
                Id_Persona = dto.Id_Persona,
                Usuario = dto.Usuario,
                Contrasena = dto.Contrasena,
                Id_Rol = dto.Id_Rol,
                Id_Estado = dto.Id_Estado
            };

            var mensaje = _logica.INSERTAR(entidad);
            return Ok(new { mensaje });
        }

        /// 📋 Listar usuarios activos
        [HttpGet("Listar")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(204)]
        public IActionResult Listar()
        {
            var lista = _logica.LISTAR();

            if (lista is null || lista.Count == 0)
                return NoContent();

            var salida = lista.Select(u => new
            {
                u.Id_Usuario,
                u.Usuario,
                u.Nombre_Completo,
                u.Rol,
                u.Estado_Registro
            });

            return Ok(salida);
        }

        /// 🔍 Filtrar usuarios por texto (nombre, usuario, etc.)
        [HttpGet("Filtrar")]
        [ProducesResponseType(typeof(IEnumerable<DTO_UsuarioRespuesta>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Filtrar([FromQuery] string busqueda)
        {
            if (string.IsNullOrWhiteSpace(busqueda))
                return BadRequest(new { error = "Debe proporcionar un texto de búsqueda." });

            var lista = _logica.FILTRAR(busqueda.Trim());

            var salida = (lista ?? new List<CE_USUARIO>())
                .Select(u => new DTO_UsuarioRespuesta
                {
                    Id_Usuario = u.Id_Usuario ?? 0,
                    Usuario = u.Usuario ?? "Sin usuario",
                    Nombre_Completo = u.Nombre_Completo ?? "Sin nombre",
                    Rol = u.Rol ?? "Sin rol",
                    Estado_Registro = u.Estado_Registro ?? "No definido",
                    Fecha_Creacion = u.Fecha_Creacion ?? DateTime.Now,
                    Fecha_Modificacion = u.Fecha_Modificacion ?? DateTime.Now
                });

            return Ok(salida); // ✅ Siempre devuelve 200 con array (vacío o con datos)
        }

        /// ❌ Eliminar usuario (marcar como inactivo)
        [HttpDelete("Eliminar/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Eliminar(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "ID inválido." });

            var resultado = _logica.ELIMINAR(id);

            if (resultado.StartsWith("ID"))
                return BadRequest(new { error = resultado });

            return Ok(new { mensaje = resultado });
        }

        /// 🔄 Restaurar usuario (marcar como activo)
        [HttpPut("Restaurar/{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Restaurar(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "ID inválido." });

            var resultado = _logica.RESTAURAR(id);

            if (resultado.StartsWith("ID"))
                return BadRequest(new { error = resultado });

            return Ok(new { mensaje = resultado });
        }

        /// 🔧 Actualizar usuario existente
        [HttpPut("Actualizar")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult Actualizar([FromBody] CE_USUARIO dto)
        {
            if (dto == null || dto.Id_Usuario <= 0)
                return BadRequest(new { error = "Datos inválidos para actualizar." });

            var entidad = new CE_USUARIO
            {
                Id_Usuario = dto.Id_Usuario,
                Id_Persona = dto.Id_Persona,
                Usuario = dto.Usuario,
                Contrasena = dto.Contrasena,
                Id_Rol = dto.Id_Rol,
                Id_Estado = dto.Id_Estado
            };

            var mensaje = _logica.ACTUALIZAR(entidad);

            if (mensaje.StartsWith("❌") || mensaje.StartsWith("Debe"))
                return BadRequest(new { error = mensaje });

            return Ok(new { mensaje });
        }
    }
}