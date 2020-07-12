using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Teste_T4L
{
    class UpdateProduto
    {
        public string msg;

        public UpdateProduto(string descricao, string codBarra, string codGrupo, string precoCusto, string precoVenda, int ativo, string cod)
        {
            try
            {
                //Fazendo a conexão com o bd e passando a query Update para atualizar os campos desejados
                Conexao conexao = new Conexao();
                string updateQuery = "UPDATE produto SET descricao = @descricao, codBarra = @codBarra, codGrupo = @codGrupo, precoCusto = @precoCusto, " +
                                      "precoVenda = @precoVenda, ativo = @ativo WHERE cod = @cod";
                MySqlCommand comando = new MySqlCommand(updateQuery, conexao.conectar());

                //Passando parametros para SQL
                comando.Parameters.AddWithValue("@descricao", descricao);
                comando.Parameters.AddWithValue("@codGrupo", int.Parse(codGrupo));
                comando.Parameters.AddWithValue("@precoCusto", double.Parse(precoCusto));
                comando.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
                comando.Parameters.AddWithValue("@ativo", ativo);
                comando.Parameters.AddWithValue("@cod", int.Parse(cod));

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

                comando.ExecuteNonQuery(); //Comando de execusao da query
                conexao.desconectar(); //Metodo para desconectar do banco de dados
                
                this.msg = "Alterado com sucesso!";
            }
            catch (Exception)
            { 
                
                this.msg = "Erro ao se conectar com o banco de dados";
            }

        }
    }
}
