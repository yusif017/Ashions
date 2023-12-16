using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebUI.Utilities.Photos
{
    public static class FileUplouds
    {
        public static async Task<List<string>> SeveFileAsync(this List<IFormFile> file, string WebRootPath)
        {
            List<string> files = new();

            for (int i = 0; i < file.Count; i++)
            {
                var path = "/uploadss/" + Guid.NewGuid() + file[i].FileName;
                using FileStream fileStream = new(WebRootPath + path, FileMode.Create);
                file[i].CopyTo(fileStream);
                files.Add(path);
            }

            return files;
        }
    }
}
