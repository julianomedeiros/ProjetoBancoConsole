using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoBancoConsole.Classes
{
    internal class Historico : OperacoesBancarias
    {
        //public int idContaBancaria; a variavel ja se encontra na classe que esta herdando
        public string descricao;
        public float valorMovimentacao;
        public float valorFinal;


        public void Depositar(int idContaBancaria, float valorMovimentacao, float valorFinal)
        {
            try
            {
                CriarConexao();
                SqlCommand comando = new SqlCommand(
                    "INSERT INTO Historico(idContaBancaria, descricao, valorMovimentacao, valorFinal)" +
                    "VALUES(@idContaBancaria, 'DEPÓSITO', @valorMovimentacao, @valorFinal)", conn);

                comando.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                comando.Parameters.AddWithValue("@valorMovimentacao", valorMovimentacao);
                comando.Parameters.AddWithValue("@valorFinal", valorFinal);

                SqlDataReader dr = comando.ExecuteReader();

            }
            catch (Exception e) { Console.WriteLine("erro : " + e.Message); }
            finally { FecharConexao(); }
        }

        public void Recebe(int idContaBancaria, float valorMovimentacao, float valorFinal)
        {
            try
            {

                CriarConexao();
                SqlCommand comando = new SqlCommand(
                    "INSERT INTO Historico(idContaBancaria, descricao, valorMovimentacao, valorFinal)" +
                    "VALUES(@idContaBancaria, 'RECEBE', @valorMovimentacao, @valorFinal)", conn);

                comando.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                comando.Parameters.AddWithValue("@valorMovimentacao", valorMovimentacao);
                comando.Parameters.AddWithValue("@valorFinal", valorFinal);

                SqlDataReader dr = comando.ExecuteReader();

            }
            catch (Exception e) { Console.WriteLine("erro : " + e.Message); }
            finally { FecharConexao(); }
        }

        public void Transfere(int idContaBancaria, float valorMovimentacao, float valorFinal)
        {
            try
            {

                CriarConexao();
                SqlCommand comando = new SqlCommand(
                    "INSERT INTO Historico(idContaBancaria, descricao, valorMovimentacao, valorFinal)" +
                    "VALUES(@idContaBancaria, 'TRANSFERENCIA', @valorMovimentacao, @valorFinal)", conn);

                comando.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                comando.Parameters.AddWithValue("@valorMovimentacao", valorMovimentacao);
                comando.Parameters.AddWithValue("@valorFinal", valorFinal);
                SqlDataReader dr = comando.ExecuteReader();

            }
            catch (Exception e) { Console.WriteLine("erro : " + e.Message); }
            finally { FecharConexao(); }
        }

        public void Retirada(int idContaBancaria, float valorMovimentacao, float valorFinal)
        {
            try
            {
                CriarConexao();
                SqlCommand comando = new SqlCommand(
                    "INSERT INTO Historico(idContaBancaria, descricao, valorMovimentacao, valorFinal)" +
                    "VALUES(@idContaBancaria, 'SACA', @valorMovimentacao, @valorFinal)", conn);

                comando.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                comando.Parameters.AddWithValue("@valorMovimentacao", valorMovimentacao);
                comando.Parameters.AddWithValue("@valorFinal", valorFinal);
                SqlDataReader dr = comando.ExecuteReader();
                dr.Close();

            }
            catch (Exception e) { Console.WriteLine("erro : " + e.Message); }
            finally { FecharConexao(); }
        }

    }
}
