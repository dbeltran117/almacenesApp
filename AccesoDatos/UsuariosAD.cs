using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class UsuariosAD
    {
        private Base _base;
        private const string ConnectionStringName = "MySQLConexion";

        public UsuariosAD()
        {
            _base = new Base(ConnectionStringName);
        }

        public DataSet ValidarCredenciales(string nombre, string clave)
        {
            string query = $"SELECT\r\n    u.Id AS ID_Usuario,\r\n    u.NombreUsuario,\r\n    u.Email,\r\n    m.nombre AS Nombre_Modulo,\r\n    up.permiso_escritura AS Permiso_Escritura,\r\n    up.permiso_leer_abrir AS Permiso_Leer_Abrir\r\nFROM\r\n    usuarios u\r\nJOIN\r\n    usuarios_permisos up ON u.Id = up.usuario_id\r\nJOIN\r\n    modulos m ON up.modulo_id = m.id\r\nWHERE\r\n    u.NombreUsuario = '{nombre}' AND u.Clave = '{clave}'\r\nORDER BY\r\n    u.NombreUsuario, m.nombre";
            return _base.consulta(query, "usuarios");
        }

        public string AgregarUsuario(string nombreUsuario, string clave, string email)
        {
            string query = $"INSERT INTO usuarios (NombreUsuario, Clave, Email) VALUES ('{nombreUsuario}', '{clave}', '{email}')";
            return _base.comando(query);
        }

        public string EliminarUsuario(int idUsuario)
        {
            string query = $"DELETE FROM usuarios WHERE Id = {idUsuario}";
            return _base.comando(query);
        }

        public DataSet VerUsuarios(string filtroNombreUsuario)
        {
            string query = $"SELECT Id,NombreUsuario, Email FROM usuarios WHERE NombreUsuario LIKE '%{filtroNombreUsuario}%'";
            return _base.consulta(query, "usuarios");
        }
    }
}
