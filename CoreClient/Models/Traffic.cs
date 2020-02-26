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
        public string ProjectId { get; set; }
        [Required]
        public int StatusCode { get; set; }
        public string ModalKey { get; set; }
        public string Lang { get; set; }
        public StatusCore ResponseStatus { get; set; } = StatusCore.Success;
        [Required]
        public ModelStatus ModelStatus { get; set; }
        public ErrorResult ErrorResult { get; set; }
        public ResponseData Result { get; set; }
        public string FunctionName { get; set; }
        [BsonIgnore]
        public int Offset { get; set; }
        [BsonIgnore]
        public int Limit { get; set; }
        
    }
    
}
