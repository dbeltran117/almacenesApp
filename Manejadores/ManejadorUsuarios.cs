using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace Manejadores
{
    public class ManejadorUsuarios
    {
        public UsuariosAD _userAD;

        public ManejadorUsuarios()
        {
            _userAD = new UsuariosAD();
        }

        public (bool esValido, string mensaje, Usuario usuarioEncontrado) validarLogin(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                return (false, "El nombre de usuario y la clave no pueden estar vacíos.", null);
            }
            DataSet ds = _userAD.ValidarCredenciales(usuario, clave);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count >= 1)
            {
                DataTable dt = ds.Tables[0];
                Usuario usuer = new Usuario();

                foreach (DataRow row in dt.Rows)
                {
                    if (usuer.Id == 0) // Solo asignar los datos del usuario una vez
                    {
                        usuer.Id = Convert.ToInt32(row["ID_Usuario"]);
                        usuer.NombreUsuario = row["NombreUsuario"].ToString();
                        usuer.Email = row["Email"].ToString();
                    }
                    Permisos permiso = new Permisos
                    {
                        NombreModulo = row["Nombre_Modulo"].ToString(),
                        PermisoEscritura = Convert.ToBoolean(row["Permiso_Escritura"]),
                        PermisoLeerAbrir = Convert.ToBoolean(row["Permiso_Leer_Abrir"])
                    };
                    usuer.permisos.Add(permiso);
                    
                }
                return (true, "Login exitoso.", usuer);
            }
            else
            {
                return (false, "Credenciales inválidas.", null); 
            }
        }
    }
}
