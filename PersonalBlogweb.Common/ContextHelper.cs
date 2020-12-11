using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PersonalBlogweb.Common
{

    public class ContextHelper
    {
        public static string GetActionDescriptorInfo(ActionContext context)
        {
            string info = String.Format("{0}",
                JsonConvert.SerializeObject(context.ActionDescriptor.RouteValues)
                );
            return info;
        }

        public static string GetBodyInfo(ActionContext context)
        {
            if (context.HttpContext.Request.ContentLength > 1024 * 1024)
            {
                return String.Empty;
            }
            if (context.HttpContext.Request.Body == null || context.HttpContext.Request.ContentLength == null || context.HttpContext.Request.ContentLength == 0)
            {
                return String.Empty;
            }
            if (!context.HttpContext.Request.ContentType.Contains("application/json"))
            {
                return String.Empty;
            }

            string result = null;
            var stream = context.HttpContext.Request.Body;
            Encoding encoding = Encoding.UTF8;

            stream.Position = 0;
            using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
            {
                result = reader.ReadToEnd();
                /*
                这里要注意：stream.Position = 0;
                当你读取完之后必须把stream的位置设为开始
                因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。
                */
                stream.Position = 0;
            }

            return result;
        }

        public static string GetFormInfo(ActionContext context)
        {
            if (context.HttpContext.Request.ContentLength > 1024 * 1024)
            {
                return String.Empty;
            }
            if (String.IsNullOrWhiteSpace(context.HttpContext.Request.ContentType)
                || (
                !context.HttpContext.Request.ContentType.Contains("application/x-www-form-urlencoded")
                && !context.HttpContext.Request.ContentType.Contains("multipart/form-data")
                ))
            {
                return String.Empty;
            }
            return JsonConvert.SerializeObject(context.HttpContext.Request.Form);
        }

        public static string GetQueryStringInfo(ActionContext context)
        {
            if (!context.HttpContext.Request.QueryString.HasValue)
            {
                return String.Empty;
            }
            return JsonConvert.SerializeObject(context.HttpContext.Request.QueryString.Value);
        }

        public static string GetRequestContentType(ActionContext context)
        {
            return context.HttpContext.Request.ContentType ?? String.Empty;
        }

        public static string GetRequestHeaderInfo(ActionContext context)
        {
            return JsonConvert.SerializeObject(context.HttpContext.Request.Headers);
        }

        public static string GetResponseHeaderInfo(ActionContext context)
        {
            if (context.HttpContext.Response == null)
            {
                return String.Empty;
            }
            return JsonConvert.SerializeObject(context.HttpContext.Response.Headers);
        }
        public static string GetConnectionInfo(ActionContext context)
        {
            return String.Format("RemoteIpAddress:{0},RemotePort:{1},LocalIpAddress:{2},LocalPort:{3},ClientCertificate:{4},Id:{5}",
                context.HttpContext.Connection.RemoteIpAddress.ToString(),
                context.HttpContext.Connection.RemotePort,
                context.HttpContext.Connection.LocalIpAddress.ToString(),
                context.HttpContext.Connection.LocalPort,
                JsonConvert.SerializeObject(context.HttpContext.Connection.ClientCertificate),
                context.HttpContext.Connection.Id
                );
        }
        public static string GetResult(ResultExecutedContext context)
        {
            if (context.Result == null
                || context.HttpContext.Response.ContentType == null
                || !context.HttpContext.Response.ContentType.Contains("application/json")
                )
            {
                return String.Empty;
            }
            return JsonConvert.SerializeObject(context.Result);
        }
    }
}
