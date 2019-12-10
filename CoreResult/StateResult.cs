using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Models;
using RepositoryCore.Result;
using System;

namespace CoreResults
{
    public static class StateResult
    {
        public static ResponseData GetResponse( this ControllerBase cBase, object result)
        {
            switch (result)
            {
                case ErrorResult ext: { 

                    } break;
                case Exception ext: { }break;
                case ResponseData response: { } break;
                case Result rslt: { } break;
            }
        }
        
        public static ResponseData GetResponse(this ControllerBase cBase, object result, int code)
        {
            
        }
        public static ResponseData GetResponse(this ControllerBase cBase, object result, int code, object error)
        {


        }
        public static ResponseData GetResponse(this ControllerBase cBase,object result, object error)
        {

        }
        public static ResponseData GetResponse(this ControllerBase cBase, int code)
        {

        }
    }
    
    



}
