using ProjetoBancoConsole.Classes;
using System;

namespace ProjetoBancoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string rsp = "";

            do
            {
                Console.WriteLine("\n========= M E N U  P R I N C I P A L =========");
                Console.WriteLine("\n   1 - Acessar Conta");
                Console.WriteLine("   2 - Cadastrar Cliente");
                Console.WriteLine("   3 - SAIR ");
                Console.Write("                  SELECIONE : ");
                rsp = Console.ReadLine();

                Cliente cliente = new Cliente();
                Conta conta = new Conta();
                OperacoesBancarias operacoesBancarias = new OperacoesBancarias();

                switch (rsp)
                {
                    case "1":
                        {
                            Console.WriteLine("\n================ ACESSAR CONTA ===============\n");
                            Console.Write("     Informe a Conta: ");
                            int contaBancaria = Convert.ToInt32(Console.ReadLine());

                            //contaBancaria

                            try
                            {
                                if (cliente.ClienteExiste(contaBancaria))
                                {
                                    string opcao;

                                    do
                                    {
                                        Console.WriteLine("\n**************** C L I E N T E ***************");

                                        Console.WriteLine(cliente.InformacoesContaCliente(contaBancaria));
                                        Console.WriteLine("     1 - Depositar");
                                        Console.WriteLine("     2 - Retirada");
                                        Console.WriteLine("     3 - Consultar Saldo");
                                        Console.WriteLine("     4 - Transferencia");
                                        Console.WriteLine("     5 - Solicitar Extrato");
                                        Console.WriteLine("     6 - Sair");
                                        Console.Write    ("                  SELECIONE : ");
                                        opcao = Console.ReadLine();

                                        Console.WriteLine("\n");

                                        switch (opcao)
                                        {
                                            case "1": //Depositar
                                                {
                                                    Console.WriteLine("-------------- DEPÓSITO --------------\n");
                                                    Console.Write("          Informe o Valor:  R$ ");
                                                    float valorDeposito = float.Parse(Console.ReadLine());

                                                    if (operacoesBancarias.Depositar(contaBancaria, valorDeposito, opcao))
                                                    {
                                                        Console.WriteLine("          Depósito Realizado com Sucesso");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("!!!!! Erro ao Realizar Deposito !!!!!");
                                                        if(valorDeposito <= 0) { Console.WriteLine("          Valor Invalido"); }
                                                    }
                                                    break;
                                                }
                                            case "2": //Retirada
                                                {
                                                    Console.WriteLine("-------------- RETIRADA --------------\n");
                                                   

                                                    Console.Write("          Informe o Valor: R$ ");
                                                    float valorRetirada = float.Parse(Console.ReadLine());

                                                    if (operacoesBancarias.Retirada(contaBancaria, valorRetirada, opcao))
                                                    {
                                                       
                                                        Console.WriteLine("          Retirada Realizada com Sucesso");
                                                        
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("!!!!! Erro ao Realizar a Retirada !!!!!");
                                                    }
                                                    break;
                                                }
                                            case "3": //Consultar Saldo
                                                {
                                                    Console.WriteLine("----------- COSULTA  SALDO -----------\n");
                                                    if (operacoesBancarias.ConsultarSaldo(contaBancaria) > 0)
                                                    {
                                                        Console.WriteLine("          Saldo de : R$ " + operacoesBancarias.ConsultarSaldo(contaBancaria));
                                                    }
                                                    else 
                                                    {
                                                      
                                                        Console.WriteLine("          Saldo Indisponivel!"); 
                                                    }
                                                    break;
                                                }
                                            case "4": //Transferencia
                                                {
                                                    Console.WriteLine("------------ TRANSFERENCIA -----------\n");
                                                    Console.Write("          Informe o Valor: R$ ");
                                                    float valorTransferencia = float.Parse(Console.ReadLine());

                                                    Console.Write("          Informe a Conta de Destino: ");
                                                    int idContaRecebe = int.Parse(Console.ReadLine());

                                                    if (operacoesBancarias.Transferencia(contaBancaria, valorTransferencia, idContaRecebe, opcao))
                                                    {
                                                        Console.WriteLine("          Transferencia Realizada com Sucesso");
                                                    }
                                                    else {
                                                        Console.WriteLine("          Falha na Transferencia"); 
                                                    }

                                                    break;
                                                }
                                            case "5":
                                                {
                                                    foreach (var item in operacoesBancarias.Extrato(contaBancaria))
                                                    {
                                                        Console.WriteLine($"\n" +
                                                            $"          Descrição: {item.descricao}" +
                                                            $"\n          Valor: R$ {item.valorMovimentacao}" +
                                                            $"\n          Saldo Final: R$ {item.valorFinal}" +
                                                            $"\n¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨");
                                                    }
                                                    break;
                                                }

                                             default: {
                                                    Console.WriteLine("\n!!!!!!!!!!!! FUNÇÃO NÃO ENCONTRADA !!!!!!!!!!!!\n"); 
                                                    break; }
                                        }



                                    } while (opcao != "6");
                                }
                                else {
                                    Console.WriteLine("     CONTA NÃO ENCONTRADA!!"); 
                                }


                            }
                            catch (Exception e)
                            { Console.WriteLine("Ocorreu um problema: " + e.Message); }
                            break;
                        }


                    case "2":
                        {
                            Console.WriteLine("\n****** C A D A S T R A R   C L I E N T E *****\n");

                            Console.Write("     Informe o nome: ");
                            string nomeNovoCliente = Console.ReadLine();

                            Console.Write("     Informe a Idade: ");
                            int idadeNovoCliente = int.Parse(Console.ReadLine());

                            if (conta.CadastrarNovoCliente(nomeNovoCliente, idadeNovoCliente))
                            {
                                Console.WriteLine("     Cadastro Concluido!");
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("     Cadastro Não Realizado");
                                Console.ResetColor();
                                if (cliente.ClientePossuiCadastro(nomeNovoCliente))
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("     Erro: Cliente Já Possui Cadastro!!");
                                    Console.ResetColor();
                                }
                            }



                            break;
                        }


                    default:
                        Console.WriteLine("\n!!!!!!!!!!!! FUNÇÃO NÃO ENCONTRADA !!!!!!!!!!!!\n");
                        break;

                }





            } while (rsp != "3");
        }

    }
}
