using Handle.Concurrency.Application;
using Handle.Concurrency.Application.Interfaces;
using Handle.Concurrency.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Handle.Concurrency;

public static class Extensions
{
	public static void AddApplicationServices(this WebApplicationBuilder builder)
	{
		var services = builder.Services;
		services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("MediaSandboxConnection")));

		services.AddScoped<IPostRepository, PostRepository>();
		services.AddScoped<IPostService, PostService>();
	}
}
