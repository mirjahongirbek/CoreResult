using CoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Models;
using System;

namespace CoreResults
{
    public static class StateResult
    {

        public static ResponseData GetResponse(this ControllerBase cBase, object result)
        {
            switch (result)
            {
                case Exception ext:
                    return ErrorResponse(cBase, result);
                case ResponseData response: return GetResponse(cBase, response);
                /* case ResponseData rslt: break;*/
                default:
                    {
                        return new ResponseData() { Result = result };
                    }
            }
         }

        public static ResponseData DefaultResponse()
        {
           return  new ResponseData()
            {
               Result= new { noContent= true}
            };
        }
        public static ResponseData GetResponse(this ControllerBase cBase, ResponseData result)
        {
            if (result == null)
            {
                return DefaultResponse();
            }
            cBase.Response.StatusCode = (int)result.StatusCore;
            return result;
        }
        public static ResponseData GetResponse(this ControllerBase cBase, object result = null, object error = null)
        {
            if (result != null)
            {
                return GetResponse(cBase, result);
            }
            if (error != null)
            {
                return ErrorResponse(cBase, error);
            }
            return null;
        }
        public static ResponseData GetResponse(this ControllerBase cBase, int code)
        {
            var model = CoreState.ById(code);
            return GetResult(cBase, model);
        }
        #region Get Response
        
        public static ResponseData GetResponse(this ControllerBase cBase, object result, int code=0)
        {
            if (code != 0)
            {
               var model= CoreClient.RestState.Client.GetById(code, ModelStatus.DefaultStatus);
                cBase.Response.StatusCode =(int) model.ResponseStatus;
                GetResponse(cBase, model) ;
            }
            return null;
        }

        public static ResponseData GetResult(this ControllerBase cBase, MyModel model)
        {
            cBase.Response.StatusCode = (int)model.StatusCode;
            
            // TODO methos 
            return null;
        }
        public static ResponseData ErrorResponse(this ControllerBase cBase, object error)
        {
            cBase.Response.StatusCode = 400;
            ResponseData result = new ResponseData()
            {
                Error = error
            };
            return result;
            
        }
        #endregion



    }





}
