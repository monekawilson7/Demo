using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Demo.BLL.Services;
public class DocumentService : IDocumentService
{
    private List<string> _allowedExtensions = [ ".jpeg", ".png"] ;
    private const int MaxSize = 2_097_152;
    public async Task<string?> UploadAsync(IFormFile file, string folderName)
    {
        var extension = Path.GetExtension(file.FileName);
        if (!_allowedExtensions.Contains(extension))
            return null;
        if (file.Length > MaxSize)
            return null;
        var fileName = $"{Guid.NewGuid()}{extension}";
        var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "Data", folderName);
        var filePath = Path.Combine(FolderPath, fileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return fileName;

    }

    public bool Delete(string fileName, string folderName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", folderName, fileName);
        if (!File.Exists(filePath))
          return false;
        File.Delete(filePath);
        return true;
    }
}
