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
using Entidades;

namespace RestauranteApp
{
    public partial class FrmUsuarios : Form
    {
        ManejadorUsuarios mu;
        int fila = 0, columna = 0;
        Usuario u = new Usuario();
        public FrmUsuarios()
        {
            InitializeComponent();
            mu = new ManejadorUsuarios();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            mu.VerUsuarios(TxtBuscar,DtgDatos); 
        }

        private void DtgDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            u.Id = Convert.ToInt32(DtgDatos.Rows[fila].Cells["Id"].Value);
            u.NombreUsuario = DtgDatos.Rows[fila].Cells["NombreUsuario"].Value.ToString();
            switch (columna)
            {
                case 3:
                    {
                        if (u.NombreUsuario.ToLower() == "admin")
                        {
                            MessageBox.Show("No se puede eliminar el usuario Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (resultado == DialogResult.Yes)
                            {
                                mu.EliminarUsuario(u);
                                MessageBox.Show("Usuario eliminado correctamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                    break;
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmDatosUsuarios fdu = new FrmDatosUsuarios();
            fdu.Owner = this;
            fdu.Show();
        }

        private void DtgDatos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            columna = e.ColumnIndex;
            fila = e.RowIndex;
        }
    }
}
