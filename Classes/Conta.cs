using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoBancoConsole.Classes
{
    internal class Conta : Conexao
    {
        public int idContaBancaria;
        public int idCliente;
        public float saldo;
        public int idHistorico;
        private int idContaCliente;

        public bool CadastrarNovoCliente(string nomeNovoCliente, int idadeNovoCliente)
        {
            try
            {
                CriarConexao();

                Cliente novoCliente = new Cliente();

                if (!novoCliente.ClientePossuiCadastro(nomeNovoCliente) && idadeNovoCliente >= 18 && idadeNovoCliente <= 120)
                {
                    try
                    {
                        //CADASTRA NOVO CLIENTE 
                        SqlCommand cadastraCliente = new SqlCommand("INSERT INTO Cliente (nome, idade) values (@nomeNovoCliente, @idadeNovoCliente)", conn);
                        cadastraCliente.Parameters.AddWithValue("@nomeNovoCliente", nomeNovoCliente.ToUpper());
                        cadastraCliente.Parameters.AddWithValue("@idadeNovoCliente", idadeNovoCliente);

                        SqlDataReader dr = cadastraCliente.ExecuteReader();
                        
                        dr.Close();

                        try
                        {
                            //PEGA IDCLIENTE
                            SqlCommand buscaCliente = new SqlCommand("SELECT idCliente FROM Cliente WHERE nome = @nomeNovoCliente", conn);
                            buscaCliente.Parameters.AddWithValue("@nomeNovoCliente", nomeNovoCliente);
                            SqlDataReader executaBuscaCliente = buscaCliente.ExecuteReader();
                            if (executaBuscaCliente.Read())
                            {
                                idCliente = (int)executaBuscaCliente["idCliente"];
                                

                            }
                            executaBuscaCliente.Close();

                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }


                        try
                        {
                            //CRIAR CONTA DO CLIENTE --- e inserindo o idCliente
                            SqlCommand criaConta = new SqlCommand("INSERT INTO Conta(idCliente, saldo) values(@idCliente, 0)", conn);
                            criaConta.Parameters.AddWithValue("@idCliente", idCliente);

                            SqlDataReader executaCriaConta = criaConta.ExecuteReader();
                            executaCriaConta.Close();

                            

                            try
                            {
                                SqlCommand buscaIdConta = new SqlCommand("SELECT idContaBancaria FROM Conta Where idCliente = @idCliente", conn);
                                buscaIdConta.Parameters.AddWithValue("@idCliente", idCliente);
                                SqlDataReader executaBuscaIdConta = buscaIdConta.ExecuteReader();

                                if (executaBuscaIdConta.Read())
                                {
                                    idContaBancaria = (int)executaBuscaIdConta["idContaBancaria"];
                                    
                                }
                                executaBuscaIdConta.Close();
                            }catch (Exception ex) { Console.WriteLine(ex.Message); }    

                        }catch (Exception ex) { Console.WriteLine(ex.Message); }


                        try
                        {
                            //ATUALIZAR idConta bancaria do cliente 
                            SqlCommand atualizaContaCliente = new SqlCommand("UPDATE Cliente SET IdContaBancaria = @idContaBancaria Where idCliente = @idCliente", conn);
                            atualizaContaCliente.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                            atualizaContaCliente.Parameters.AddWithValue("@idCliente", idCliente);
                            SqlDataReader executaAtualizaContaCliente = atualizaContaCliente.ExecuteReader();
                            executaAtualizaContaCliente.Close();

                           
                        }catch (Exception ex ){ Console.WriteLine(ex.Message); }


                        Console.WriteLine("\nConta do Novo Cliente: " + idContaBancaria + "\n");







                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }




                    /*

                    //CRIAR CONTA DO CLIENTE --- e inserindo o idCliente
                    SqlCommand criaConta = new SqlCommand("INSERT INTO Conta(idCliente, saldo) values(@idCliente, 0)", conn);
                    criaConta.Parameters.AddWithValue("@idCliente", idCliente);

                    SqlDataReader executaCriaConta = criaConta.ExecuteReader();
                    executaCriaConta.Close();
                    */
                    /*
                    //PEGA IdContaCliente
                    SqlCommand buscaIdConta = new SqlCommand("SELECT idContaBancaria FROM Conta Where idCliente = @idCliente", conn);
                    buscaIdConta.Parameters.AddWithValue("@idCliente", idCliente);
                    SqlDataReader executaBuscaIdConta = buscaIdConta.ExecuteReader();

                    if (executaBuscaIdConta.Read())
                    {
                        idContaBancaria = (int)executaBuscaIdConta["idContaBancaria"];
                    }
                    executaBuscaIdConta.Close();
                    */

                    /*
                    //ATUALIZAR idConta bancaria do cliente 
                    SqlCommand atualizaContaCliente = new SqlCommand("UPDATE Cliente SET IdContaBancaria = @idContaBancaria Where idContaCliente = @idContaCliente", conn);
                    atualizaContaCliente.Parameters.AddWithValue("@idContaBancaria", idContaBancaria);
                    atualizaContaCliente.Parameters.AddWithValue("@idCliente", idCliente);
                    SqlDataReader executaAtualizaContaCliente = atualizaContaCliente.ExecuteReader();
                    executaAtualizaContaCliente.Close();
                    */

                    return true;
                }
                else { return false; }


            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
            finally { FecharConexao(); }
        }
    }
}
