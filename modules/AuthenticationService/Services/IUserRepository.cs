using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface IUserRepository
{
    Task<bool> AddUser(RegistrationModel user, CancellationToken cancellationToken);

    Task<bool> CheckUserExistence(string login, CancellationToken cancellationToken);

    Task<string?> GetUserPassword(string login, CancellationToken cancellationToken);
}