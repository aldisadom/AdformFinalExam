using Dapper;
using Domain.Entities;
using Domain.Repositories;
using System.Data;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<UserEntity?> Get(int id)
    {
        string sql = @"SELECT * FROM users
                        WHERE id=@Id";

        return await _dbConnection.QuerySingleOrDefaultAsync<UserEntity>(sql, new { id });
    }

    public async Task<IEnumerable<UserEntity>> Get()
    {
        string sql = @"SELECT * FROM users";

        return await _dbConnection.QueryAsync<UserEntity>(sql);
    }

    public async Task<int> Add(UserEntity item)
    {
        string sql = @"INSERT INTO users
                        (id, name, email)
                        VALUES (@Id, @Name, @Email)
                        RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<int>(sql, item);
    }
}
