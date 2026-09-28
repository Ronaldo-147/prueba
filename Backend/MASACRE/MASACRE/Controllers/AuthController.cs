using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CAPA_NEGOCIO;
using CAPA_ENTIDADES;

namespace MASACRE.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly CN_Login _logica;

        public AuthController(IConfiguration config)
        {
            _logica = new CN_Login(config ?? throw new ArgumentNullException(nameof(config)));
        }

        /// <summary>
        /// Autentica al usuario y retorna token JWT más datos resumidos.
        /// </summary>
        /// <param name="dto">Credenciales del usuario</param>
        /// <returns>DTO con token, usuario, rol, nombre, expiración y mensaje</returns>
        /// <remarks>
        /// Ejemplo de entrada:
        /// {
        ///   "usuario": "admin@gmail.com",
        ///   "contrasena": "123456"
        /// }
        /// </remarks>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DTO_LoginRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] DTO_Login dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Contrasena))
                return BadRequest(new { error = "Usuario y contraseña son requeridos." });

            try
            {
                var usuario = _logica.AutenticarYConsultar(dto.Usuario.Trim(), dto.Contrasena.Trim());

                if (usuario == null)
                    return Unauthorized(new
                    {
                        autenticado = false,
                        mensaje = "Credenciales inválidas o usuario inactivo."
                    });

                var token = _logica.GenerarToken(usuario);

                return Ok(new DTO_LoginRespuesta
                {
                    Token = token,
                    Usuario = usuario.Usuario ?? "Desconocido",
                    Nombre = usuario.Nombre_Completo ?? "Sin nombre",
                    Rol = usuario.Rol ?? "Sin rol",
                    Estado = usuario.Estado_Registro ?? "Indefinido",
                    Expira = DateTime.UtcNow.AddHours(2),
                    Mensaje = "Autenticación exitosa."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Error en login → {ex.Message}");
                return StatusCode(500, new
                {
                    error = "Error interno en el servidor.",
                    detalle = ex.Message
                });
            }
        }

        /// <summary>
        /// Retorna datos del usuario autenticado extraídos del token JWT.
        /// </summary>
        /// <returns>Usuario, rol e ID</returns>
        [Authorize]
        [HttpGet("perfil")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public IActionResult Perfil()
        {
            var identidad = HttpContext.User.Identity as ClaimsIdentity;

            if (identidad == null || !identidad.Claims.Any())
                return Unauthorized(new { error = "No se pudo leer el token JWT." });

            var claims = identidad.Claims;

            return Ok(new
            {
                Usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Rol = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Id = claims.FirstOrDefault(c => c.Type == "Id")?.Value,
                Estado = claims.FirstOrDefault(c => c.Type == "Estado")?.Value,
                Nombre = claims.FirstOrDefault(c => c.Type == "Nombre")?.Value
            });
        }
    }
}