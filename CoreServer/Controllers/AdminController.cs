
using CoreClient.Models;
using CoreResults;
using CoreServer.Models;
using Microsoft.AspNetCore.Mvc;

using RepositoryCore.Interfaces;
using RepositoryCore.Result;
using Service.Entity.Models;
using System;
using System.Linq;

namespace CoreServer.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        IRepositoryCore<MyModel, string> _myModel;
        IRepositoryCore<Project, string> _project;
        public AdminController(
            IRepositoryCore<MyModel, string> myModel,
        IRepositoryCore<Project, string> project
            )
        {
            _myModel = myModel;
            _project = project;

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
                var item= this.GetResponse(_myModel.FindReverse(m => m.ProjectName == project.ProjectName && m.Lang.ToLower()== model.Lang.ToLower()).Skip(model.Offset).Take(model.Limit));
                return item;

            }catch(Exception ext)
            {
                Console.WriteLine(ext.Message);
                return null;
            }
        }
        /*public ResponseData */

    }
}