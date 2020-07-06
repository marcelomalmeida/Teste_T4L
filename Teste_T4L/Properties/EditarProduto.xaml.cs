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

        private void btnSalvar_Click(object sender, RoutedEventArgs e) //Salvar edição e passar para a classe Updateproduto para efutar a alteração no bd
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

                ConsultaProdutos conProd = new ConsultaProdutos();
                string codigo = "13";

                if (checkBoxAtivo.IsChecked == true) //Validação para ver se o produto foi ativado
                {

                    ativo = 1;
                    UpdateProduto upProd = new UpdateProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, ativo, codigo);
                    MessageBox.Show(upProd.msg);
                    
                }
                else
                {
                    ativo = 0;
                    UpdateProduto upProd = new UpdateProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, ativo, codigo);
                    MessageBox.Show(upProd.msg);

                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja deletar o item?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int cod = 7;
                    Conexao con = new Conexao();
                    string deleteQuery = "DELETE FROM produto WHERE cod = @cod";
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, con.conectar());
                    cmd.Parameters.AddWithValue("cod", cod);

                    //Executar comandos MySql
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto Deletado com Sucesso");

                    //Limpar Campos
                    txtCodBarra.Clear();
                    txtDesc.Clear();
                    txtPrecoCusto.Clear();
                    txtPrecoVenda.Clear();
                }
                else
                {

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
