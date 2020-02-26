using CoreClient.Models;
using CoreResults;
using CoreServer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Enums;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;
using Service.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IRepositoryCore<Project, string> _project;
        IRepositoryCore<ProjectConfig, string> _config;
        public ProjectController(
            IRepositoryCore<Project, string> project,
            IRepositoryCore<ProjectConfig, string> config
            )
        {
            _project = project;
            _config = config;
        }
        [HttpGet]
        public List<Project> GetallProject()
        {
            return _project.FindAll().ToList();
        }
        [HttpPost]
        public NetResult<Project> AddProject([FromBody] Project model)
        {
            model.AddProject(new InProjectConfig() { Key = "default", Value = "default", ModalKey = "default" });
            _project.Add(model);
            ProjectConfig config = new ProjectConfig() { MyKey = "default", ProjectName = model.ProjectName, };
            config.Config = new Dictionary<string, string>();
            config.Config.Add("default", "default");
            _config.Add(config);
            return model;
        }
        [HttpPut]
        public NetResult<ResponseData> UpdateProject([FromBody] Project model)
        {
            _project.Update(model);
            return this.GetResponse(model);
        }
        [HttpPost]
        public NetResult<ResponseData> AddImage([FromForm]AddImageView model)
        {
            try
            {
                if (model == null|| string.IsNullOrEmpty(model.Id)) return null;
               var project= _project.Get(model.Id);
               var file=CoreState.AddwwwRoot("projects", model.File);
                project.ImageUrl = file;
                _project.Update(project);
                return new ResponseData()
                {
                    Result = new { url = project.ImageUrl }
                };

            }
            catch(Exception ext)
            {
                return ext;
            }
        }
        [HttpGet]
        public NetResult<ResponseData> Test()
        {
           return StatusCore.Success;
         }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController
    {
        public TestController()
        {

        }
        public NetResult<ResponseData> SuccessTest()
        {
           return StatusCore.Success;
        }
    
    }
}
