using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    /// <summary>
    /// 统一封装HEADER等校验操作
    /// </summary>
    //[Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        //protected LoginInfoVO GetLoginInfo()
        //{
        //    IIdentity identity = Request.HttpContext.User.Identity;
        //    if (identity == null)
        //    {
        //        throw new Exception("未登录或者登录已过期");
        //    }

        //    LoginInfoVO loginUser = JsonConvert.DeserializeObject<LoginInfoVO>(identity.Name);
        //    return loginUser;
        //}
    }
}
