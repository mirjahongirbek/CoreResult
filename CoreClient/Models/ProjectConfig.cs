using LiteDB;
using RepositoryCore.Interfaces;
using System.Collections.Generic;

namespace CoreClient.Models
{
    public class ProjectConfig :IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public Dictionary<string,object> Config { get; set; }
    }
}
