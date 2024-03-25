using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace AuthenticationService.Services;

public class UserRepository : IUserRepository
{
    private readonly UserRepositoryConfig _config;
    
    public UserRepository(IOptions<UserRepositoryConfig> config)
    {
        _config = config.Value;
    }
    
    public async Task<bool> AddUser(
        RegistrationModel user,
        CancellationToken cancellationToken) {
        const string request = "INSERT INTO users (login, name, surname) VALUES (@login, @name, @surname);" +
                               "INSERT INTO accounts (login, password) VALUES (@login, @password);";
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        try {
            await connection.ExecuteAsync(request, user);
            await transaction.CommitAsync(cancellationToken);
            return true;
        } catch {
            await transaction.RollbackAsync(cancellationToken);
        }

        return false;
    }

    public async Task<bool> CheckUserExistence(
        string login, 
        CancellationToken cancellationToken) {
        const string request = "SELECT COUNT(1) FROM users WHERE login = @login";
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        return (await connection.QueryAsync<int>(request, new { login })).FirstOrDefault() == 1;
    }

    public async Task<string?> GetUserPassword(
        string login, 
        CancellationToken cancellationToken) {
        const string request = "SELECT password FROM accounts WHERE login = @login;";
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        return (await connection.QueryAsync<string?>(request, new {login})).FirstOrDefault();
    }
}