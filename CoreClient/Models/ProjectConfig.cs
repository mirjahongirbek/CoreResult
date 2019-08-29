using LiteDB;
using RepositoryCore.Interfaces;

namespace CoreClient.Models
{
    public class ProjectConfig :IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public dynamic Config { get; set; }
    }
}
