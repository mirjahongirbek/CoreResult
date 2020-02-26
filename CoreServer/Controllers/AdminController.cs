
using CoreClient.Models;
using CoreResults;
using CoreServer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Interfaces;
using RepositoryCore.Models;
using Service.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        IRepositoryCore<MyModel, string> _myModel;
        IRepositoryCore<Project, string> _project;
        IRepositoryCore<ProjectConfig, string> _config;
        public AdminController(
            IRepositoryCore<MyModel, string> myModel,
        IRepositoryCore<Project, string> project,
        IRepositoryCore<ProjectConfig, string> config
            )
        {
            _myModel = myModel;
            _project = project;
            _config = config;
        }
        
        [HttpPost]
        public ResponseData MonitorBy([FromBody] FilterModel model)
        {
            try
            {
               var project= _project.GetFirst(m => m.Id == model.Id);
                if(project== null)
                {

                }
                List<MyModel> result = null;
                if (string.IsNullOrEmpty(model.Lang))
                {
                   result= _myModel.FindReverse(m => m.ProjectName == project.ProjectName).Skip(model.Offset).Take(model.Limit).ToList();
                }
                else
                {
                    result= _myModel.FindReverse(m => m.ProjectName == project.ProjectName&& m.Lang== model.Lang).Skip(model.Offset).Take(model.Limit).ToList();
                }               
                var item= this.GetResponse(result);
                return item;

            }catch(Exception ext)
            {
                Console.WriteLine(ext.Message);
                return null;
            }
        }
       public ResponseData Dashboard()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("project", _project.Count());
            result.Add("config", _config.Count());
            result.Add("myModel", _myModel.Count());
            result.Add("errorModels", _myModel.Count(m => m.ErrorResult != null));
            result.Add("modelResult", _myModel.Count(m => m.Result != null));
            return new ResponseData()
            { Result = result, 
            };
        }
       

    }
}