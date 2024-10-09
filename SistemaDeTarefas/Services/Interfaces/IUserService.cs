using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> Post(UserModel user);
        Task<UserModel> Put(UserModel user, int id);
        Task<bool> Delete(int id);
    }
}
