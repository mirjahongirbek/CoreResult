
using RepositoryCore.Interfaces;
using Service.Entity.Models;

namespace Service.Interfaces
{
    public interface IProjectService : IRepositoryCore<Project, string>
    {
        string CheckProject(Project project, string v);
    }
}
