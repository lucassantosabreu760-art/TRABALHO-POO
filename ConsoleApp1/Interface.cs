using ConexaoBancodeDados.Model;
using System.Collections.Generic;

namespace ConexaoBancodeDados.Interface
{
    
    public interface IFuncionarioDAO
    {
        
        bool Inserir(Funcionario funcionario);

       
        List<Funcionario> ConsultarTodos();

        bool Atualizar(Funcionario funcionario);

        
        bool Deletar(int id);
    }
}