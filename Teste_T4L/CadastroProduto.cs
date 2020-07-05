using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Teste_T4L
{
    class CadastroProduto
    {
        Conexao con = new Conexao();
        MySqlCommand cmd = new MySqlCommand();
        DateTime data = DateTime.Now;
        public String msg;

        public CadastroProduto(string descricao, string codBarra, string codGrupo, string precoCusto, string precoVenda, DateTime data, int ativo)
        {
            //Comando Sql
            cmd.CommandText = "INSERT INTO produto (descricao, codBarra, codGrupo, precoCusto, precoVenda, dataHoraCadastro, ativo) values(@descricao, @codBarra, @codGrupo, @precoCusto, @precoVenda, @dataHoraCadastro, @ativo)";

            //Passando parametros para SQL
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Parameters.AddWithValue("@codBarra", int.Parse(codBarra));
            cmd.Parameters.AddWithValue("@codGrupo", int.Parse(codGrupo));
            cmd.Parameters.AddWithValue("@precoCusto", double.Parse(precoCusto));
            cmd.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
            cmd.Parameters.AddWithValue("@dataHoraCadastro", data);
            cmd.Parameters.AddWithValue("@ativo", ativo);

            try
            {
                //Conectar com BD
                cmd.Connection = con.conectar();
                //Executar os comandos SQL
                cmd.ExecuteNonQuery();
                //Desconectar
                con.desconectar();
                //Mostrar msg Sucesso
                this.msg = "Cadastrado com Sucesso!";

            }
            catch (MySqlException e)
            {
                //Mostrar msg de Erro
                this.msg = "Erro ao se conectar com o banco de dados";
            }

        }
    }
}
