using CoreResult;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepositoryCore.CoreState;
using RepositoryCore.Enums;
using RepositoryCore.Models;
using RepositoryCore.Result;
using System;
using System.Runtime.CompilerServices;

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
        public NetResult(ModelStateDictionary modelState, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(StatusCore.BadRequest);
            foreach (var state in modelState)
            {
                if (state.Value.ValidationState != ModelValidationState.Invalid)
                    continue;
                Error = new ErrorResult { Code = (int)StatusCore.BadRequest, Message = $"{state.Key} не соответствует" };
                break;
            }
        }
        public NetResult(T result, StatusCore code, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(code);
            Result = result;
        }
        public NetResult(string message, StatusCore code, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            //TODO Change
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
        public NetResult(bool isSuccess, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(StatusCore.OK);
        }
        public NetResult(ErrorResult errorResult, StatusCore code, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            Error = errorResult;
        }
        public NetResult(StatusCore code, [CallerMemberName] string caller = null)
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
        public NetResult(ErrorResult errorResult, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(StatusCore.BadRequest);
            Error = errorResult;
        }
        public NetResult(Exception ex, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(StatusCore.Conflict);
            Error = NewMethod(ex);
        }
        public NetResult(string message, [CallerMemberName] string caller = null)
        {
            HttpContextHelper.SetStatusCode(StatusCore.Success);
            Error = new ErrorResult { Code = (int)StatusCore.BadRequest, Message = message };
            Result = ConvertValue<T>(message);

        }
        public NetResult(T model, [CallerMemberName] string caller = null)
        {
            Result = model;
        }
        private void ParseErrorResult(ErrorResult err)
        {
            Error = err;
        }
        private void ParseResult(Result result)
        {
           Result= ConvertResult<T>(result);
        }
        public NetResult(int status, bool isError, [CallerMemberName] string caller = null)
        {
             var result= CoreState.Rest.GetById(status);
            if (result.ErrorResult != null)
            {
                ParseErrorResult(result.ErrorResult);
            }else if(result.Result!= null)
            {
                ParseResult(result.Result);
            }
            HttpContextHelper.SetStatusCode(result.ResponseStatus);
        }
        
        public static NetResult<Result> NewResult(int id, [CallerMemberName] string caller = null)
        {
            Result response = new Result() { Code = id };
            var result = new NetResult<Result>();
            result.Result = response;
            return result;
        }
        private static ErrorResult NewMethod(Exception ex, [CallerMemberName] string caller = null)
        {
            return new ErrorResult(ex);
        }
  
         #region Result
        public static implicit operator NetResult<T>(Exception ext)
        {
            return new NetResult<T>(ext);
        }
        public static implicit operator NetResult<T>(ErrorResult error)
        {
            return new NetResult<T>(error);
        }
        public static implicit operator NetResult<T>(bool value)
        {
            return new NetResult<T>(value);
        }
        public static implicit operator NetResult<T>(ModelStateDictionary modelState)
        {
            return new NetResult<T>(modelState);
        }
        public static implicit operator NetResult<T>(StatusCore code)
        {
            return new NetResult<T>(code);
        }
        public static implicit operator NetResult<T>(string message)
        {
            return new NetResult<T>(message);
        }

        public static implicit operator NetResult<T>(T model)
        {
            return new NetResult<T>(model);
        }

        #endregion
        private static T ConvertResult<T>(Result result)
        {
            if (typeof(Result).GUID == typeof(T).GUID)
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            return (T)RepositoryState.CreateObject<T>();
        }
        private static T ConvertValue<T>(string value)
        {

            if (typeof(Result).GUID == typeof(T).GUID)
            {
                return (T)Convert.ChangeType(new Result { Message = value }, typeof(T));
            }
            return (T)RepositoryState.CreateObject<T>();

        }

    }
}
