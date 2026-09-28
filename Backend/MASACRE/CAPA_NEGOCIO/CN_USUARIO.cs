using System;
using System.Collections.Generic;
using CAPA_DATOS;
using CAPA_ENTIDADES;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO
{
    public class CN_USUARIO
    {
        private readonly CD_USUARIO _cd;

        public CN_USUARIO(IConfiguration configuration)
        {
            _cd = new CD_USUARIO(configuration);
        }

        public string INSERTAR(CE_USUARIO obj)
        {
            if (obj == null)
                return "❌ El objeto usuario está vacío.";

            if (obj.Id_Persona is null or <= 0)
                return "Debe seleccionar una persona válida.";

            if (string.IsNullOrWhiteSpace(obj.Usuario))
                return "El nombre de usuario no puede estar vacío.";

            if (string.IsNullOrWhiteSpace(obj.Contrasena))
                return "La contraseña es obligatoria.";

            if (obj.Id_Rol is null or <= 0)
                return "Debe asignar un rol válido.";

            return _cd.Insertar(obj);
        }

        public List<CE_USUARIO> LISTAR()
        {
            return _cd.Listar();
        }

        public List<CE_USUARIO> FILTRAR(string texto)
        {
            return _cd.Filtrar(texto?.Trim() ?? "");
        }

        public string ELIMINAR(int idUsuario)
        {
            if (idUsuario <= 0)
                return "ID inválido para eliminar.";
            return _cd.Eliminar(idUsuario);
        }

        public string RESTAURAR(int idUsuario)
        {
            if (idUsuario <= 0)
                return "ID inválido para restaurar.";
            return _cd.Restaurar(idUsuario);
        }

        public string ACTUALIZAR(CE_USUARIO obj)
        {
            if (obj == null)
                return "❌ El objeto usuario está vacío.";

            if (obj.Id_Usuario is null or <= 0)
                return "Debe proporcionar un ID de usuario válido.";

            if (obj.Id_Persona is not null && obj.Id_Persona <= 0)
                return "La persona seleccionada no es válida.";

            if (!string.IsNullOrWhiteSpace(obj.Usuario) && obj.Usuario.Length < 3)
                return "El nombre de usuario debe tener al menos 3 caracteres.";

            if (obj.Id_Rol is not null && obj.Id_Rol <= 0)
                return "El rol asignado no es válido.";

            if (obj.Id_Estado is not null && obj.Id_Estado <= 0)
                return "El estado asignado no es válido.";

            return _cd.Actualizar(obj);
        }
    }
}