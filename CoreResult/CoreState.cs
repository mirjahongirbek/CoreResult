using CoreClient;
using CoreResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CoreResults
{
    public static class CoreState
    {
        public static Rest Rest { get; set; }
        public static string AddFile(string path, IFormFile file)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "myimages", path);
            var filename = RepositoryCore.CoreState.RepositoryState.GenerateRandomString(6) + file.FileName;
            byte[] fileData = null;
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                fileData = reader.ReadBytes((int)file.Length);

            }
            filePath = Path.Combine(filePath, filename);
            File.WriteAllBytes(filePath, fileData);
            return filename;

        }
        public static void AddContext(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
        public static void ContextMiddleware(IApplicationBuilder app)
        {
           var accses= app.ApplicationServices.GetService<IHttpContextAccessor>();
            if (app.ApplicationServices.GetService<IHttpContextAccessor>() != null)
                HttpContextHelper.Accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        }
    }





}
