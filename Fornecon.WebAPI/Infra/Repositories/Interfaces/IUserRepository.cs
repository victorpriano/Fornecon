using Fornecon.WebAPI.Models;

namespace Fornecon.WebAPI.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsersExists(User user);
        Task<int> LoginUsers(User user);
    }
}
