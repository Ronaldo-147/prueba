using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CAPA_ENTIDADES;

namespace CAPA_NEGOCIO
{
    public class CN_Autenticacion
    {
        // 🔐 Clave secreta configurable (evitá hardcodearla en producción)
        private readonly string claveSecreta;

        public CN_Autenticacion(string claveConfigurada = "TuSuperClaveSecreta🔐")
        {
            claveSecreta = claveConfigurada;
        }

        /// <summary>
        /// Genera un JWT válido a partir de los datos del usuario.
        /// </summary>
        public string GenerarToken(CE_USUARIO usuario)
        {
            if (usuario == null || usuario.Id_Usuario == null || string.IsNullOrWhiteSpace(usuario.Usuario))
                throw new ArgumentException("Datos del usuario insuficientes para generar el token.");

            var claims = new[]
            {
                new Claim("id", usuario.Id_Usuario.ToString() ?? ""),
                new Claim("usuario", usuario.Usuario),
                new Claim("rol", usuario.Rol ?? "sin-rol")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TuApp",
                audience: "TuAppFront",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}