using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaService : ITarefaService
    {
        private readonly SistemaDeTarefasDBContext _dbContext;

        public TarefaService(SistemaDeTarefasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TarefaModel>> GetAll()
        {
            return await _dbContext.Tarefas
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TarefaModel> GetById(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TarefaModel> Post(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> Put(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaById = await GetById(id);

            if (tarefaById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }

            tarefaById.Name = tarefa.Name;
            tarefaById.Description = tarefa.Description;
            tarefaById.Status = tarefa.Status;
            tarefaById.UserId = tarefa.UserId;

            _dbContext.Tarefas.Update(tarefaById);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<bool> Delete(int id)
        {
            TarefaModel tarefaById = await GetById(id);

            if (tarefaById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Tarefas.Remove(tarefaById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
