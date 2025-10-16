using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AccesoDatos
{
    public class Base
    {
        private string _conecctionString;

        public Base(string nombreCadenaConexion)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[nombreCadenaConexion];
            if (settings == null)
            {
                throw new Exception("La cadena de conexión no fue encontrada.");
            }
            _conecctionString = settings.ConnectionString;
        }

        public string comando(string query)
        {
            using (MySqlConnection con = new MySqlConnection(_conecctionString))
            {
                try
                {
                    using (MySqlCommand i = new MySqlCommand(query,con))
                    {
                        con.Open();
                        int filasAfectadas = i.ExecuteNonQuery();
                        con.Close();
                        return $"Correcto, Filas Afectadas: {filasAfectadas}";
                    }
                }
                catch (Exception ex) 
                {
                    return $"Error: {ex.Message}";
                }
            }
        }

        public DataSet consulta(string query, string tabla)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection con = new MySqlConnection(_conecctionString))
            {
                try
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, con))
                    {
                        con.Open();
                        da.Fill(ds, tabla);
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            return ds;
        }

    }
}