using MiniFramework.TestApp;
using MiniFramework.Core.Interfaces;
using MiniFramework.Core.Base;
using System.Data;

namespace MyApp.Repositories
{
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Int32 id);
    Task InsertAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(Int32 id);
}
}
