using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Teste_T4L.Properties;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for ConsultaProdutos.xaml
    /// </summary>
    public partial class ConsultaProdutos : Window
    {
        public ConsultaProdutos()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MenuInicial mInicial = new MenuInicial();
            mInicial.Show();
            this.Close();
        }

        private void btnCarrProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Carregar a dataGrid com as informações do bd
                Conexao con = new Conexao();
                string selectQuery = "SELECT PRODUTO.cod as Código, PRODUTO.descricao as Descrição, PRODUTO_GRUPO.nome as Grupo, " +
                                     "PRODUTO.precoCusto as PrecoCusto, PRODUTO.precoVenda as PrecoVenda, PRODUTO.ativo as Ativo FROM PRODUTO INNER JOIN PRODUTO_GRUPO ON PRODUTO.codGrupo = PRODUTO_GRUPO.cod";
                DataTable table = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectQuery, con.conectar());
                dataAdapter.Fill(table);
                dataGridConsult.ItemsSource = table.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditProd_Click(object sender, RoutedEventArgs e)
        {
            EditarProduto editProd = new EditarProduto();
            DataRowView dr = dataGridConsult.SelectedItem as DataRowView;
            if (dr != null)
            {
                editProd.Show();
                this.Close();

                Conexao con = new Conexao();
                
                //Pegando os itens selecionados no dataGrid
                editProd.txtDesc.Text = dr["Descrição"].ToString();
                editProd.txtPrecoCusto.Text = dr["PrecoCusto"].ToString();
                editProd.txtPrecoVenda.Text = dr["precoVenda"].ToString();
                editProd.cbxGrupoProduto.Text = dr["Grupo"].ToString();

                //Pegando o Código de Barras do bdd com base no Código do produto
                string selectQuery = "SELECT codBarra FROM produto WHERE cod = ?";
                MySqlCommand cmd = new MySqlCommand(selectQuery, con.conectar());
                cmd.Parameters.Add("@cod", MySqlDbType.String).Value = dr["Código"].ToString();
                cmd.CommandType = CommandType.Text;
                
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                
                string codBarra = reader.GetString("codBarra");
                editProd.txtCodBarra.Text = codBarra;

                //Verificação se o produto esta Ativo ou não
                if (dr["Ativo"].ToString() == "True")
                {
                    editProd.checkBoxAtivo.IsChecked = true;
                }
                else
                {
                    editProd.checkBoxAtivo.IsChecked = false;
                }
            }

        }
    }

}
