using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Teste_T4L.Properties
{
    /// <summary>
    /// Interaction logic for EditarProduto.xaml
    /// </summary>
    public partial class EditarProduto : Window
    {
        public EditarProduto()
        {
            InitializeComponent();

            try
            {
                Conexao con = new Conexao();
                string selectQuery = "SELECT * FROM produto_grupo";
                MySqlCommand cmd = new MySqlCommand(selectQuery, con.conectar());

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbxGrupoProduto.Items.Add(reader.GetString("nome"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProdutos consultProd = new ConsultaProdutos();
            consultProd.Show();
            this.Close();
        }
    }
}
