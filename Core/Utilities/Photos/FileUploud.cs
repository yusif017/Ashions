using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebUI.Helper
{
    public static class FileUploud
    {
        public static async Task<List<string>> SeveFileAsync(this List<IFormFile> file, string WebRootPath)
        {
            List<string> files = new();

            for (int i = 0; i < file.Count; i++)
            {
                var path = "/uploads/" + Guid.NewGuid() + file[i].FileName;
                using FileStream fileStream = new(WebRootPath + path, FileMode.Create);
                file[i].CopyToAsync(fileStream);
                files.Add(path);
            }

            return files;
        }
    }
}
