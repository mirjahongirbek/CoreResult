using CoreClient.Models;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Interfaces;
using Service.Entity.Models;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyRestController : ControllerBase
    {
        IRepositoryCore<MyModel, string> _myModel;
        IRepositoryCore<ProjectConfig, string> _config;
        IProjectService _project;
        public MyRestController(
          IRepositoryCore<MyModel, string> myModel,
           IRepositoryCore<ProjectConfig, string> config,
          IProjectService project
          )
        {
            _myModel = myModel;
            _config = config;
            _project = project;
        }
        [HttpPost]
        public NetResult<MyModel> ByTraffic([FromBody]MyModel model)
        {
            return _myModel.GetFirst(m => m.StatusCode == model.StatusCode);

        }
        [HttpGet]
        public NetResult<ProjectConfig> GetConfig(string name)
        {
            var project = _project.GetFirst(m => m.ProjectName == name);
            if (!project.IsCheck(this.AuthMe()))
            {
                return null;
            }
            string modalKey = _project.CheckProject(project, this.GetIp());
            var result = _config.GetFirst(m => m.ProjectName == name && m.MyKey ==modalKey) ;
            if (result == null)
            {
                result = new ProjectConfig() { ProjectName = name };
                _config.Add(result);
            }
            return result;
        }
        public NetResult<List<MyModel>> GetAllResult(string name)
        {
           var project= _project.GetFirst(m => m.ProjectName == name);
            if (!project.IsCheck(this.AuthMe()))
            {
                return null;
            }
            string modalKey=_project.CheckProject(project, this.GetIp());
            var result = _myModel.Find(m => m.ProjectName == name&& m.ModalKey== modalKey).ToList();
            return result;
        }
        

    }
}
