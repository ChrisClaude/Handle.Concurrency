using Handle.Concurrency.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handle.Concurrency.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Post> Posts { get; set; }
}