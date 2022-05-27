using Fornecon.WebAPI.Models;

namespace Fornecon.WebAPI.Services
{
    public interface ITokenService
    {
        string GerarToken(User user);
    }
}
