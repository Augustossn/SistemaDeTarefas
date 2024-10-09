using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface ITarefaService
    {
        Task<List<TarefaModel>> GetAll();
        Task<TarefaModel> GetById(int id);
        Task<TarefaModel> Post(TarefaModel tarefa);
        Task<TarefaModel> Put(TarefaModel tarefa, int id);
        Task<bool> Delete(int id);
    }
}
