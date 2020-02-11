

using Microsoft.AspNetCore.Http;

namespace CoreServer.Models
{
    public class FilterModel
    {
        public string Id { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Lang { get; set; }
    }
    public class AddImageView
    {
        public string Id { get; set; }
        public IFormFile File { get; set; }
    }
}
