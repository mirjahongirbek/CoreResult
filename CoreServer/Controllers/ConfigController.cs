using CoreClient.Models;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Enums;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase {

        IRepositoryCore<ProjectConfig, string> _config;
        public ConfigController(IRepositoryCore<ProjectConfig, string> config)
        {
            _config = config;
        }

        [HttpGet]
        public  NetResult<ProjectConfig> Get(string name)
        {
           var result= _config.GetFirst(m => m.ProjectName == name);
            if(result== null)
            {
                result = new ProjectConfig() { ProjectName = name  };
                _config.Add(result);
            }
            return result;
        }
        [HttpPut]
        public NetResult<ResponseData> Update([FromBody]ProjectConfig model)
        {
            _config.Update(model);
            return StatusCore.Success;
            
        }
        [HttpGet]
        public List<ProjectConfig> GetList()
        {
           return _config.FindAll().ToList();
        }




    }
}
