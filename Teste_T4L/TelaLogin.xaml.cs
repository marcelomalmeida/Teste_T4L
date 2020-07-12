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
using System.Data;
using MySql.Data.MySqlClient;


namespace Teste_T4L
{
    /// <summary>
    /// Interaction logic for TelaLogin.xaml
    /// </summary>
    public partial class TelaLogin : Window
    {
        public TelaLogin()
        {
            InitializeComponent();
            txtLogin.Focus();
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Select para verificar se o usuario e senha digitados existem no bd
                int i = 0;
                Conexao conexao = new Conexao();
                string selectQuery = "SELECT * FROM usuarios where usuario = '" + txtLogin.Text + "' and senha = '" + txtSenha.Password + "'";
                DataTable table = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectQuery, conexao.conectar());

                dataAdapter.Fill(table);
                i = Convert.ToInt32(table.Rows.Count.ToString());

                if (i == 0)
                {
                    MessageBox.Show("Usuário ou Senha Invalido");
                }

                else
                { 
                    MenuInicial menuInicial = new MenuInicial();
                    menuInicial.Show();

                    this.Close();
                }

                conexao.desconectar();
                
            }
            catch(Exception)
            {
                MessageBox.Show("Erro ao se conectar com o Banco de Dados!!");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
