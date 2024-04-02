using Handle.Concurrency.Application.Entities;

namespace Handle.Concurrency.Application.Interfaces;

public interface IPostService
{
	Task<List<Post>> GetAllPostsAsync();

	Task<Result> LikePostAsync(Guid postId);
}
