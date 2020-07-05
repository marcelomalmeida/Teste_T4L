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
        Conexao conexao = new Conexao();
        MySqlCommand cmd = new MySqlCommand();
        DateTime data = DateTime.Now;
        public String msg;

        public CadastroProduto(string descricao, string codBarra, string codGrupo, string precoCusto, string precoVenda, DateTime data)
        {
            //Comando Sql
            cmd.CommandText = "INSERT INTO produto (descricao, codBarra, codGrupo, precoCusto, precoVenda, dataHoraCadastro) values(@descricao, @codBarra, @codGrupo, @precoCusto, @precoVenda, @dataHoraCadastro)";

            //
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Parameters.AddWithValue("@codBarra", int.Parse(codBarra));
            cmd.Parameters.AddWithValue("@codGrupo", int.Parse(precoCusto));
            cmd.Parameters.AddWithValue("@precoCusto", double.Parse(precoCusto));
            cmd.Parameters.AddWithValue("@precoVenda", double.Parse(precoVenda));
            cmd.Parameters.AddWithValue("@dataHoraCadastro", data);

            try
            {
                //Conectar com BD
                cmd.Connection = conexao.conectar();
                //Executar comando
                cmd.ExecuteNonQuery();
                //Desconectar
                conexao.desconectar();
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
