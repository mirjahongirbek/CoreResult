using LiteDB;
using RepositoryCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Service.Entity.Models
{
    public class Project : IEntity<string>
    {
        [BsonId]
        public string Id { get; set; }
        [BsonIndex]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsCheckUser { get; set; } = false;
        public List<InProjectConfig> ProjectConfig { get; set; } = new List<InProjectConfig>();
        public void AddProject(InProjectConfig inProject)
        {
           var isExist= ProjectConfig.FirstOrDefault(m => m.Value == inProject.Value && m.Key == inProject.Key && m.ModalKey == inProject.ModalKey);
            if(isExist!= null)
            {
                return;
            }
            ProjectConfig.Add(inProject);
        }
        public InProjectConfig GetConfig(string key)
        {
           return ProjectConfig.FirstOrDefault(m => m.Key == key);
        }
        public InProjectConfig GetConfig(string key, string value)
        {
           return ProjectConfig.FirstOrDefault(m => m.Key == key && m.Value.ToString() == value);
        }
        public bool IsCheck(KeyValuePair<string, string>? pro)
        {
            if (!IsCheckUser) return true;
            if (!pro.HasValue) return false;
            if (pro.GetValueOrDefault().Key == UserName && pro.GetValueOrDefault().Value == Password) return true;
            return false;
        }

    }
    //TODO true name
    public class InProjectConfig
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string ModalKey { get; set; }
    }
    

    
}
