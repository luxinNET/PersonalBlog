using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using PersonalBlogweb.Common;
using PersonalBlogweb.Domain;

namespace PersonalBlogweb.api
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public const string HEADER_TOKEN = "TOKEN";
        private ILogger _logger;
        private IUserService _userService;
        private IAccessLogService _accessLogService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="accessLogService"></param>
        public CustomAuthorizationFilter(IUserService userService, IAccessLogService accessLogService)
        {
            _userService = userService;
            _accessLogService = accessLogService;
            _logger = LogHelper.CreateLogger<CustomAuthorizationFilter>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string requestId = context.HttpContext.Request.Headers[CustomActionFilterAttribute.REQUEST_ID];
            if (String.IsNullOrWhiteSpace(requestId))
            {
                requestId = Guid.NewGuid().ToString();
                context.HttpContext.Request.Headers.Add(CustomActionFilterAttribute.REQUEST_ID, requestId);
            }

            string token = context.HttpContext.Request.Headers[HEADER_TOKEN];
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("【{0}】OnAuthorization:{1}", requestId, context.HttpContext.Request.Path);
            var str2 = ContextHelper.GetFormInfo(context);
            if (!String.IsNullOrWhiteSpace(str2))
            {
                strBuilder.AppendFormat("\r\nForm:{0}", str2);
            }
            var str3 = ContextHelper.GetQueryStringInfo(context);
            if (!String.IsNullOrWhiteSpace(str3))
            {
                strBuilder.AppendFormat("\r\nQueryString:{0}", str3);
            }
            if (!String.IsNullOrWhiteSpace(token))
            {
                strBuilder.AppendFormat("\r\nTOKEN:{0}", token);
            }
            _logger.LogInformation(strBuilder.ToString());

            //访问日志
            AccessLog accessLog = new AccessLog();
            accessLog.AccessPath = context.HttpContext.Request.Path;
            accessLog.LoginCode = String.Empty;
            accessLog.UserNo = String.Empty;
            string opUser = String.Empty;

            //匿名访问
            Type allowAnonymousAttribute = typeof(AllowAnonymousAttribute);
            foreach (var item in context.ActionDescriptor.EndpointMetadata)
            {
                if (item.GetType() == allowAnonymousAttribute)
                {
                    //接口允许匿名访问，记录访问日志
                    accessLog.AccessStatus = (byte)EntityStatusEnum.NORMAL;
                    _accessLogService.Add(accessLog, opUser);
                    return;
                }
            }

            //认证信息
            _logger.LogInformation(String.Format("【{0}】开始认证信息：{1}", requestId, context.HttpContext.Request.Path));
            ILoginUser userModel = null;
            accessLog.LoginCode = token ?? String.Empty;
            if (!String.IsNullOrWhiteSpace(token))
            {
                userModel = _userService.QueryLogin(token);
            }
            if (userModel == null)
            {
                //记录错误日志
                string info = String.Format("【{0}】未登录或者登录已过期:{1}", requestId, context.HttpContext.Request.Path);
                _logger.LogError(info);

                context.Result = new JsonResult(ApiResponse<Object>.ResponseFailed(StatusCodeEnum.LOGIN_EXPIRED, EnumHelper.GetEnumDescription(StatusCodeEnum.LOGIN_EXPIRED)));

                //记录拦截日志
                accessLog.AccessStatus = (byte)EntityStatusEnum.DISABLED;
                _accessLogService.Add(accessLog, opUser);
                return;
            }
            accessLog.UserNo = userModel.UserNo ?? String.Empty;
            opUser = userModel.UserAccount;

            //授权信息
            _logger.LogInformation(String.Format("【{0}】开始授权信息：{1}", requestId, context.HttpContext.Request.Path));
            IPrincipal principal = new UserPrincipal(new UserIdentity(userModel));
            if (!principal.IsInRole(context.HttpContext.Request.Path))
            {
                //记录错误日志
                string info = String.Format("【{0}】未授权：{1}", requestId, context.HttpContext.Request.Path);
                _logger.LogError(info);

                context.Result = new JsonResult(ApiResponse<Object>.ResponseFailed(StatusCodeEnum.PERMISSION_DENIED, EnumHelper.GetEnumDescription(StatusCodeEnum.PERMISSION_DENIED)));

                //记录拦截日志
                accessLog.AccessStatus = (byte)EntityStatusEnum.DISABLED;
                _accessLogService.Add(accessLog, opUser);
                return;
            }

            Thread.CurrentPrincipal = principal;
            //每次都重新覆盖user，避免不同用户对不同action的访问
            //对于web-hosting，你必须在这两个地方食指principal，否则安全上下文可能会变得不一致。
            context.HttpContext.User = new ClaimsPrincipal(principal);

            //记录成功日志
            accessLog.AccessStatus = (byte)EntityStatusEnum.NORMAL;
            _accessLogService.Add(accessLog, opUser);

            _logger.LogInformation(String.Format("【{0}】授权通过：{1}", requestId, context.HttpContext.Request.Path));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        /// <summary>
        /// 
        /// </summary>
        private IRoleAuthorizationService roleAuthorizationService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="identity"></param>
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
            roleAuthorizationService = new RoleAuthorizationServiceImpl();
        }

        /// <summary>
        /// 
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            //return true;//用于屏蔽授权校验
            LoginInfoVO loginUser = JsonConvert.DeserializeObject<LoginInfoVO>(Identity.Name);
            return roleAuthorizationService.IsAuthorized(loginUser.LoginType, role);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="user"></param>
        public UserIdentity(ILoginUser user)
        {
            if (user != null)
            {
                IsAuthenticated = true;
                Name = JsonConvert.SerializeObject(user);
                AuthenticationType = "CustomAuthentication";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationType { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
    }
}
