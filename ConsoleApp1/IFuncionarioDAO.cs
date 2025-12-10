using MySql.Data.MySqlClient;
using ConexaoBancodeDados.Model;
using ConexaoBancodeDados.Interface;
using System;
using System.Collections.Generic;

namespace ConexaoBancodeDados.DAO
{

    public class FuncionarioDAO : IFuncionarioDAO
    {
    
        private const string ConnectionString =
            "server=localhost;user id=root;password=SUA_SENHA_AQUI;database=trabalho_de_banco_de_dados";

       
        public bool Inserir(Funcionario funcionario)
        {
            string query = "INSERT INTO Funcionario (nome, documento) VALUES (@nome, @documento)";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@documento", funcionario.Documento);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"\n❌ Erro ao inserir funcionário: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public List<Funcionario> ConsultarTodos()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            string query = "SELECT id, nome, documento FROM Funcionario";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                             
                                Funcionario f = new Funcionario
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome"),
                                    Documento = reader.GetString("documento")
                                };
                                funcionarios.Add(f);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"\n❌ Erro ao consultar funcionários: {ex.Message}");
                    }
                }
            }
            return funcionarios;
        }


        public bool Atualizar(Funcionario funcionario)
        {
            string query = "UPDATE Funcionario SET nome = @nome, documento = @documento WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@documento", funcionario.Documento);
                    command.Parameters.AddWithValue("@id", funcionario.Id);

                    try
                    {
                        connection.Open();
                        int linhasAfetadas = command.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"\n❌ Erro ao atualizar funcionário: {ex.Message}");
                        return false;
                    }
                }
            }
        }

      
        public bool Deletar(int id)
        {
            string query = "DELETE FROM Funcionario WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int linhasAfetadas = command.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"\n❌ Erro ao deletar funcionário: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}