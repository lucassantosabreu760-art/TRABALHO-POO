using ConexaoBancodeDados.Model;
using ConexaoBancodeDados.DAO;
using ConexaoBancodeDados.Interface; 
using System;
using System.Collections.Generic;

namespace ConexaoBancodeDados
{
    public class Program
    {
        
        private static IFuncionarioDAO _dao = new FuncionarioDAO();

        public static void Main(string[] args)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine(" SISTEMA CRUD FUNCIONÁRIOS (ADO.NET - MySQL)");
            Console.WriteLine("====================================================");

            bool rodando = true;
            while (rodando)
            {
                ExibirMenu();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CadastrarFuncionario(); break;
                    case "2": ListarFuncionarios(); break;
                    case "3": AtualizarFuncionario(); break;
                    case "4": ExcluirFuncionario(); break;
                    case "5": rodando = false; break;
                    default: Console.WriteLine("\nOpção inválida. Tente novamente."); break;
                }
                if (rodando)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.WriteLine("\nPrograma encerrado.");
        }

        private static void ExibirMenu()
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Cadastrar Novo Funcionário (CREATE)");
            Console.WriteLine("2. Listar Todos os Funcionários (READ)");
            Console.WriteLine("3. Atualizar Funcionário (UPDATE)");
            Console.WriteLine("4. Excluir Funcionário (DELETE)");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
        }

        private static void CadastrarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("\n--- CADASTRO DE NOVO FUNCIONÁRIO ---");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Documento (Único): ");
            string documento = Console.ReadLine();

            Funcionario novoFunc = new Funcionario { Nome = nome, Documento = documento };
            if (_dao.Inserir(novoFunc)) 
            {
                Console.WriteLine($"\n✅ Funcionário {nome} cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("\n❌ Falha ao cadastrar. Documento pode já existir ou houve um erro de conexão.");
            }
        }

        private static void ListarFuncionarios()
        {
            Console.Clear();
            Console.WriteLine("\n--- LISTA DE FUNCIONÁRIOS ---");
            List<Funcionario> lista = _dao.ConsultarTodos(); 

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum funcionário cadastrado.");
                return;
            }

            foreach (var f in lista)
            {
                Console.WriteLine($"ID: {f.Id} | Nome: {f.Nome} | Documento: {f.Documento}");
            }
        }

        private static void AtualizarFuncionario()
        {
            ListarFuncionarios();
            Console.Write("\nDigite o ID do funcionário para atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.Write("Novo Nome: ");
            string novoNome = Console.ReadLine();
            Console.Write("Novo Documento: ");
            string novoDoc = Console.ReadLine();

            Funcionario funcAtualizado = new Funcionario { Id = id, Nome = novoNome, Documento = novoDoc };
            if (_dao.Atualizar(funcAtualizado)) 
            {
                Console.WriteLine($"\n✅ Funcionário ID {id} atualizado com sucesso.");
            }
            else
            {
                Console.WriteLine("\n❌ Falha ao atualizar. Verifique o ID e o novo Documento.");
            }
        }

        private static void ExcluirFuncionario()
        {
            ListarFuncionarios();
            Console.Write("\nDigite o ID do funcionário para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.Write($"Tem certeza que deseja excluir o ID {id}? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                if (_dao.Deletar(id)) 
                {
                    Console.WriteLine($"\n✅ Funcionário ID {id} excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("\n❌ Falha ao excluir. Verifique o ID ou se há registros dependentes.");
                }
            }
        }
    }
}