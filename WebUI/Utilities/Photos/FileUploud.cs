﻿using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebUI.Helper
{
    public static class FileUploud
    {
        public static async Task<string> SeveFileAsync(this IFormFile file, string WebRootPath)
        {
            var path = "/uploads/" + Guid.NewGuid() + file.FileName;
            using FileStream fileStream = new(WebRootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return path;
        }
    }
}
