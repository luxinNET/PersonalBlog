using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PersonalBlogweb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        public CustomExceptionFilter()
        {
            _logger = LogHelper.CreateLogger<CustomExceptionFilter>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;

            //记录错误日志
            string requestId = context.HttpContext.Request.Headers[CustomActionFilterAttribute.REQUEST_ID];
            string info = String.Format("OnException,{0}:{1},TraceIdentifier:{2}\r\nAPI:{3}",
                CustomActionFilterAttribute.REQUEST_ID, requestId, context.HttpContext.TraceIdentifier,
                context.HttpContext.Request.Path
                );
            //_logger.LogCritical(ex, info);
            _logger.LogError(ex, info);

            context.ExceptionHandled = true; //代表异常已经处理，不会再跳转到开发调试时的异常信息页，可以跳转到我们下面自定义的方法中。若开发过程可以将该行注释掉，则直接抛出异常调试

            context.Result = new JsonResult(ApiResponse<Object>.ResponseFailed(StatusCodeEnum.ERROR, "处理异常"));
        }
    }
}
