using Dapper;
using Domain.Entities;
using Domain.Repositories;
using System.Data;

namespace Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _dbConnection;

    public OrderRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<OrderEntity?> Get(Guid id)
    {
        string sql = @"SELECT * FROM orders
                        WHERE id=@Id";

        return await _dbConnection.QuerySingleOrDefaultAsync<OrderEntity>(sql, new { id });
    }

    public async Task<IEnumerable<OrderEntity>> Get()
    {
        string sql = @"SELECT * FROM orders";

        return await _dbConnection.QueryAsync<OrderEntity>(sql);
    }

    public async Task<Guid> Add(OrderEntity order)
    {
        string sql = @"INSERT INTO orders
                        (name, price, seller_id)
                        VALUES (@Name, @Price, @SellerId)
                        RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<Guid>(sql, order);
    }

    public async Task Update(OrderEntity order)
    {
        string sql = @"UPDATE orders
                        SET name=@Name, price=@Price, seller_id=@SellerId
                        WHERE id=@Id";

        await _dbConnection.ExecuteAsync(sql, order);
    }

    public async Task Delete(Guid id)
    {
        string sql = @"DELETE FROM orders
                        WHERE id=@Id";

        await _dbConnection.ExecuteAsync(sql, new { id });
    }
}
