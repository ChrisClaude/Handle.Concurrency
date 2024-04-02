using Handle.Concurrency.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handle.Concurrency.Infrastructure;

public class SeedData
{
	public static void SeedPosts(WebApplication app)
	{
		using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var context = scope.ServiceProvider.GetService<AppDbContext>();
		context.Database.Migrate();

		if (context.Posts.Any())
		{
			return;
		}

		var posts = new List<Post>()
		{
			new()
			{
				Content = "This is new article on building microservices with .NET",
				LikesCount = 15
			},
			new()
			{
				Content = "Implementing the CQRS pattern in your web API",
				LikesCount = 20
			},
			new()
			{
				Content = "React state management with Redux Toolkit",
				LikesCount = 12
			}
		};

		context.Posts.AddRange(posts);
		context.SaveChanges();
	}
}
