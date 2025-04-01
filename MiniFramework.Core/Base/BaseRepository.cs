using System.Data;
using Dapper;
using MiniFramework.Core.Interfaces;

namespace MiniFramework.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
{
    protected readonly IDbConnection _connection;

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var table = typeof(TEntity).Name + "s";
        return await _connection.QueryAsync<TEntity>($"SELECT * FROM [{table}]");
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        var table = typeof(TEntity).Name + "s";
        return await _connection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM [{table}] WHERE [Id] = @Id", new { Id = id });
    }
}
