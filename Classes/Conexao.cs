using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoBancoConsole.Classes
{
    internal abstract class Conexao
    {
        protected SqlConnection conn;

        public void CriarConexao()
        {
            string datasource = @"172.20.13.96";//your server or S1189
            string database = "TesteProjetoBanco"; //your database name
            string username = "axel"; //username of server to connect
            string password = "axel123"; //password

            string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        public void FecharConexao()
        {
            conn.Close();
        }
    }
}
