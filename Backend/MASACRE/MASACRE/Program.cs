using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔐 JWT configurado desde appsettings.json
var config = builder.Configuration;
var claveJwt = config["Jwt:Key"];
var issuer = config["Jwt:Issuer"];
var audience = config["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveJwt))
        };
    });

// 🌐 CORS para desarrollo con Vite usando HTTP
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteDev", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()); // opcional si usás sesión o headers extra
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 📘 Swagger con soporte para autorización por token
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MASACRE API",
        Version = "v1",
        Description = "API protegida con JWT. Swagger listo para pruebas."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 🧪 Middleware para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔄 Pipeline ordenado y compatible con HTTP
// ⚠️ No usamos UseHttpsRedirection para evitar forzar HTTPS
app.UseCors("AllowViteDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();