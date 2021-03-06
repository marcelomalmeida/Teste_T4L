﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using MySql.Data.MySqlClient;
using Teste_T4L.Properties;

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for ConsultaProdutos.xaml
    /// </summary>
    public partial class ConsultaProdutos : Window
    {
        //Processo para arquivar o codigo em uma variavel para pode acessa-la em outras classes
        private static string _codigo;
        public static string codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }


        public ConsultaProdutos()
        {
            InitializeComponent();
        }

        //Metodo para voltar para o menu inicial
        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MenuInicial mInicial = new MenuInicial();
            mInicial.Show();
            this.Close();
        }

        //Metodo para carregar os produtos do BD no Datagrid
        private void btnCarrProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Select para pegar os valores e colocar no dataGrids
                Conexao conexao = new Conexao();
                string selectQuery = "SELECT PRODUTO.cod as Código, PRODUTO.descricao as Descrição, PRODUTO_GRUPO.nome as Grupo, " +
                                     "PRODUTO.precoCusto as PrecoCusto, PRODUTO.precoVenda as PrecoVenda, PRODUTO.ativo as Ativo, PRODUTO.unidade as Unidade FROM " +
                                     "PRODUTO INNER JOIN PRODUTO_GRUPO ON PRODUTO.codGrupo = PRODUTO_GRUPO.cod ORDER BY 1";
                DataTable table = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectQuery, conexao.conectar());
                dataAdapter.Fill(table);
                dataGridConsult.ItemsSource = table.DefaultView;
            }

            catch (Exception)
            {
                MessageBox.Show("Erro no processo!!!");
            }
        }

        //Metodo para Carregar tela de edição de produtos conforme linha selecionada no dataGrid
        private void btnEditProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditarProduto editProd = new EditarProduto();
                DataRowView dr = dataGridConsult.SelectedItem as DataRowView;
                if (dr != null)
                {
                    editProd.Show();
                    this.Close();
                    string cod;

                    //Pegando os itens selecionados no dataGrid
                    editProd.txtDesc.Text = dr["Descrição"].ToString();
                    editProd.txtPrecoCusto.Text = dr["PrecoCusto"].ToString();
                    editProd.txtPrecoVenda.Text = dr["precoVenda"].ToString();
                    editProd.cbxGrupoProduto.Text = dr["Grupo"].ToString();
                    cod = dr["Código"].ToString();
                    editProd.cbxUnidade.Text = dr["Unidade"].ToString();

                    ConsultaProdutos.codigo = cod; //Passando o valor do codigo para a variavel global

                    //Pegando o Código de Barras do bdd com base no Código do produto
                    Conexao conexao = new Conexao();
                    string selectQuery = "SELECT codBarra FROM produto WHERE cod = ?";
                    MySqlCommand comando = new MySqlCommand(selectQuery, conexao.conectar());
                    comando.Parameters.Add("@cod", MySqlDbType.String).Value = dr["Código"].ToString();
                    comando.CommandType = CommandType.Text;

                    MySqlDataReader reader = comando.ExecuteReader();
                    reader.Read();

                    //Pegando o valor do código de barras e verificando se o mesmo é um valor null
                    if (!reader.IsDBNull(reader.GetOrdinal("codBarra")))
                    {
                        string codBarra = reader.GetString("codBarra");
                        editProd.txtCodBarra.Text = codBarra;
                    }
                    else
                    {
                        editProd.txtCodBarra.Text = "";
                    }

                    //Verificação se o produto esta com o checkBox Ativo ou não
                    if (dr["Ativo"].ToString() == "True")
                    {
                        editProd.checkBoxAtivo.IsChecked = true;
                    }
                    else
                    {
                        editProd.checkBoxAtivo.IsChecked = false;
                    }

                    conexao.desconectar();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro no processo!!!");
            }

        }

        //Metodo para Minimizar a janela
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Metodo para Maximizar a janela
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
