using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Event_Management_System.Service;
using System.Drawing;
using System.Drawing.Imaging;


public class ImageService : IImageService
{
    public async Task<string> AddFileAsync(string dir, IFormFile file)
    {
        dir = dir.ToLower();
    
        var rootDirectory = Path.GetFullPath("wwwroot/image/");
        var fileDirectory = Path.Combine(rootDirectory, dir);

        if (!Directory.Exists(fileDirectory))
            Directory.CreateDirectory(fileDirectory);

        var cleanFileName = string.Concat(DateTime.Now.Ticks, file.FileName.Replace(" ", "_"));
        var filePath = Path.Combine(fileDirectory, cleanFileName);

        using var stream = file.OpenReadStream();
        using var image = await Image.LoadAsync(stream);

        image.Mutate(x => x.Resize(400, 300));

        await image.SaveAsync(filePath, new PngEncoder()); 

        return Path.Combine(dir, cleanFileName);
    }


    private static void DeleteFile(string filePath, string dir)
    {
        if (String.IsNullOrEmpty(filePath))
            return;
        dir = dir.ToLower();
        var rootDirectory =  Path.GetFullPath("wwwroot/image/");
        var fileDirectory = Path.Combine(rootDirectory, dir);
        if(File.Exists(fileDirectory))
            File.Delete(fileDirectory);
        if(Directory.GetFiles(fileDirectory!).Length == 0)
            Directory.Delete(fileDirectory);
    }

    public async Task<string> UpdateFileAsync(string oldFilePath, string dir, IFormFile file)
    {
        DeleteFile(oldFilePath,dir);
        return await AddFileAsync(dir, file);
    }
    
    
}