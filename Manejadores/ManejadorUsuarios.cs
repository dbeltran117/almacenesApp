using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatos;
using Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public string AgregarUsuario(string nombreUsuario, string clave, string email)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(email))
            {
                return "El nombre de usuario, la clave y el email no pueden estar vacíos.";
            }
            return _userAD.AgregarUsuario(nombreUsuario, clave, email);
        }

        public void VerUsuarios(System.Windows.Forms.TextBox usuario , DataGridView tablas)
        {
            tablas.Columns.Clear();
            tablas.DataSource = _userAD.VerUsuarios(usuario.Text).Tables[0];
            tablas.Columns["Id"].Visible = false;
            tablas.Columns.Insert(3, Boton("Eliminar", Color.Red));
            tablas.AutoResizeColumns();
            tablas.AutoResizeRows();
        }

        public string EliminarUsuario(Usuario u)
        {
            return _userAD.EliminarUsuario(u.Id);
        }

        public static DataGridViewButtonColumn Boton(string titulo, Color fondo)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Text = titulo;
            btn.DefaultCellStyle.BackColor = fondo;
            btn.DefaultCellStyle.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseColumnTextForButtonValue = true;
            return btn;
        }
    }
}
