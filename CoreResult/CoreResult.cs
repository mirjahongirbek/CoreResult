using CoreResult;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepositoryCore.Enums;
using RepositoryCore.Models;
using RepositoryCore.Result;
using System;

namespace CoreResults
{
    public class NetResult<T>
        where T : class
    {
        public int StatusCode { get; set; }
        public T Result { get; set; }
        public string Id { get; set; }
        public ErrorResult Error { get; set; }
        public NetResult()
        {

        }
        public NetResult(ModelStateDictionary modelState)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.BadRequest);

            foreach (var state in modelState)
            {
                if (state.Value.ValidationState != ModelValidationState.Invalid)
                    continue;
                Error = new ErrorResult { Code = (int)ResponseStatusCore.BadRequest, Message = $"{state.Key} не соответствует" };
                break;
            }
        }

        /// <summary>
        /// Empty
        /// </summary>
        public NetResult(T result, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            Result = result;
        }
        public static NetResult<Result> NewResult(string message)
        {

            return null;
        }
        public static NetResult<Result> NewResult(int id)
        {
            Result response = new Result() { Code = id };
            var result = new NetResult<Result>();
            result.Result = response;

            return result;
        }
        /// <summary>
        /// string Error
        /// </summary>
        public NetResult(string message, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);

            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                case 484:
                    //TODO Change
                    //Result = new { T= message };
                    Error = null;
                    break;

                default:
                    Result = null;
                    Error = new ErrorResult { Code = StatusCode, Message = message };
                    break;
            }
        }

        /// <summary>
        /// Result is exist
        /// </summary>
        /// <param name="isSuccess"></param>
        public NetResult(bool isSuccess)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.OK);

            //Result =new T { };
        }

        /// <summary>
        /// Result or Error is exist
        /// </summary>
        /// <param name="response"></param>
        /// <param name="code"></param>
        //TODO change
            //public NetResult(CoreResult response, ResponseStatusCore code)
        //{
        //    HttpContextHelper.SetStatusCode(code);
        //    Result = response.Result;
        //    Error = response.Error;
        //}

        /// <summary>
        /// Error is exist
        /// </summary>
        /// <param name="errorResult"></param>
        /// <param name="code"></param>
        public NetResult(ErrorResult errorResult, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            Error = errorResult;
        }

        /// <summary>
        /// Return Result success true or false
        /// </summary>
        /// <param name="code"></param>
        public NetResult(ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                case 484:
                    //TODO change
                    //Result = new { success = true };
                    break;
                case 401:
                    Error = new ErrorResult { Code = StatusCode, Message = "Не существует пользователь" };
                    break;
                default:
                    //TODO Change
                    //Result = new { success = false };
                    break;
            }
        }

        /// <summary>
        /// Error is Exist
        /// </summary>
        /// <param name="errorResult"></param>
        public NetResult(ErrorResult errorResult)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.BadRequest);
            Error = errorResult;
        }

        public NetResult(Exception ex)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.Conflict);
            Error = NewMethod(ex);
        }

        private static ErrorResult NewMethod(Exception ex)
        {
            return new ErrorResult(ex);
        }

        public NetResult(string message)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.BadRequest);
            Error = new ErrorResult { Code = (int)ResponseStatusCore.BadRequest, Message = message };
        }


        public static implicit operator NetResult<T>(Exception ext)
        {
            return null;
        }
        public static implicit operator NetResult<T>(ErrorResult error)
        {
            return null;
        }

        public static implicit operator NetResult<T>(bool value)
        {
            return null;
        }

        public static implicit operator NetResult<T>(ModelStateDictionary modelState)
        {
            return null;
        }

        
        public static implicit operator NetResult<T>(ResponseStatusCore code)
        {
            return null;
        }

        public static implicit operator NetResult<T>(string message)
        {
            return null;
        }
    }


    public static class CoreState
    {
        public static NetResult<Result> GetResult(string message)
        {
            return NetResult<Result>.NewResult(message);
        }
        public static NetResult<Result> GetResult(int message)
        {
            return NetResult<Result>.NewResult(message);
        }



    }



}
