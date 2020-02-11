
using LiteDB;
using LiteRepository.Context;
using LiteRepository.Repository;
using Service.Entity.Models;
using Service.Interfaces;

namespace Service.Services
{
  public  class ProjectService: LiteRepository<Project>, IProjectService
    {
        LiteCollection<Project> _db;
        public ProjectService(ILiteContext context):base(context)
        {
           _db= context.Database.GetCollection<Project>();
        }

        public string CheckProject(Project project, string ip)
        {
           var item=project.GetConfig("ip", ip);
            if (item != null) return item.ModalKey;
            return string.Empty;

        }
    }
}
