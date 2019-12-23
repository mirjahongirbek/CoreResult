using CoreResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RepositoryCore.CoreState;
using RepositoryCore.Enums;
using RepositoryCore.Exceptions;
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
            var result = CoreState.Rest?.GetById((int)code, CoreClient.Models.ModelStatus.ResponseStatus);
            //TODO Change
            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                    {

                    }
                    break;
                case 484:
                    //TODO Change
                    //Result = new { T= message };
                    Error = new ErrorResult { Message = "sa" };
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
        public NetResult(CoreException e)
        {
            if (e == null)
            {

            }
            if (e.Id != 0)
            {
                ById(e.Id);
            }

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
        private void ParseResult(ResponseData result)
        {
            Result = ConvertResult<T>(result);
        }

        public NetResult(int status, [CallerMemberName] string caller = null)
        {
            ById(status);
        }
        private void ById(int status)
        {
            var result = CoreState.Rest.GetById(status, CoreClient.Models.ModelStatus.IntStatus);
            if (result.Result != null && !string.IsNullOrEmpty(result.Result.Message))
            {
                ParseResult(result.Result);
            }
            else if (result.ErrorResult != null)
            {
                ParseErrorResult(result.ErrorResult);
            }
            HttpContextHelper.Accessor.HttpContext.Response.StatusCode = (int)result.ResponseStatus;
        }

        public static NetResult<ResponseData> NewResult(int id, [CallerMemberName] string caller = null)
        {
            ResponseData response = new ResponseData() { Code = id };
            var result = new NetResult<ResponseData>();
            result.Result = response;
            return result;
        }
        private static ErrorResult NewMethod(Exception ex, [CallerMemberName] string caller = null)
        {
            return new ErrorResult(ex);
        }

        #region Result
        public static implicit operator NetResult<T>(CoreException e)
        {
            return new NetResult<T>(e);
        }
        public static implicit operator NetResult<T>(Exception e)
        {
            return new NetResult<T>(e);
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
        public static implicit operator NetResult<T>(int a)
        {
            return new NetResult<T>(a);
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


        private static T ConvertResult<T>(ResponseData result)
        {

            if (typeof(ResponseData).GUID == typeof(T).GUID)
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            return (T)RepositoryState.CreateObject<T>();
        }

        private static T ConvertValue<T>(string value)
        {

            if (typeof(ResponseData).GUID == typeof(T).GUID)
            {
                return (T)Convert.ChangeType(new ResponseData { Message = value }, typeof(T));
            }
            return (T)RepositoryState.CreateObject<T>();

        }

    }

}
