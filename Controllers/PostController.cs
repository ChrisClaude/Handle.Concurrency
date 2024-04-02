using Handle.Concurrency.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Handle.Concurrency.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController(IPostService postService) : ControllerBase
{
	private readonly IPostService _postService = postService;

	[HttpGet()]
	public async Task<IActionResult> GetAllPostsAsync()
	{
		var posts = await _postService.GetAllPostsAsync();
		return Ok(posts);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> LikePostAsync(Guid id)
	{
		var result = await _postService.LikePostAsync(id);
		return result.IsSuccess? Ok(result) : BadRequest(result);
	}
}
