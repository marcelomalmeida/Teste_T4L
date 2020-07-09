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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Reflection;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for NovaVenda.xaml
    /// </summary>
    public partial class NovaVenda : Window
    {
   
        public NovaVenda()
        {
            InitializeComponent();
            txtCodigo.Focus();
                      
            // Pegando as variaveis globais declaradas no DadosCLiente e colocando eles nas caixas de texto
            string nome = DadosCliente.nome;
            string cpf = DadosCliente.cpf;

            txtNomeCliente.Text = nome;
            txtDocCliente.Text = cpf;

        }

        //Metodo para reconhecer a tecla enter e pesquisar o código
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            string str = e.Key.ToString();

            if (str == "Return")
            {
                try
                {
                    string cod = txtCodigo.Text;

                    //Fazendo a conexão com o bd e passando a query Select para verificar se o produto esta cadastrado
                    string selectQuery = "SELECT COUNT(1) FROM produto WHERE cod = @cod";
                    Conexao conexao = new Conexao();
                    MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                    comando.Parameters.AddWithValue("@cod", cod);
                                        
                    var result = comando.ExecuteScalar();
                    int resultado = int.Parse(result.ToString());

                    if (resultado > 0)
                    {
                        //Fazendo a conexão com o bd e passando a query Select para verificar se o produto esta Ativo
                        string selectQuerySeg = "SELECT ativo FROM produto WHERE cod = @cod";
                        MySqlCommand cmd = new MySqlCommand(selectQuerySeg, conexao.conectar());
                        cmd.Parameters.AddWithValue("@cod", cod);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        //Metodo para pegar o valor exato no banco de dados
                        while (reader.Read())
                        {
                            string ativo = reader.GetValue(0).ToString();

                            if (ativo == "True")
                            {
                                MessageBox.Show("Produto OK");
                            }
                            else
                            {
                                MessageBox.Show("Produto nao OK");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Produto não Cadastrado!!!!");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("catch");
                }
            }
       
        }

        private void txtQuantidade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            string str = e.Key.ToString();
            if (str == "Return")
            {
                int n = Convert.ToInt32(txtQuantidade.Text);
                if (n > 0)
                {
                    MessageBox.Show("Vai entrar no try");
                    try
                    {
                        Conexao conexao = new Conexao();
                        string selectQuery = "SELECT descricao, precoVenda FROM produto WHERE cod = @cod";
                        MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                        comando.Parameters.AddWithValue("@cod", txtCodigo.Text);

                        comando.CommandType = CommandType.Text;
                        MySqlDataReader reader = comando.ExecuteReader();
                        reader.Read();
                        int index = 1;
                        string desc = reader.GetString(0);
                        double prVenda = reader.GetDouble(1);
                        double valorTotal = prVenda * Convert.ToDouble(txtQuantidade.Text);

                        /*ProdVenda prodVenda = new ProdVenda(index, Convert.ToInt32(txtCodigo.Text), desc, prVenda, Convert.ToDouble(txtQuantidade), valorTotal);
                        ProdVenda pro = dataGridPedVenda.SelectedItem as ProdVenda;
                        dataGridPedVenda.Items.Add(desc);*/

                        DataTable table = new DataTable();
                        DataRow row;
                        table.Columns.Add("Index", typeof(int));
                        table.Columns.Add("Cod", typeof(int));
                        table.Columns.Add("Quantidade", typeof(double));
                        table.Columns.Add("Descrição", typeof(string));
                        table.Columns.Add("Valor", typeof(double));
                        table.Columns.Add("Valor Total", typeof(double));
                    
                        /*table.Rows.Add(index, txtCodigo.Text, txtQuantidade.Text, desc, prVenda, valorTotal);
                        dataGridPedVenda.ItemsSource = table.DefaultView;*/

                        for (int i = 1; i < 10; i++)
                        {
                            row = table.NewRow();
                            row["Index"] = 1;
                            row["Cod"] = txtCodigo.Text;
                            row["Quantidade"] = txtQuantidade.Text;
                            row["Descrição"] = desc;
                            row["Valor"] = prVenda;
                            row["Valor Total"] = valorTotal;

                            table.Rows.Add(row);
                        }

                        dataGridPedVenda.ItemsSource = table.DefaultView;


                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Quantidade tem que ser maior que 0");
                }
            }
        }

    }
}
