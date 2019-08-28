using Microsoft.AspNetCore.Http;
using RepositoryCore.Enums;
using System;


namespace CoreResult
{
    public static class HttpContextHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static IHttpContextAccessor Accessor;

        /// <summary>
        /// 
        /// </summary>
        public static HttpContext Current => Accessor?.HttpContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public static void SetStatusCode(StatusCore code)
        {
            var responseCode = (int)Enum.ToObject(code.GetType(), code);
            SetStatusCode(responseCode);
        }

        public static int GetStatusCode(StatusCore code)
        {
            return (int)Enum.ToObject(code.GetType(), code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public static void SetStatusCode(int code)
        {
            if (Current?.Response != null)
                Current.Response.StatusCode = code;
        }
    }


}
