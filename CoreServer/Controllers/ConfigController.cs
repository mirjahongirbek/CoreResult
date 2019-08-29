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
    public class ConfigController : ControllerBase {

        IRepositoryCore<ProjectConfig, string> _config;
        public ConfigController(IRepositoryCore<ProjectConfig, string> config)
        {
            _config = config;
        }
        [HttpGet]
        public ProjectConfig Get(string name)
        {
           return _config.GetFirst(m => m.ProjectName == name);
        }
        [HttpGet]
        public List<ProjectConfig> GetList()
        {
           return _config.FindAll().ToList();
        }




    }
}
