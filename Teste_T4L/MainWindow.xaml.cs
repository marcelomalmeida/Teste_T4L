using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
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

            // Carregar itens no combobox
            try
            {
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

        //Método para limpar os campos
        public void Limpar()
        {
            txtDesc.Clear();
            txtCodBarra.Clear();
            txtPrecoCusto.Clear();
            txtPrecoVenda.Clear();
            
        }

        //Botao Cadastar
        private void btnCadastrar_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                Conexao conect = new Conexao();
                string selectQuery = "SELECT cod FROM produto_grupo WHERE PRODUTO_GRUPO.nome = ?";
                MySqlCommand command = new MySqlCommand(selectQuery, conect.conectar());
                command.Parameters.Add("@PRODUTO_GRUPO.nome", MySqlDbType.String).Value = cbxGrupoProduto.Text;

                command.CommandType = CommandType.Text;

                MySqlDataReader dr;
                dr = command.ExecuteReader();
                dr.Read();

                string codGrupo = dr.GetString("cod");

                int ativo;

                if (checkBoxAtivo.IsChecked == true)
                {

                    ativo = 1;
                    CadastroProduto cadProd = new CadastroProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, DateTime.Now, ativo);
                    MessageBox.Show(cadProd.msg);
                    Limpar();
                }
                else
                {
                    ativo = 0;
                    CadastroProduto cadProd = new CadastroProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, DateTime.Now, ativo);
                    MessageBox.Show(cadProd.msg);
                    Limpar();
                }
            }catch(Exception ex)
            {

            }
            
        }

        //Botao Limpar
        private void btnLimpar_Click_1(object sender, RoutedEventArgs e)
        {
            Limpar();
        }

    }
}