using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Models;

namespace CoreResults
{
    public class CoreJsonResult : JsonResult
    {
        NetResult<ResponseData> _result;
        public CoreJsonResult(object value) : base(value)
        {
            if (value is NetResult<ResponseData>)
            {
                _result = (NetResult<ResponseData>)value;
            }
        }
        public override void ExecuteResult(ActionContext context)
        {
            if (_result== null)
            {
                //TODO 
                context.HttpContext.Response
                    .Body = null;
            }
            if (_result != null)
            {
                context.HttpContext.Response.StatusCode = _result.HttpStatus;

                var body = RepositoryCore.CoreState.RepositoryState.SerializeToStream(_result);
                context.HttpContext.Response.Body = body;
            }                
            base.ExecuteResult(context);
        }

    }


}
