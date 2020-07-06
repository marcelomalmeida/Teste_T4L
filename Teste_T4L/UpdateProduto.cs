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
        Conexao con = new Conexao();
        MySqlCommand cmd = new MySqlCommand();
        public String msg;

        public UpdateProduto(string descricao, string codBarra, string codGrupo, string precoCusto, string precoVenda, int ativo, string cod)
        {
            //Comando Sql
            //cmd.CommandText = "INSERT INTO produto (descricao, codBarra, codGrupo, precoCusto, precoVenda, ativo) values (@descricao, @codBarra, @codGrupo, @precoCusto, @precoVenda, @ativo)";
            cmd.CommandText = "UPDATE produto SET descricao = @descricao, codBarra = @codBarra, codGrupo = @codGrupo, precoCusto = @precoCusto, precoVenda = @precoVenda, ativo = @ativo WHERE cod = @cod";
            //cmd.CommandText = "UPDATE produto SET (descricao, codBarra, codGrupo, precoCusto, precoVenda, ativo, cod) values(@descricao, @codBarra, @codGrupo, @precoCusto, @precoVenda, @dataHoraCadastro, @ativo, @cod) ";

            

            //Passando parametros para SQL
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Parameters.AddWithValue("@codGrupo", int.Parse(codGrupo));
            cmd.Parameters.AddWithValue("@precoCusto", double.Parse(precoCusto));
            cmd.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
            cmd.Parameters.AddWithValue("@ativo", ativo);
            cmd.Parameters.AddWithValue("@cod", int.Parse(cod));

            //Se o campo Código de barras não for preenchido ele será um null no bd
            if (codBarra == "")
            {
                codBarra = null;
                cmd.Parameters.AddWithValue("@codBarra", codBarra);
            }
            else
            {
                cmd.Parameters.AddWithValue("@codBarra", codBarra);
            }

            try
            {
                //Conectar com BD
                cmd.Connection = con.conectar();
                //Executar os comandos SQL
                cmd.ExecuteNonQuery();
                //Desconectar
                con.desconectar();
                //Mostrar msg Sucesso
                this.msg = "Alterado com sucesso!";

            }
            catch (MySqlException e)
            {
                //Mostrar msg de Erro
                this.msg = "Erro ao se conectar com o banco de dados";
            }

        }
    }
}
