namespace MiniFramework.Core.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
}