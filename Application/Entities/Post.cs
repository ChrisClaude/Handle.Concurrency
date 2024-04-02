namespace Handle.Concurrency.Application.Entities;

public class Post
{
	public Guid Id { get; set; }
	public string Content { get; set; }
	public int LikesCount { get; set; }
}