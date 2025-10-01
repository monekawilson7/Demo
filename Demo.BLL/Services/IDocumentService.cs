using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services;
public interface IDocumentService
{
    //upload 
    //delete
    Task<string?> UploadAsync(IFormFile file, string folderName);
    bool Delete(string fileName, string folderName);
}
