using CAPA_ENTIDADES;
using CAPA_DATOS;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace CAPA_NEGOCIO
{
    public class CN_Login
    {
        private readonly CD_Login _cdLogin;
        private readonly IConfiguration _config;

        public CN_Login(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _cdLogin = new CD_Login(_config);
        }

        /// <summary>
        /// Autentica al usuario y devuelve sus datos completos si es válido.
        /// </summary>
        /// <param name="usuario">Nombre de usuario</param>
        /// <param name="contrasena">Contraseña en texto plano</param>
        /// <returns>Entidad CE_USUARIO o null si las credenciales son inválidas</returns>
        public CE_USUARIO AutenticarYConsultar(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("Credenciales incompletas.");

            var usuarioAutenticado = _cdLogin.ConsultarPorCredenciales(usuario.Trim(), contrasena.Trim());

            if (usuarioAutenticado != null)
            {
                Console.WriteLine($"🔐 Usuario autenticado: {usuarioAutenticado.Usuario} | Rol: {usuarioAutenticado.Rol}");
            }
            else
            {
                Console.WriteLine("❌ Falló la autenticación. Usuario o contraseña incorrectos.");
            }

            return usuarioAutenticado;
        }

        /// <summary>
        /// Genera el token JWT firmado para el usuario autenticado.
        /// </summary>
        /// <param name="usuario">Entidad CE_USUARIO ya validada</param>
        /// <returns>Token JWT en formato string</returns>
        public string GenerarToken(CE_USUARIO usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "No se puede generar token para usuario nulo.");

            return JWTGenerator.GenerarToken(usuario, _config);
        }
    }
}