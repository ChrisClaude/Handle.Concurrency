using System.Data;
using Handle.Concurrency.Application.Entities;
using Handle.Concurrency.Application.Exceptions;
using Handle.Concurrency.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Handle.Concurrency.Infrastructure;

public class PostRepository(AppDbContext context) : IPostRepository, IDisposable
{
	private readonly AppDbContext _context = context;
	private IDbContextTransaction _transaction;

	public async Task LikePostAsync(Guid postId)
	{
		var post = await _context.Posts
			.FirstAsync(p => p.Id == postId);
		post.LikesCount += 1;
		await _context.SaveChangesAsync();
	}

	public async Task<List<Post>> GetAllPostsAsync()
	{
		return await _context.Posts.ToListAsync();
	}

	public async Task AcquireLock(string resourceName, string action)
	{
		if (_transaction == null)
		{
			throw new Exception("The transaction is not initialized");
		}
		using var command = _transaction.GetDbTransaction().Connection.CreateCommand();
		command.CommandText = "sp_getapplock";
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.Add(new SqlParameter("Resource", resourceName));
		command.Parameters.Add(new SqlParameter("LockMode", "Exclusive"));
		// The command will wait for a maximum of 10 seconds to acquire the lock
		command.Parameters.Add(new SqlParameter("LockTimeout", "10000"));
		var returnParameter = new SqlParameter("Result", SqlDbType.Int)
		{
			Direction = ParameterDirection.ReturnValue
		};
		command.Parameters.Add(returnParameter);
		// The command will timeout after 15 seconds
		command.CommandTimeout = 15;
		command.Transaction = _transaction.GetDbTransaction();
		await command.ExecuteNonQueryAsync();
		var result = (int)returnParameter.Value;
		if (result != 0)
		{
			throw new ConcurrentConflictException($"A concurrent {action} occurred for resource {resourceName}");
		}
	}

	public async Task BeginTransactionAsync()
	{
		_transaction = await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitAsync()
	{
		await _transaction.CommitAsync();
	}

	public async Task RollbackAsync()
	{
		await _transaction.RollbackAsync();
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_transaction == null)
		{
			return;
		}
		_transaction.Dispose();
	}
}
