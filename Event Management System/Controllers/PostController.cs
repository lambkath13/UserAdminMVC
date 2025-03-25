using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;
[Authorize]
public class PostController(IPostService postService, IMapper mapper, IImageService imageService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await postService.GetAllAsync();
        return View(mapper.Map<List<PostDto>>(posts));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var postEntity = await postService.GetByIdAsync(id);
        if (postEntity == null)
            return NotFound();

        return Ok(mapper.Map<PostDto>(postEntity));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized();
        }
        
        var postDto = new CreatePostDto()
        {
            UserId = userId,

        };
        return View(postDto);
    }
   
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await postService.AddAsync(new PostDto()
        {
            UserId = createPostDto.UserId,
            Content = createPostDto.Content,
            CreatedAt = DateTime.Now,
            ImageUrl = await imageService.AddFileAsync(nameof(Post), createPostDto.Image)
        });
        
        return Redirect("/post/getAll");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PostDto postDto)
    {
        if (!ModelState.IsValid || id != postDto.PostId)
            return BadRequest(ModelState);

        var postEntity = mapper.Map<PostDto>(postDto);
        await postService.UpdateAsync(postEntity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var postExist = await postService.GetByIdAsync(id);
        if (postExist == null)
            return NotFound();
        
        await postService.DeleteAsync(id);
        return NoContent();
    }
}
