using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //MySqlConnection conect = new MySqlConnection("Server=localhost;Database=testdev;Uid=root;Pwd=123456;");
                Conexao conect = new Conexao();
                string selectQuery = "SELECT * FROM produto_grupo";
                MySqlCommand command = new MySqlCommand(selectQuery, conect.conectar());
                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    cbxGrupoProduto.Items.Add(reader.GetString("nome"));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Método limpar campo
        public void Limpar()
        {
            txtDesc.Clear();
            txtCodBarra.Clear();
            txtPrecoCusto.Clear();
            txtPrecoVenda.Clear();
        }

        private void btnCadastrar_Click_1(object sender, RoutedEventArgs e)
        {
            CadastroProduto cadProd = new CadastroProduto(txtDesc.Text, txtCodBarra.Text, cbxGrupoProduto.Text, txtPrecoCusto.Text, txtPrecoVenda.Text, DateTime.Now);
            MessageBox.Show(cadProd.msg);
            Limpar();
        }

        private void btnLimpar_Click_1(object sender, RoutedEventArgs e)
        {
            Limpar();
        }

    }
}