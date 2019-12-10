using CoreClient;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CoreResults
{
    public static class CoreState
    {
       public static Rest Rest { get; set; }
        public static string  AddFile(string path, IFormFile file) {

           var filePath= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "myimages", path);
           var filename= RepositoryCore.CoreState.RepositoryState.GenerateRandomString(6) + file.FileName;
            byte[] fileData = null;
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                fileData = reader.ReadBytes((int)file.Length);
                
            }
            filePath = Path.Combine(filePath, filename);
            File.WriteAllBytes(filePath, fileData);
           return filename;
                
        }
       
    }
    
    



}
