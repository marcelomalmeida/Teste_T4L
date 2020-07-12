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
using MySqlX.XDevAPI.Relational;
using System.Xml.Schema;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for NovaVenda.xaml
    /// </summary>
    public partial class NovaVenda : Window
    {
        //Declarações de variaveis globais
        public bool AutoINcrement { get; set; }  //Processo para autoincrementação da coluna Index
        public bool ReadOnly { get; set; }  //Processo para travar edição do da tabela
        public double Total { get; set; }
        public double Subtotal { get; set; }

        DataTable table = new DataTable();

        public NovaVenda()
        {
            InitializeComponent();
            txtCodigo.Focus();

            // Pegando as variaveis globais declaradas no DadosCLiente e colocando eles nas caixas de texto
            string nome = DadosCliente.nome;
            string cpf = DadosCliente.cpf;

            txtNomeCliente.Text = nome;
            txtDocCliente.Text = cpf;

           
            DataColumn index = new DataColumn();
            index.ColumnName = "Index";
            index.DataType = System.Type.GetType("System.Int32");
            index.AutoIncrement = true; //Processo para autoincrementação da coluna Index
            index.AutoIncrementSeed = 1; //Processo para autoincrementação da coluna Index
            index.AutoIncrementStep = 1; //Processo para autoincrementação da coluna Index
            index.ReadOnly = true;

            DataColumn codigo = new DataColumn();
            codigo.ColumnName = "Código";
            codigo.DataType = System.Type.GetType("System.Int32");
            codigo.ReadOnly = true;

            DataColumn descricao = new DataColumn();
            descricao.ColumnName = "Descrição";
            descricao.ReadOnly = true;

            DataColumn quantidade = new DataColumn();
            quantidade.ColumnName = "Quantidade";
            quantidade.DataType = System.Type.GetType("System.Decimal");
            quantidade.ReadOnly = true;

            DataColumn valor = new DataColumn();
            valor.ColumnName = "Valor Unitário";
            valor.DataType = System.Type.GetType("System.Decimal");
            valor.ReadOnly = true;

            DataColumn valorTotal = new DataColumn();
            valorTotal.ColumnName = "Valor Total";
            valorTotal.DataType = System.Type.GetType("System.Decimal");
            valorTotal.ReadOnly = true;

            //Adicionando colunas para a tabela criada
            table.Columns.Add(index);
            table.Columns.Add(codigo);
            table.Columns.Add(descricao);
            table.Columns.Add(quantidade);
            table.Columns.Add(valor);
            table.Columns.Add(valorTotal);

            dataGridPedVenda.ItemsSource = table.DefaultView; //Inserindo colunas no datagrid

        }

        //Metodo para reconhecer a tecla enter e pesquisar o código
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            string str = e.Key.ToString();

            if (str == "Return") //Processo para reconhecer a tecla enter
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
                                txtQuantidade.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Produto não esta ativo!");
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
                    MessageBox.Show("Erro no processo!!!");
                }
            }

        }

        //Método para deixar entrar apenas numeros na quantidade
        private void txtQuantidade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9 ,]+");
        }

        //Metodo para reconhecer a tecla enter e inserir os campos nas linhas da tabela
        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            string str = e.Key.ToString(); //Processo para reconhecer a tecla enter
            if (str == "Return")
            {
                int? n = Convert.ToInt32(txtQuantidade.Text);//Processo para aceitar apenas quantidades positivas
                if (n > 0)
                {
                    try
                    {
                        //Fazendo a conexão com o bd e passando a query Select para pegar os valores do bd
                        Conexao conexao = new Conexao();
                        string selectQuery = "SELECT descricao, precoVenda FROM produto WHERE cod = @cod";
                        MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                        comando.Parameters.AddWithValue("@cod", txtCodigo.Text);

                        //Processo para ler os dados do bd
                        comando.CommandType = CommandType.Text;
                        MySqlDataReader reader = comando.ExecuteReader();
                        reader.Read();

                        string descricao = reader.GetString(0);
                        double prVenda = reader.GetDouble(1);
                        double valorTotal = prVenda * Convert.ToDouble(txtQuantidade.Text);

                        //Inserindo dados nas linhas da tableda criada
                        table.Rows.Add(null, Convert.ToInt32(txtCodigo.Text), descricao, Convert.ToDouble(txtQuantidade.Text), prVenda, valorTotal);
                        dataGridPedVenda.ItemsSource = table.DefaultView;

                        //Processo para o adquirir o total do pedido de vendas
                        foreach (DataRow row in table.Rows)
                        {
                            Subtotal = valorTotal + Total;

                        }

                        Total = Subtotal;
                        txtValorTotal.Text = Total.ToString(); //Adicionando o Subtotal para a caixa de texto
                        txtCodigo.Focus();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro no Processo!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Quantidade tem que ser maior que 0");
                }
                
            }
        }

        //Metodo para o botão voltar na tela de pedido
        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja fechar o pedido sem salvar?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MenuInicial menuinicial = new MenuInicial();
                menuinicial.Show();

                this.Close();
            }
            
        }

        private void btnFinalVenda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja finalizar o pedido?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string obs = txtObs.Text;

                    //Passando os valores dos campos digitados para a classe Venda
                    Venda venda = new Venda(txtDocCliente.Text, txtNomeCliente.Text, obs, txtValorTotal.Text, DateTime.Now, txtCodigo.Text, txtQuantidade.Text);
                    DataRowView dr = dataGridPedVenda.SelectedItem as DataRowView;

                    foreach (DataRow row in table.Rows)// Pegando os parametros de cada linha do novo pedido e passando para a classe ProdVenda
                    {

                        string codProd = row.ItemArray[1].ToString();
                        string quantidade = row.ItemArray[3].ToString();
                        string precoVenda = row.ItemArray[4].ToString();

                        ProdVenda prodVenda = new ProdVenda(codProd, quantidade, precoVenda);

                    }

                    MessageBox.Show("Venda finalizada com sucesso!!!");
                    

                    //Iniciar uma nova venda
                    if (MessageBox.Show("Deseja informar o nome e/ou CPF do cliente para a nova venda?", "Cliente", MessageBoxButton.YesNo) == MessageBoxResult.Yes) // O Programa aceita pedido de venda sem dados do cliente
                    {
                        DadosCliente dadosCliente = new DadosCliente();
                        dadosCliente.Show();
                        this.Close();
                    }
                    else
                    {
                        NovaVenda novaVenda = new NovaVenda();
                        novaVenda.Show();
                        this.Close();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Erro no processo!!");
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
