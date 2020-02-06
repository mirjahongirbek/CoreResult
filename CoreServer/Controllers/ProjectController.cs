using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Enums;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;
using Service.Entity.Models;

using System.Collections.Generic;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IRepositoryCore<Project, string> _project;
        public ProjectController(
            IRepositoryCore<Project, string> project
            )
        {
            _project = project;
        }
        [HttpGet]
        public List<Project> GetallProject()
        {
            return _project.FindAll().ToList();
        }
        [HttpPost]
        public NetResult<Project> AddProject([FromBody] Project model)
        {
            _project.Add(model);
            return model;
        }
        [HttpPut]
        public NetResult<ResponseData> UpdateProject([FromBody] Project model)
        {
            _project.Update(model);
            return StatusCore.Success;
        }
       
    }
}
