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
            
            // Pegando as variaveis globais declaradas no DadosCLiente e colocando eles nas caixas de texto
            string nome = DadosCliente.nome;
            string cpf = DadosCliente.cpf;

            txtNomeCliente.Text = nome;
            txtDocCliente.Text = cpf;
        }

        //Metodo para reconhecer a tecla enter
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

    }
}
