using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Teste_T4L
{
    class ProdVenda
    {
        public ProdVenda(string codProd, string quantidade, string precoVenda)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Select para pegar o ultimo código de venda gerado pelo bd 
                Conexao conexao = new Conexao();
                string selectMax = "SELECT MAX(cod) from venda";
                MySqlCommand comando = new MySqlCommand(selectMax, conexao.conectar());
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                string codVenda = reader.GetString(0);
                conexao.desconectar();

                //Fazendo a conexão com o bd e passando a query Insert para inserir os dados no bd
                Conexao conexao1 = new Conexao();
                string insertQuery = "INSERT INTO venda_produto(codVenda, codProduto, precoVenda, quantidade) " +
                                     "values(@codVenda, @codProduto, @precoVenda, @quantidade)";
                MySqlCommand comando2 = new MySqlCommand(insertQuery, conexao1.conectar());

                //Passando parametros para SQL
                comando2.Parameters.AddWithValue("@codVenda", int.Parse(codVenda));
                comando2.Parameters.AddWithValue("@codProduto", int.Parse(codProd));
                comando2.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
                comando2.Parameters.AddWithValue("@quantidade", double.Parse(quantidade));

                comando2.ExecuteNonQuery(); //Comando de execusao da query

                conexao.desconectar();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro no processo!!!!");
            }
        }
    }
}
