using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repository;

public class ItemRepository : IItemRepository
{
    private readonly IDbConnection _dbConnection;

    public ItemRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ItemEntity?> Get(Guid id)
    {
        string sql = @"SELECT * FROM items
                        WHERE id=@Id";

        return await _dbConnection.QuerySingleOrDefaultAsync<ItemEntity>(sql, new { id });
    }

    public async Task<IEnumerable<ItemEntity>> GetOrderItems(Guid orderId)
    {
        string sql = @"SELECT * FROM items
                        INNER JOIN orders_items ON orders_items.item_id=items.id
                        WHERE orders_items.order_id=@orderId";

        return await _dbConnection.QueryAsync<ItemEntity>(sql, new { orderId });
    }

    public async Task<IEnumerable<ItemEntity>> Get()
    {
        string sql = @"SELECT * FROM items";

        return await _dbConnection.QueryAsync<ItemEntity>(sql);
    }

    public async Task<Guid> Add(ItemEntity item)
    {
        string sql = @"INSERT INTO items
                        (name, price, seller_id)
                        VALUES (@Name, @Price, @SellerId)
                        RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<Guid>(sql, item);
    }
}