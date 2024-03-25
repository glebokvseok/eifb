using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Services;

public interface IUserRepository
{
    Task<bool> AddUser(RegistrationModel user);

    Task<bool> CheckUserExistence(string login);

    Task<string?> GetUserPassword(string login);
}