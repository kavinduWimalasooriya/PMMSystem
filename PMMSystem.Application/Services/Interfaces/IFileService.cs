using Microsoft.AspNetCore.Http;

namespace PMMSystem.Application.Services.Interfaces
{
  public interface IFileService
  {
    Task<string> SaveFileAsync(IFormFile image, string root,string folder);
    Task DeleteFileAsync(string filePath);
    string GetFileUrl(string filePath);
  }
}
