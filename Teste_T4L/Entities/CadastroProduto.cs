using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Teste_T4L
{
    class CadastroProduto
    {
        
        public CadastroProduto(string descricao, string codBarra, string codGrupo, string precoCusto, string precoVenda, DateTime data, int ativo, string unidade)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Insert para inserir os campos desejados
                Conexao conexao = new Conexao();
                string insertQuery = "INSERT INTO produto (descricao, codBarra, codGrupo, precoCusto, precoVenda, dataHoraCadastro, ativo, unidade) " +
                                     "values(@descricao, @codBarra, @codGrupo, @precoCusto, @precoVenda, @dataHoraCadastro, @ativo, @unidade)";
                MySqlCommand comando = new MySqlCommand(insertQuery, conexao.conectar());
               
                //Passando parametros para SQL
                comando.Parameters.AddWithValue("@descricao", descricao);
                comando.Parameters.AddWithValue("@codGrupo", int.Parse(codGrupo));
                comando.Parameters.AddWithValue("@precoCusto", double.Parse(precoCusto));
                comando.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
                comando.Parameters.AddWithValue("@dataHoraCadastro", data);
                comando.Parameters.AddWithValue("@ativo", ativo);
                comando.Parameters.AddWithValue("@unidade", unidade);

                //Testando o campo codigo de barras, se o mesmo não for preenchido ele será um null no bd
                if (codBarra == "")
                {
                    codBarra = null;
                    comando.Parameters.AddWithValue("@codBarra", codBarra);
                }
                else
                {
                    comando.Parameters.AddWithValue("@codBarra", codBarra);
                }
 
                comando.ExecuteNonQuery(); //Comando de execusao da querya
                conexao.desconectar(); //Metodo para desconectar do banco de dados

                MessageBox.Show("Produto Cadastrado com Sucesso!");
            }
            catch (Exception)
            {
                MessageBox.Show("Verificar Preenchimento!");
            }

        }
    }
}
