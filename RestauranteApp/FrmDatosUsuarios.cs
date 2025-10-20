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
    public partial class FrmDatosUsuarios : Form
    {
        ManejadorUsuarios mu;
        public FrmDatosUsuarios()
        {
            InitializeComponent();
            mu = new ManejadorUsuarios();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            mu.AgregarUsuario(TxtUsuario.Text, TxtClave.Text, TxtEmail.Text);
            Close();
        }
    }
}
