using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_T4L
{
    public class Conexao
    {

        MySqlConnection conexao = new MySqlConnection();

        //Construtor
        public Conexao()
        {
            conexao.ConnectionString = "Server=localhost;Database=testdev;Uid=root;Pwd=123456;";
        }

        //Metodo Conectar
        public MySqlConnection conectar()
        {
            if (conexao.State == System.Data.ConnectionState.Closed)
            {
                conexao.Open();
            }
            return conexao;
        }

        //Metodo Desconectar
        public void desconectar()
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }

        }


    }
}
