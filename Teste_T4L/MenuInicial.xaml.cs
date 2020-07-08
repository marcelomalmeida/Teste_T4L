﻿using System;
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

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for MenuInicial.xaml
    /// </summary>
    public partial class MenuInicial : Window
    {
        public MenuInicial()
        {
            InitializeComponent();
        }

        private void btnCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProd cadProd = new CadastrarProd();
            cadProd.Show();
            this.Close();
        }

        private void btnConsultProd_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProdutos conProd = new ConsultaProdutos();
            conProd.Show();
            this.Close();
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNovoPedido_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja informar o nome e/ou CPF do cliente?", "Cliente", MessageBoxButton.YesNo) == MessageBoxResult.Yes) // O Programa aceita pedido de venda sem dados do cliente
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
}
