using Dapper;
using Domain.Entities;
using Domain.Repositories;
using System.Data;

namespace Infrastructure.Repository;

public class SellerRepository : ISellerRepository
{
    private readonly IDbConnection _dbConnection;

    public SellerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<SellerEntity?> Get(Guid id)
    {
        string sql = @"SELECT * FROM sellers
                        WHERE id=@Id";

        return await _dbConnection.QuerySingleOrDefaultAsync<SellerEntity>(sql, new { id });
    }

    public async Task<IEnumerable<SellerEntity>> Get()
    {
        string sql = @"SELECT * FROM sellers";

        return await _dbConnection.QueryAsync<SellerEntity>(sql);
    }

    public async Task<Guid> Add(SellerEntity seller)
    {
        string sql = @"INSERT INTO sellers
                        (name)
                        VALUES (@Name)
                        RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<Guid>(sql, seller);
    }
}
