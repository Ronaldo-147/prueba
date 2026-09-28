using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public static class JWTGenerator
    {
        public static string GenerarToken(CE_USUARIO usuario, IConfiguration config)
        {
            // 🔐 Validación robusta de configuración
            var clave = config["Jwt:Key"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(clave))
                throw new InvalidOperationException("⚠️ Clave JWT no configurada en appsettings.json");
            if (string.IsNullOrWhiteSpace(issuer))
                Console.WriteLine("⚠️ Aviso: Issuer no configurado, el token se generará sin validación de emisor.");
            if (string.IsNullOrWhiteSpace(audience))
                Console.WriteLine("⚠️ Aviso: Audience no configurado, el token se generará sin validación de audiencia.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
           {
                new Claim(ClaimTypes.Name, usuario.Usuario ?? "sin_usuario"),
                new Claim("Id", usuario.Id_Usuario?.ToString() ?? "0"),
                new Claim("Rol", usuario.Rol ?? "sin_rol"),
                new Claim("Estado", usuario.Estado_Registro ?? "sin_estado"),
                new Claim("Nombre", usuario.Nombre_Completo ?? "sin_nombre")
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}