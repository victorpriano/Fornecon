using Dapper;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Repository.DataContext;
using System.Data;

namespace Fornecon.WebAPI.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }
        public async Task<int> LoginUsers(User user)
        {
            var incluir = await _context.Connection.ExecuteAsync("sp_Add_Users",
                new
                {
                    Username = user.Username,
                    Password = user.Password,
                    Role = user.Role
                }, commandType: CommandType.StoredProcedure);

            return incluir;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Connection.QueryAsync<User>("SELECT * FROM Users");

            return users;
        }

        public async Task<IEnumerable<User>> GetUsersExists(User user)
        {
            var exists = await _context.Connection.QueryAsync<User>(
                $"SELECT * FROM Users WHERE Username = '{user.Username}' AND Password = '{user.Password}'");

            return exists;
        }
    }
}
