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
            MessageBox.Show("Vai Entrar no try do prodvenda");
            try
            {
                //Fazendo a conexão com o bd e passando a query Insert para inserir os campos desejados
                Conexao conexao = new Conexao();

                string selectMax = "SELECT MAX(cod) from venda";
                MySqlCommand comando = new MySqlCommand(selectMax, conexao.conectar());
                comando.CommandType = CommandType.Text;
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                string codVenda = reader.GetString("cod");
                MessageBox.Show(codVenda);
                //---------------------------------------------------------------------------------------------------
                /*string insertQuery = "INSERT INTO venda_produto(codVenda, codProduto, precoVenda, quantidade) " +
                                     "values(@codVenda, @codProduto, @precoVenda, @quantidade)";
                MySqlCommand comando2 = new MySqlCommand(insertQuery, conexao.conectar());

                //Passando parametros para SQL
                comando2.Parameters.AddWithValue("@codVenda", int.Parse(codVenda));
                comando2.Parameters.AddWithValue("@codProduto", int.Parse(codProd));
                comando2.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
                comando2.Parameters.AddWithValue("@quantidade", double.Parse(quantidade));

                comando2.ExecuteNonQuery(); //Comando de execusao da query;

                MessageBox.Show("Venda Produto Cadastrado Sucesso");*/


            }

            catch (Exception)
            {
                MessageBox.Show("Erro no processo!!!!");
            }
        }
    }
}
