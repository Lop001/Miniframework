using MiniFramework.TestApp;
using MiniFramework.Core.Interfaces;
using MiniFramework.Core.Base;
using System.Data;
using Dapper;

namespace MyApp.Repositories
{

	public partial class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		private readonly IDbConnection _connection;

		public CustomerRepository(IDbConnection connection) : base(connection)
		{
		}


		public async Task InsertAsync(Customer entity) =>
			await _connection.ExecuteAsync("INSERT INTO [Customers] ([FirstName], [LastName], [Email], [RegisteredAt]) VALUES (@FirstName, @LastName, @Email, @RegisteredAt)", entity);

		public async Task UpdateAsync(Customer entity) =>
			await _connection.ExecuteAsync("UPDATE [Customers] SET [FirstName] = @FirstName, [LastName] = @LastName, [Email] = @Email, [RegisteredAt] = @RegisteredAt WHERE [Id] = @Id", entity);

		public async Task DeleteAsync(int id) =>
			await _connection.ExecuteAsync("DELETE FROM [Customers] WHERE [Id] = @Id", new { Id = id });
	}
}
