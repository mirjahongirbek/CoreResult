using CoreClient.Models;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Interfaces;
using Service.Entity.Models;
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
        public MyRestController(
          IRepositoryCore<MyModel, string> myModel,
           IRepositoryCore<ProjectConfig, string> config,
          IRepositoryCore<Project, string> project
          )
        {
            _myModel = myModel;
            _config = config;

        }
        [HttpPost]
        public NetResult<MyModel> ByTraffic([FromBody]MyModel model)
        {
            return _myModel.GetFirst(m => m.StatusCode == model.StatusCode);

        }
        [HttpGet]
        public NetResult<ProjectConfig> GetConfig(string name)
        {
            var result = _config.GetFirst(m => m.ProjectName == name);
            if (result == null)
            {
                result = new ProjectConfig() { ProjectName = name };
                _config.Add(result);
            }
            return result;
        }
        public NetResult<List<MyModel>> GetAllResult(string name)
        {
           var result= _myModel.Find(m => m.ProjectName == name).ToList();
            return result;
        }

    }
}
