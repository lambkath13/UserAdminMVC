using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var postEntity = await _postService.GetPostByIdAsync(id);
        if (postEntity == null)
            return NotFound();

        return Ok(_mapper.Map<PostDto>(postEntity));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var postEntity = _mapper.Map<PostDto>(createPostDto);
        return CreatedAtAction(nameof(GetPostById), new { id = postEntity.PostId }, _mapper.Map<PostDto>(postEntity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto postDto)
    {
        if (!ModelState.IsValid || id != postDto.PostId)
            return BadRequest(ModelState);

        var postEntity = _mapper.Map<PostDto>(postDto);
        await _postService.UpdatePostAsync(postEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var postExist = await _postService.GetPostByIdAsync(id);
        if (postExist == null)
            return NotFound();
        
        await _postService.DeletePostAsync(id);
        return NoContent();
    }
}
