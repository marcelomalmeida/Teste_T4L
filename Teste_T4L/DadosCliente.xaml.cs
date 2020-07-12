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

namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for DadosCliente.xaml
    /// </summary>
    public partial class DadosCliente : Window
    {
        //Declaração de variavel global e metodo de GET e SET para utilização em outra classe
        private static string _nome;
        private static string _cpf;
        
        public static string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public static string cpf
        {
            get { return _cpf;  }
            set { _cpf = value; }
        }

        public DadosCliente()
        {
            InitializeComponent();
            txtDadoNome.Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) //Clicar nesse botão, os valores digitados no txt serão carregados para o formulario de nova venda
        {
            nome = txtDadoNome.Text;
            string str = txtDadoCPF.Text;
            int valid = str.Length;

            if (valid == 11) //Validando numeros CPF
            {
                cpf = Convert.ToUInt64(str).ToString(@"000\.000\.000\-00"); //Formatando CPF
                NovaVenda novaVenda = new NovaVenda();
                novaVenda.Show();
                
                this.Close();
            }
            else if (str == "")
            {
                NovaVenda novaVenda = new NovaVenda();
                novaVenda.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("CPF Incorreto");
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) // clicar no botão para ir para o formulario de nova venda sem carregar nome e documento do cliente
        {
            NovaVenda novaVenda = new NovaVenda();
            novaVenda.Show();

            this.Close();
        }

        //Método para aceitar apenas numeros no CPF
        private void txtDadoCPF_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
