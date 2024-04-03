using Handle.Concurrency.Application.Entities;

namespace Handle.Concurrency.Application.Interfaces;

public interface IPostRepository
{
	Task LikePostAsync(Guid postId);

	Task<List<Post>> GetAllPostsAsync();

	Task BeginTransactionAsync();

	Task GetLock(string resourceName, string action);

	Task CommitAsync();

	Task RollbackAsync();
}
