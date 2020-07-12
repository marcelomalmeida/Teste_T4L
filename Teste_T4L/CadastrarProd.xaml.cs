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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CadastrarProd : Window
    {
        public CadastrarProd()
        {
            InitializeComponent();
            txtDesc.Focus();

            // Carregar itens no combobox de Grupo de Produto
            try
            {
                Conexao conexao = new Conexao();
                string selectQuery = "SELECT * FROM produto_grupo";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conexao.conectar());

                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    cbxGrupoProduto.Items.Add(reader.GetString("nome"));
                }

                conexao.desconectar();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro no Processo!!!");
            }
        }

        //Botao Cadastar
        private void btnCadastrar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //Convertendo Nome do grupo do Produto para o código
                Conexao conexao = new Conexao();
                string selectQuery = "SELECT cod FROM produto_grupo WHERE produto_grupo.nome = ?";
                MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                comando.Parameters.Add("@produto_grupo.nome", MySqlDbType.String).Value = cbxGrupoProduto.Text;
                comando.CommandType = CommandType.Text;
                
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();

                string codGrupo = reader.GetString("cod");

                int ativo;

                if (checkBoxAtivo.IsChecked == true) //Validação para ver se o produto foi ativado
                {

                    ativo = 1;
                    CadastroProduto cadProd = new CadastroProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, DateTime.Now, ativo);
                    txtDesc.Clear();
                    txtCodBarra.Clear();
                    txtPrecoCusto.Clear();
                    txtPrecoVenda.Clear();
                }
                else
                {
                    ativo = 0;
                    CadastroProduto cadProd = new CadastroProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, DateTime.Now, ativo);
                    txtDesc.Clear();
                    txtCodBarra.Clear();
                    txtPrecoCusto.Clear();
                    txtPrecoVenda.Clear();
                }

                conexao.desconectar();
           
            }
            catch(Exception)
            {
                MessageBox.Show("Falta informação!!");
                txtDesc.Focus();
            }
        }

        //Botao para limpar os campos
        private void btnLimpar_Click_1(object sender, RoutedEventArgs e)
        {
            txtDesc.Clear();
            txtCodBarra.Clear();
            txtPrecoCusto.Clear();
            txtPrecoVenda.Clear();
            txtDesc.Focus();
        }

        //Botao para voltar ao Menu
        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MenuInicial mI = new MenuInicial();
            mI.Show();
            this.Close();
        }

        //Metodo para aceitar apenas numeros no campo Preco
        private void txtPrecoCusto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9 ,]+");
        }

        //Metodo para aceitar apenas numeros no campo Preco
        private void txtPrecoVenda_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9 ,]+");
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}