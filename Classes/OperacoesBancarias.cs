using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoBancoConsole.Classes
{
    internal class OperacoesBancarias : Conta
    {
        public bool Depositar(int numeroContaBancaria, float valorDeposito, string opcao)
        {
            try
            {
                CriarConexao();
                SqlCommand comando = new SqlCommand("SELECT saldo FROM Conta WHERE idContaBancaria = @numeroContaBancaria", conn);
                comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.saldo = (float)Convert.ToDecimal(dr["saldo"]);
                    if (c.saldo >= 0 && valorDeposito > 0)
                    {
                        dr.Close();

                        if (valorDeposito > 0)
                        {
                            c.saldo += valorDeposito;
                            float novoSaldo = (float)c.saldo;

                            comando = new SqlCommand("UPDATE Conta SET saldo = @novoSaldo WHERE idContaBancaria = @numeroContaBancaria", conn);
                            comando.Parameters.AddWithValue("@novoSaldo", novoSaldo);
                            comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                            comando.ExecuteNonQuery(); //executar

                            if(opcao == "1")
                            {
                                Historico historico = new Historico();
                                historico.Depositar(numeroContaBancaria, valorDeposito, novoSaldo);
                            }

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else { return false; }
                }
                else { Console.WriteLine("Saldo Não encontrado"); return false; }
            }
            catch (Exception e) { throw new Exception("Erro ao realizar depósito:     " + e.Message); }
            finally { FecharConexao(); }

        }

        public bool Retirada(int numeroContaBancaria, float valorRetirada, string opcao)
        {
            try
            {
                CriarConexao();

                SqlCommand comando = new SqlCommand("SELECT saldo FROM Conta WHERE idContaBancaria = @numeroContaBancaria", conn);
                comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.saldo = (float)Convert.ToDecimal(dr["saldo"]);
                    if (c.saldo >= 0)
                    {
                        dr.Close();
                        float validaResultado = (float)(c.saldo - valorRetirada);

                        if (valorRetirada > 0 && validaResultado >= 0)
                        {
                            c.saldo -= valorRetirada;
                            float novoSaldo = (float)c.saldo;

                            comando = new SqlCommand("UPDATE Conta SET saldo = @novoSaldo WHERE IdContaBancaria =  @numeroContaBancaria", conn);
                            comando.Parameters.AddWithValue("@novoSaldo", novoSaldo);
                            comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                            comando.ExecuteNonQuery(); //executar

                            if(opcao == "2")
                            {
                                Historico historico = new Historico();
                                historico.Retirada(numeroContaBancaria, valorRetirada, novoSaldo);
                            }

                            return true;

                        }
                        else { return false; }
                    }
                    else { return false; }
                }
                else { return false; }
            }
            catch (Exception e) { throw new Exception("Erro ao Sacar : " + e.Message); }
            finally { FecharConexao(); }
        }

        public float ConsultarSaldo(int numeroContaBancaria)
        {

            try
            {
                CriarConexao();
                SqlCommand comando = new SqlCommand("SELECT saldo FROM Conta WHERE idContaBancaria = @numeroContaBancaria", conn);
                comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.saldo = (float)Convert.ToDecimal(dr["saldo"]);
                    if (c.saldo >= 0)
                    {
                        return (float)c.saldo;
                    }
                    else { return 0; }

                }
                else { return 0; }
                dr.Close();
            }
            catch (Exception e) { throw new Exception("Erro ao Consultar saldo" + e.Message); return 0; }
            finally { FecharConexao(); }

        }

        public bool Transferencia(int numeroContaBancaria, float valorTransferencia, int numeroContaBancariaRecebe, string opcao)
        {
            try
            {
                CriarConexao();

                Historico historico = new Historico();
                Conta conta = new Conta();
                Cliente cliente = new Cliente();

                if (valorTransferencia > 0 && 
                    Retirada(numeroContaBancaria, valorTransferencia, opcao) && 
                    Depositar(numeroContaBancariaRecebe, valorTransferencia, opcao))
                {
                    try
                    {
                        historico.Transfere(numeroContaBancaria, valorTransferencia, ConsultarSaldo(numeroContaBancaria));
                        historico.Recebe(numeroContaBancariaRecebe, valorTransferencia, ConsultarSaldo(numeroContaBancaria));

                    }catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                   

                    return true;
                }
                return false;

            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
            finally { FecharConexao(); }

        }

        public List<Historico> Extrato(int numeroContaBancaria)
        {
            List<Historico> lista = new List<Historico>();
            try
            {
                CriarConexao();
                SqlCommand comando = new SqlCommand("SELECT * FROM Historico WHERE idContaBancaria = @numeroContaBancaria" , conn);
                comando.Parameters.AddWithValue("@numeroContaBancaria", numeroContaBancaria);
                SqlDataReader dr = comando.ExecuteReader();
                

                while (dr.Read())
                {
                    Historico historico = new Historico();
                    historico.idContaBancaria = (int)dr["idContaBancaria"];
                    historico.descricao = dr["descricao"].ToString();
                    historico.valorMovimentacao = (float)Convert.ToDecimal(dr["valorMovimentacao"]);
                    historico.valorFinal = (float)Convert.ToDecimal(dr["valorFinal"]);
                    
                    lista.Add(historico);   
                }

                if (!dr.Read())
                {
                    Console.WriteLine("Cliente não possui movimentação bancária");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { FecharConexao(); }

            return lista;

        }

    }
}
