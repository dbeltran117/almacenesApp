using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manejadores;

namespace RestauranteApp
{
    public partial class Form1 : Form
    {
        ManejadorUsuarios mu;
        public Form1()
        {
            InitializeComponent();
            mu = new ManejadorUsuarios();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string usuario = TxtUser.Text;
            string clave = TxtClave.Text;

            var rs = mu.validarLogin(usuario, clave);

            if (rs.esValido)
            {
                MessageBox.Show(rs.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MenuPrincipal mainForm = new MenuPrincipal(rs.usuarioEncontrado);
                mainForm.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show(rs.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
