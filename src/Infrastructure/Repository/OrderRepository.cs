using Dapper;
using Domain.Entities;
using Domain.Repositories;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;

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

    public async Task<OrderEntity?> GetUnfinishedOrder(int userId)
    {
        string sql = @"SELECT * FROM orders
                        WHERE (user_id=@userId AND status='Ordering')";

        return await _dbConnection.QuerySingleOrDefaultAsync<OrderEntity>(sql, new { userId });
    }

    public async Task<IEnumerable<OrderEntity>> GetUserOrders(int userId)
    {
        string sql = @"SELECT * FROM orders
                        WHERE user_id = @userId";

        return await _dbConnection.QueryAsync<OrderEntity>(sql, new { userId });
    }

    public async Task<IEnumerable<OrderEntity>> Get()
    {
        string sql = @"SELECT * FROM orders";

        return await _dbConnection.QueryAsync<OrderEntity>(sql);
    }

    public async Task<Guid> Add(OrderEntity order)
    {
        string sql = @"INSERT INTO orders
                        (status, user_id, seller_id)
                        VALUES (@Status, @UserId, @SellerId)
                        RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<Guid>(sql, order);
    }

    public async Task AddItemToOrder(OrderItemEntity order)
    {
        string sql = @"INSERT INTO orders_items
                        (order_id, item_id)
                        VALUES (@OrderId, @ItemId)";

        await _dbConnection.ExecuteScalarAsync<Guid>(sql, order);
    }

    public async Task<OrderEntity> Update(OrderEntity order)
    {
        string sql = @"UPDATE orders
                        SET status=@status
                        WHERE id=@Id
                        RETURNING status";

        return (await _dbConnection.QuerySingleOrDefaultAsync<OrderEntity>(sql, order))!;
    }

    public async Task<int> DeleteOrdersByDate(DateTime date)
    {
        string sql = @"DELETE FROM orders
                        WHERE (create_date<@date AND status='Ordering')";

        return await _dbConnection.ExecuteAsync(sql, new { date });
    }
}
