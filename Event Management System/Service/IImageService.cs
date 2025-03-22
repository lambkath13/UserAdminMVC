namespace Event_Management_System.Service;

public interface IImageService
{
    Task<string> AddFileAsync(string dir, IFormFile file);
    Task<string> UpdateFileAsync(string oldFilePath, string dir, IFormFile file);
}