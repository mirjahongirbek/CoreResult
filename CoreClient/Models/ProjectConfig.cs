using LiteDB;
using RepositoryCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoreClient.Models
{
    public class ProjectConfig :IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public Dictionary<string,string> Config { get; set; }
        public string ImageUrl { get; set; }

        public string ToString(string key)
        {
            if (Config == null) return "";
           var str= Config.FirstOrDefault(m => m.Key.ToLower() == key.ToLower());
            if (string.IsNullOrEmpty(str.Key))
            {
                return "";
            }
            return str.Value;
            
        }
        
    }
}
