using Microsoft.AspNetCore.Http;
using PMMSystem.Application.Services.Interfaces;

namespace PMMSystem.Application.Services
{
  public class FileService() : IFileService
  {
    public Task DeleteFileAsync(string filePath)
    {
      throw new NotImplementedException();
    }

    public string GetFileUrl(string filePath)
    {
      throw new NotImplementedException();
    }

    public async Task<string> SaveFileAsync(IFormFile image, string root, string folder)
    {
      var uniqueFileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
      var filePath = Path.Combine(root,folder, uniqueFileName);
      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await image.CopyToAsync(stream);
      }
      return $"{folder}/{ uniqueFileName}";
    }

  }
}
