using AutoMapper;
using Event_Management_System.DTO;
using Event_Management_System.Models;
using Event_Management_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers;

[Authorize]
public class PostController(IPostService postService, IImageService imageService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetCurrentUserId();
        var posts = await postService.GetAllAsync(userId);
        return View(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var postEntity = await postService.GetByIdAsync(id);
        if (postEntity == null)
            return NotFound();

        return Ok(postEntity);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var userId = GetCurrentUserId();
        
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
            return View(createPostDto);

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

        await postService.UpdateAsync(postDto);
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
