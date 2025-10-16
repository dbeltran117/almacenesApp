using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace RestauranteApp
{
    public partial class MenuPrincipal : Form
    {
        private Usuario _usuario;
        public MenuPrincipal(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            LblUsuario.Text = _usuario.NombreUsuario;
            productoToolStripMenuItem.Checked = false;
            usuarioToolStripMenuItem.Enabled = false;
            salirToolStripMenuItem.Enabled = true;
            foreach (var permiso in _usuario.permisos)
            {
                if (permiso.NombreModulo.Equals("productos",StringComparison.OrdinalIgnoreCase))
                {
                    productoToolStripMenuItem.Enabled = permiso.PermisoLeerAbrir;
                }
                else if (permiso.NombreModulo.Equals("usuarios",StringComparison.OrdinalIgnoreCase))
                {
                    usuarioToolStripMenuItem.Enabled = permiso.PermisoEscritura;
                    usuarioToolStripMenuItem.Enabled = permiso.PermisoLeerAbrir;
                }
            }
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Entro a Productos");
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios fu = new FrmUsuarios();
            fu.MdiParent = this;
            fu.Show();
        }
    }
}
