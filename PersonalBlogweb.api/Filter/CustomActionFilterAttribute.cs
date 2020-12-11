using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PersonalBlogweb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// HEADER-访问追踪
        /// </summary>
        public const string REQUEST_ID = "X-Request-Id";

        private ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        public CustomActionFilterAttribute()
        {
            _logger = LogHelper.CreateLogger<CustomActionFilterAttribute>();
        }

        /// <summary>
        /// 执行操作前调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (String.IsNullOrWhiteSpace(context.HttpContext.Request.Headers[REQUEST_ID]))
            {
                context.HttpContext.Request.Headers.Add(REQUEST_ID, Guid.NewGuid().ToString());
            }
            string requestId = context.HttpContext.Request.Headers[REQUEST_ID];
            //_logger.LogInformation(String.Format("OnActionExecuting,{0}:{1},TraceIdentifier:{2}", REQUEST_ID, requestId, context.HttpContext.TraceIdentifier));

            string info = String.Format(@"【{0}】OnActionExecuting:{1},
TraceIdentifier:{2}
ActionDescriptor：{3}
RequestContentType：{4}
Body：{5}
Form：{6}
QueryString：{7}
RequestHeader：{8}
Connection：{9}",
                requestId, context.HttpContext.Request.Path,
                context.HttpContext.TraceIdentifier,
                ContextHelper.GetActionDescriptorInfo(context),
                ContextHelper.GetRequestContentType(context),
                ContextHelper.GetBodyInfo(context),
                ContextHelper.GetFormInfo(context),
                ContextHelper.GetQueryStringInfo(context),
                ContextHelper.GetRequestHeaderInfo(context),
                ContextHelper.GetConnectionInfo(context)
                );
            _logger.LogInformation(info);
        }

        /// <summary>
        /// 执行操作后调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string requestId = context.HttpContext.Request.Headers[REQUEST_ID];
            _logger.LogInformation(String.Format("【{0}】OnActionExecuted:{1}", requestId, context.HttpContext.Request.Path));
        }

        /// <summary>
        /// 执行结果前调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            string requestId = context.HttpContext.Request.Headers[REQUEST_ID];
            _logger.LogInformation(String.Format("【{0}】OnResultExecuting:{1}", requestId, context.HttpContext.Request.Path));

            context.HttpContext.Response.Headers.Add(REQUEST_ID, requestId);
        }

        /// <summary>
        /// 执行结果后调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            string requestId = context.HttpContext.Request.Headers[REQUEST_ID];
            //_logger.LogInformation(String.Format("OnResultExecuted,{0}:{1},TraceIdentifier:{2}", REQUEST_ID, requestId, context.HttpContext.TraceIdentifier));

            string info = String.Format(@"【{0}】OnResultExecuted:{1},
Result:{2}
ResponseHeader:{3}",
                requestId, context.HttpContext.Request.Path,
                ContextHelper.GetResult(context),
                ContextHelper.GetResponseHeaderInfo(context)
                );
            _logger.LogInformation(info);
        }
    }
}
