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

        //Metodo para Abrir cadastro de produtos
        private void btnCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProd cadProd = new CadastrarProd();
            cadProd.Show();
            this.Close();
        }

        //Metodod para Abrir consulta de produtos
        private void btnConsultProd_Click(object sender, RoutedEventArgs e)
        {
            ConsultaProdutos conProd = new ConsultaProdutos();
            conProd.Show();
            this.Close();
        }

        //MetodoFechar o programa
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja encerrar o programa?", "Atenção!", MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
            {
                this.Close();
            }
        }

        //Metodo para novo pedido
        private void btnNovoPedido_Click(object sender, RoutedEventArgs e)
        {
            //Perguntando se deseja informar dados do cliente
            if (MessageBox.Show("Deseja informar o nome e/ou CPF do cliente?", "Cliente", MessageBoxButton.YesNo) == MessageBoxResult.Yes) // O Programa aceita pedido de venda sem dados do cliente
            {
                DadosCliente dadosCliente = new DadosCliente();
                dadosCliente.ShowDialog();
                this.Close();
            }
            else
            {
                NovaVenda novaVenda = new NovaVenda();
                novaVenda.Show();
                this.Close();
            }
        }

        //Metodo para Minimizar a janela
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Método para Maximizar a janela
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

        //Metodo para usar o mouse para mover a janela
        private void moverJanela_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
