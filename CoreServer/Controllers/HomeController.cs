using CoreClient.Models;
using CoreResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.CoreState;
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
    public class HomeController : ControllerBase
    {
        IRepositoryCore<MyModel, string> _myModel;
        IRepositoryCore<Project, string> _project;
        public HomeController(
            IRepositoryCore<MyModel, string> myModel,
            IRepositoryCore<Project, string> project
            )
        {
            _myModel = myModel;
            _project = project;
        }
        
        #region 
        [HttpPost]
        public MyModel Add([FromBody] MyModel model)
        {
            if (model == null)
            {
                return null;
            }
            _myModel.Add(model);
            return model;
        }
        [HttpPut]
        public NetResult<Result> Update([FromBody] MyModel model)
        {
            _myModel.Update(model);
            return StatusCore.Success;
        }
        [HttpDelete]
        public NetResult<Result> Delete(string id)
        {
            _myModel.Delete(id);
            return StatusCore.Success;
        }
        #endregion

        [HttpGet]
        public List<MyModel> GetAll(string projectName)
        {
            return _myModel.Find(m => m.ProjectName == projectName).ToList();
        }
        #region GetAll
        [HttpPost]
        public MyModel ByTraffic([FromBody]MyModel model)
        {
           return _myModel.GetFirst(m=>m.StatusCode==model.StatusCode);
          
        }
        [HttpGet]
        public List<Project> GetallProject()
        {
            return    _project.FindAll().ToList();
        }
        #endregion
        #region
        [HttpPost]
        public NetResult<Project> AddProject([FromBody] Project model)
        {
            _project.Add(model);
            return model;
        }
        [HttpGet]
        public NetResult<List<string>> GetCulters()
        {
           return RepositoryState.GetAllCulter.Select(m => m.Name).ToList<string>();
                        
        }
        public NetResult<object> GetStatusCore()
        {
           var tip= typeof(StatusCore);
            return null;
        }
        #endregion
        #region
        [HttpPost]
        public List<MyModel> GetbyProject([FromBody]MyModel model)
        {
            
           return _myModel.Find(m => m.ProjectName == model.ProjectName, model.Offset*model.Limit, model.Limit).ToList();
        }
        [HttpPost]
        public NetResult<MyModel> AddMyModel([FromBody] MyModel model)
        {
            if (!ModelState.IsValid)
            {
                return ModelState; 
            }
           var myModel= _myModel.GetFirst(m => m.ProjectName == model.ProjectName && m.StatusCode == model.StatusCode);
            if(myModel== null)
            {
                _myModel.Add(model);
                return model;
            }
            return null;
                            
        }
        [HttpPut]
        public NetResult<Result> UpdateModel([FromBody]MyModel model)
        {
            _myModel.Update(model);
        return StatusCore.Success;
        }
        #endregion
    }
}
