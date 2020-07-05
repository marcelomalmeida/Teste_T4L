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

        MySqlConnection con = new MySqlConnection();

        //Construtor
        public Conexao()
        {
            con.ConnectionString = "Server=localhost;Database=testdev;Uid=root;Pwd=123456;";
        }

        //Metodo Conectar
        public MySqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        //Metodo Desconectar
        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

        }


    }
}
