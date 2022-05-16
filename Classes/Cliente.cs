using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoBancoConsole.Classes
{
    internal class Cliente : Conexao
    {

        public string nome;
        public int idCliente;
        public int idade;
        public double saldo;
        public int idContaBancaria;

        public bool ClienteExiste(int contaBancaria)
        {
            try
            {
                CriarConexao();
                SqlCommand comandoPesquisa = new SqlCommand("SELECT IdContaBancaria FROM Cliente WHERE IdContaBancaria = @contaBancaria ", conn);
                comandoPesquisa.Parameters.AddWithValue("@contaBancaria", contaBancaria);
                SqlDataReader executaPesquisa = comandoPesquisa.ExecuteReader();

                if (executaPesquisa.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.idContaBancaria = (int)executaPesquisa["IdContaBancaria"];
                    executaPesquisa.Close();
                    return true;

                }
                else { return false; }
            }
            catch (Exception e) { throw new Exception("Error : " + e.Message); }
            finally { FecharConexao(); }
        }

        public bool ClientePossuiCadastro(string nomeNovoCliente)
        {
            try
            {
                CriarConexao();
                SqlCommand comandoPesquisa = new SqlCommand("SELECT nome FROM Cliente WHERE nome = @nomeNovoCliente", conn);
                comandoPesquisa.Parameters.AddWithValue("@nomeNovoCliente", nomeNovoCliente);
                SqlDataReader executaPesquisa = comandoPesquisa.ExecuteReader();
                //SqlDataReader dr = comandoPesquisa.ExecuteReader();


                if (executaPesquisa.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.nome = (string)executaPesquisa["nome"];

                    executaPesquisa.Close();
                    return true;
                }
                else { return false; }
            }
            catch (Exception e) { throw new Exception("Error : " + e.Message); }
            finally { FecharConexao(); }
        }

        public string InformacoesContaCliente(int contaBancaria)
        {
            try
            {
                CriarConexao();
                SqlCommand comandoPesquisaInformacoesCliente = new SqlCommand("SELECT * FROM Cliente WHERE IdContaBancaria = @contaBancaria ", conn);
                comandoPesquisaInformacoesCliente.Parameters.AddWithValue("@contaBancaria", contaBancaria);
                SqlDataReader executaPesquisa = comandoPesquisaInformacoesCliente.ExecuteReader();


                if (executaPesquisa.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.nome = (string)executaPesquisa["nome"];
                    cliente.idContaBancaria = (int)executaPesquisa["IdContaBancaria"];
                    return ("                          Nome: " + cliente.nome + 
                          "\n                          Conta: " + cliente.idContaBancaria);
                    

                    executaPesquisa.Close();
                    /*return true;*/

                }
                else { return (""); }
            }
            catch (Exception e) { throw new Exception("Error : " + e.Message); }
            finally { FecharConexao(); }
        }
    }
}
