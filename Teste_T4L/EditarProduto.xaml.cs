﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Teste_T4L.Entities.Enums;

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
            txtDesc.Focus();

            try
            {
                //Carregar itens para ComboBox na tela de edição de Produto;
                Conexao conexao = new Conexao();
                string selectQuery = "SELECT * FROM produto_grupo";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conexao.conectar());

                MySqlDataReader reader = cmd.ExecuteReader();

                //Metodo para pegar o valor exato no banco de dados
                while (reader.Read())
                {
                    cbxGrupoProduto.Items.Add(reader.GetString("nome"));
                }

                conexao.desconectar();

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao carregar combobox");
            }

            cbxUnidade.ItemsSource = Enum.GetValues(typeof(Unidade)); //Carregar combobox com as unidades
        }

        //Botão cancelar para voltar a tela de consulta de itens
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProdutos consultProd = new ConsultaProdutos();
            consultProd.Show();
            this.Close();
        }

        //Salvar edição e passar para a classe Updateproduto para efutar a alteração no bd
        private void btnSalvar_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                //Convertendo Nome do grupo do Produto para o código
                string codigo = ConsultaProdutos.codigo; //Pegando o valor da variavel global inserido no metodo de Editar produto

                //Fazendo a conexão com o bd e passando a query Select para pegar o valor do codigo para usar como key para atualizar os cados no bd
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
                    UpdateProduto upProd = new UpdateProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, ativo, codigo, cbxUnidade.Text);
                    MessageBox.Show(upProd.msg);
                    
                }
                else
                {
                    ativo = 0;
                    UpdateProduto upProd = new UpdateProduto(txtDesc.Text, txtCodBarra.Text, codGrupo, txtPrecoCusto.Text, txtPrecoVenda.Text, ativo, codigo, cbxUnidade.Text);

                }

                conexao.desconectar();

            }
            catch (Exception)
            {
                MessageBox.Show("Verificar se os campos estão preenchidos corretamente!!");
            }

        }

        //Botão para deletar produtos no banco de dados
        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (MessageBox.Show("Deseja deletar o item?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string cod = ConsultaProdutos.codigo; 

                    //Fazendo a conexão com o bd e passando a query Select para pegar o valor do codigo para ver se o mesmo ja foi usado em alguma venda
                    string selectQuery = "SELECT COUNT(1) FROM venda_produto WHERE codProduto = @cod";
                    Conexao conexao = new Conexao();
                    MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                    comando.Parameters.AddWithValue("@cod", cod);

                    var result = comando.ExecuteScalar();
                    int resultado = int.Parse(result.ToString());
                    
                    if (resultado == 0) //Verificar se o produto ja foi usado em alguma venda
                    {

                        //Deletando o produto selecionado.
                        string deleteQuery = "DELETE FROM produto WHERE cod = @cod";
                        MySqlCommand cmd = new MySqlCommand(deleteQuery, conexao.conectar());
                        cmd.Parameters.AddWithValue("@cod", cod);

                        //Executar comandos MySql
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Produto Deletado com Sucesso!!!!");

                        //Limpar Campos
                        txtCodBarra.Clear();
                        txtDesc.Clear();
                        txtPrecoCusto.Clear();
                        txtPrecoVenda.Clear();

                        conexao.desconectar();
                    }
                    else
                    {
                        MessageBox.Show("Produto não pode ser deletado pois já foi usado em Vendas");
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erro no processo!!");
            }
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

        //Metodo para minimizar a janela
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Metodo para maximizar a janela
        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        //Metodo para mover a janela com o mouse
        private void moverJanela_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
