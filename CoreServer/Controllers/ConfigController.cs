using CoreClient.Models;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Enums;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;
using System;
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
        public  NetResult<List<ProjectConfig>> Get(string name)
        {
           var result= _config.Find(m => m.ProjectName == name).ToList();
            if (result.Count() == 0)
            {
                var first = new ProjectConfig() { ProjectName = name, MyKey = "default" };
                _config.Add(first);
                result.Add(first);
            }
            return result;
        }
        [HttpPost]
        public NetResult<ProjectConfig> AddConfig([FromBody]ProjectConfig model)
        {
            try
            {
               var config= _config.GetFirst(m => m.MyKey == model.MyKey && m.ProjectName == model.ProjectName);
                if(config!= null)
                {

                }
                model.Config = new Dictionary<string, string>();
                model.Config.Add("default", "default");
                _config.Add(model);
                return model;

            }catch(Exception ext)
            {
                return ext;
            }
        }
        [HttpPut]
        public NetResult<ResponseData> Update([FromBody]List<ProjectConfig> model)
        {
            foreach(var i in model)
            {
                _config.Update(i);
            }
            return new ResponseData()
            {
                Result = new { success = true }
            };
            //_config.Update(model);
            //return StatusCore.Success;
            
        }
        [HttpGet]
        public List<ProjectConfig> GetList()
        {
           return _config.FindAll().ToList();
        }




    }
}
