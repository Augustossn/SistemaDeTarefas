using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UserService : IUserService
    {
        private readonly SistemaDeTarefasDBContext _dbContext;

        public UserService(SistemaDeTarefasDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Post(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Put(UserModel user, int id)
        {
            UserModel userById = await GetById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel userById = await GetById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
