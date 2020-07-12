using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Schema;
using MySql.Data.MySqlClient;

namespace Teste_T4L
{
    class Venda
    {
        public Venda (string clienteDocumento, string clienteNome, string obs, string total, DateTime data, string codProd, string quantidade)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Insert para inserir os campos desejados
                Conexao conexao = new Conexao();
                string insertQuery = "INSERT INTO venda (clienteDocumento, clienteNome, obs, total, dataHora) " +
                                     "values(@clienteDocumento, @clienteNome, @obs, @total, @dataHora)";
                MySqlCommand comando = new MySqlCommand(insertQuery, conexao.conectar());

                //Passando parametros para SQL
                comando.Parameters.AddWithValue("@clienteDocumento", clienteDocumento);
                comando.Parameters.AddWithValue("@clienteNome", clienteNome);
                comando.Parameters.AddWithValue("@obs", obs);
                comando.Parameters.AddWithValue("@total", total);
                comando.Parameters.AddWithValue("@dataHora", data);

                comando.ExecuteNonQuery(); //Comando de execusao da query
            }
            catch (Exception)
            {
                MessageBox.Show("Erro no Processo!!!");
            }
        }
    }
}
