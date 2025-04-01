using MiniFramework.TestApp;
using MiniFramework.Core.Interfaces;
using MiniFramework.Core.Base;
using System.Data;

namespace MyApp.Repositories
{
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Int32 id);
    Task InsertAsync(Customer entity);
    Task UpdateAsync(Customer entity);
    Task DeleteAsync(Int32 id);
}
}
