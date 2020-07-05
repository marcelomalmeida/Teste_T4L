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
                Conexao con = new Conexao();
                string selectQuery = "SELECT * FROM produto_grupo";
                MySqlCommand cmd = new MySqlCommand(selectQuery, con.conectar());

                MySqlDataReader reader = cmd.ExecuteReader();
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
                //Convertendo Nome do grupo do Produto para o código
                Conexao con = new Conexao();
                string selectQuery = "SELECT cod FROM produto_grupo WHERE produto_grupo.nome = ?";
                MySqlCommand cmd = new MySqlCommand(selectQuery, con.conectar());
                cmd.Parameters.Add("@produto_grupo.nome", MySqlDbType.String).Value = cbxGrupoProduto.Text;
                cmd.CommandType = CommandType.Text;
                
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string codGrupo = reader.GetString("cod");

                int ativo;

                if (checkBoxAtivo.IsChecked == true) //Validação para ver se o produto foi ativado
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

        //Botao para limpar os campos
        private void btnLimpar_Click_1(object sender, RoutedEventArgs e)
        {
            Limpar();
        }

        //Botao para voltar ao Menu
        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MenuInicial mI = new MenuInicial();
            mI.Show();
            this.Close();
        }
    }
}