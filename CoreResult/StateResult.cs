using CoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Models;
using RepositoryCore.Result;
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
                case ResponseData response: return response;
               /* case ResponseData rslt: break;*/
                default: {
                        return new ResponseData() { Result = result };
                    } 
            }
            return null;
        }        
        public static ResponseData GetResponse(this ControllerBase cBase,object result= null, object error= null)
        {
            if(result!= null)
            {
                return  GetResponse(cBase, result);
            }
            if(error!= null)
            {
                return ErrorResponse(cBase, error);
            }
            return null;
        }
        public static ResponseData GetResponse(this ControllerBase cBase, int code)
        {
           var result= CoreClient.RestState.Client.GetById(code, CoreClient.Models.ModelStatus.IntStatus);
           return GetResult(cBase, result);
        }
        
        

        #region Get Response

        public static ResponseData GetResponse(this ControllerBase cBase, object result, int code)
        {
            return null;
        }
        public static ResponseData GetResult(this ControllerBase cBase, MyModel model)
        {
            // TODO methos 
            return null;
        }
        public static ResponseData ErrorResponse(this ControllerBase cBase, object error)
        {
            return null;
        }
        #endregion



    }

    



}
