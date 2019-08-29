using LiteDB;
using RepositoryCore.Enums;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;
using RepositoryCore.Result;
using System.ComponentModel.DataAnnotations;

namespace CoreClient.Models
{
    public class MyModel : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public int StatusCode { get; set; }
        [Required]
        public string Lang { get; set; }
        public StatusCore ResponseStatus { get; set; }
        [Required]
        ModelStatus ModelStatus { get; set; }
        public ErrorResult ErrorResult { get; set; }
        public Result Result { get; set; }
        public string FunctionName { get; set; }
        [BsonIgnore]
        public int Offset { get; set; }
        [BsonIgnore]
        public int Limit { get; set; }
    }
}
