using Newtonsoft.Json;
using RepositoryCore.Enums;
using RepositoryCore.Result;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace CoreResult
{
    public class CoreResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("statusCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; set; }

        /// <summary>
        /// Result
        /// </summary>
        [JsonProperty("result")]
        public object Result { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("error")]
        public ErrorResult Error { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        public CoreResult()
        {

        }
        public CoreResult(ModelStateDictionary modelState)
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
        public CoreResult(object result, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            Result = result;
        }

        /// <summary>
        /// string Error
        /// </summary>
        public CoreResult(string message, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);

            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                case 484:
                    Result = new { obj = message };
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
        public CoreResult(bool isSuccess)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.OK);
            Result = new { success = isSuccess };
        }

        /// <summary>
        /// Result or Error is exist
        /// </summary>
        /// <param name="response"></param>
        /// <param name="code"></param>
        public CoreResult(CoreResult response, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            Result = response.Result;
            Error = response.Error;
        }

        /// <summary>
        /// Error is exist
        /// </summary>
        /// <param name="errorResult"></param>
        /// <param name="code"></param>
        public CoreResult(ErrorResult errorResult, ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            Error = errorResult;
        }

        /// <summary>
        /// Return Result success true or false
        /// </summary>
        /// <param name="code"></param>
        public CoreResult(ResponseStatusCore code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                case 484:
                    Result = new { success = true };
                    break;
                case 401:
                    Error = new ErrorResult { Code = StatusCode, Message = "Не существует пользователь" };
                    break;
                default:
                    Result = new { success = false };
                    break;
            }
        }

        /// <summary>
        /// Error is Exist
        /// </summary>
        /// <param name="errorResult"></param>
        public CoreResult(ErrorResult errorResult)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.BadRequest);
            Error = errorResult;
        }

        public CoreResult(Exception ex)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.Conflict);
            Error = NewMethod(ex);
        }

        private static ErrorResult NewMethod(Exception ex)
        {
            return new ErrorResult(ex);
        }

        public CoreResult(string message)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCore.BadRequest);
            Error = new ErrorResult { Code =(int)ResponseStatusCore.BadRequest, Message = message };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator CoreResult(ErrorResult error) => new CoreResult(error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator CoreResult(bool value) => new CoreResult(value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public static implicit operator CoreResult(ModelStateDictionary modelState)
            => new CoreResult(modelState);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static implicit operator CoreResult(Exception ex) => new CoreResult(ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public static implicit operator CoreResult(ResponseStatusCore code) => new CoreResult(code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public static implicit operator CoreResult(string message) => new CoreResult(message);
                     
    }


}
