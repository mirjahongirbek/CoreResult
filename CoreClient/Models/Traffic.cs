using RepositoryCore.Enums;
using RepositoryCore.Models;
using RepositoryCore.Result;

namespace CoreClient.Models
{
    public class Traffic 
    {
        public string ProjectName { get; set; }
        public int StatusCode { get; set; }
        public string Lang { get; set; }
        public StatusCore ResponseStatus { get; set; }
        public ErrorResult ErrorResult { get; set; }
        public Result Result { get; set; }
        public string FunctionName { get; set; }

    }
}
