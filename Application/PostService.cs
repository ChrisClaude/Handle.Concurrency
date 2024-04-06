using Handle.Concurrency.Application.Entities;
using Handle.Concurrency.Application.Exceptions;
using Handle.Concurrency.Application.Interfaces;

namespace Handle.Concurrency.Application;

public class PostService(IPostRepository repository) : IPostService
{
	private readonly IPostRepository _repository = repository;

	public async Task<List<Post>> GetAllPostsAsync()
	{
		return await _repository.GetAllPostsAsync();
	}

	public async Task<Result> LikePostAsync(Guid postId)
	{
		Result result;
		try
		{
			await _repository.BeginTransactionAsync();
			var resourceName = $"{nameof(Post)}_{postId}";
			await _repository.GetLock(resourceName, nameof(LikePostAsync));
			await _repository.LikePostAsync(postId);
			// Uncomment the following and send 2 or more requests simultaneously to simulate a concurrent request
			// await Task.Delay(15000);
			await _repository.CommitAsync();
			result = new(true, "Successfully liked post");

			return result;
		}
		catch (ConcurrentConflictException ex)
		{
			await _repository.RollbackAsync();
			result = new(false, ex.Message);

			return result;
		}
	}
}
