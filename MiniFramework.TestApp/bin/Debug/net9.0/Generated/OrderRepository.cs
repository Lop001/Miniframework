using MiniFramework.TestApp;
using MiniFramework.Core.Interfaces;
using MiniFramework.Core.Base;
using System.Data;
using Dapper;

namespace MyApp.Repositories
{

	public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		private readonly IDbConnection _connection;

		public OrderRepository(IDbConnection connection) : base(connection)
		{
		}


		public async Task InsertAsync(Order entity) =>
			await _connection.ExecuteAsync("INSERT INTO [Orders] ([CustomerId], [CreatedAt]) VALUES (@CustomerId, @CreatedAt)", entity);

		public async Task UpdateAsync(Order entity) =>
			await _connection.ExecuteAsync("UPDATE [Orders] SET [CustomerId] = @CustomerId, [CreatedAt] = @CreatedAt WHERE [Id] = @Id", entity);

		public async Task DeleteAsync(int id) =>
			await _connection.ExecuteAsync("DELETE FROM [Orders] WHERE [Id] = @Id", new { Id = id });
	}
}
