using Microsoft.Extensions.Logging;
using PersonalBlogweb.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain
{
    /// <summary>
    /// 角色授权服务
    /// </summary>
    public class RoleAuthorizationServiceImpl : IRoleAuthorizationService
    {
        private readonly ILogger _logger;

        //private RoleAuthorizationProvider roleAuthorizationProvider;
        //private RoleResourceProvider roleResourceProvider;

        public RoleAuthorizationServiceImpl()
        {
            _logger = LogHelper.CreateLogger<RoleAuthorizationServiceImpl>();

            //roleAuthorizationProvider = new RoleAuthorizationProvider();
            //roleResourceProvider = new RoleResourceProvider();
        }

        /// <summary>
        /// 是否已授权
        /// </summary>
        /// <param name="loginType">登录类型</param>
        /// <param name="uri">API地址</param>
        /// <returns></returns>
        public bool IsAuthorized(byte loginType, string uri)
        {
            //var dicRoleResource = QueryRoleResource();
            //var dicRoleAuthorization = QueryRoleAuthorization();

            //int index = uri.IndexOf('/', 1);
            //uri = uri.Substring(index, uri.Length - index);

            //if (!dicRoleResource.ContainsKey(uri))
            //{
            //    //资源不存在
            //    return false;
            //}
            //var resourceList = dicRoleResource[uri];
            //if (resourceList == null || resourceList.Count < 1)
            //{
            //    //资源不存在
            //    return false;
            //}
            //foreach (var item in resourceList)
            //{
            //    if (!dicRoleAuthorization.ContainsKey(loginType))
            //    {
            //        continue;
            //    }
            //    if (!dicRoleAuthorization[loginType].ContainsKey(item.ResourceNo))
            //    {
            //        continue;
            //    }
            //    var authorization = dicRoleAuthorization[loginType][item.ResourceNo];
            //    if (authorization != null)
            //    {
            //        //有授权
            //        return true;
            //    }
            //}

            return false;
        }

        private const string CACHE_KEY_RESOURCE = "F4395236-DD70-4CC2-B3D4-D8C37BA29EB2";
        //private Dictionary<string, List<RoleResource>> QueryRoleResource()
        //{
        //    var instance = Common.CacheService.GetInstance();
        //    Dictionary<string, List<RoleResource>> dic = new Dictionary<string, List<RoleResource>>();
        //    var obj = instance.GetCache(CACHE_KEY_RESOURCE);
        //    if (obj != null)
        //    {
        //        return (Dictionary<string, List<RoleResource>>)obj;
        //    }

        //    dic = roleResourceProvider.QueryAll().GroupBy(o => o.ResourceUri).ToDictionary(o => o.Key, o => o.ToList());
        //    if (dic != null && dic.Count > 0)
        //    {
        //        instance.AddCache(CACHE_KEY_RESOURCE, dic);
        //    }
        //    return dic;
        //}
        //private const string CACHE_KEY_ROLE_AUTH = "3A42FE3D-213C-48EE-A06A-E618EA414644";
        //private Dictionary<byte, Dictionary<string, RoleAuthorization>> QueryRoleAuthorization()
        //{
        //    var instance = Common.CacheService.GetInstance();
        //    Dictionary<byte, Dictionary<string, RoleAuthorization>> dic = new Dictionary<byte, Dictionary<string, RoleAuthorization>>();
        //    var obj = instance.GetCache(CACHE_KEY_ROLE_AUTH);
        //    if (obj != null)
        //    {
        //        return (Dictionary<byte, Dictionary<string, RoleAuthorization>>)obj;
        //    }

        //    var lst = roleAuthorizationProvider.QueryAll();
        //    if (lst == null || lst.Count == 0)
        //    {
        //        return dic;
        //    }
        //    foreach (var item in lst)
        //    {
        //        if (!dic.ContainsKey(item.LoginType))
        //        {
        //            dic.Add(item.LoginType, new Dictionary<string, RoleAuthorization>());
        //        }
        //        if (!dic[item.LoginType].ContainsKey(item.ResourceNo))
        //        {
        //            dic[item.LoginType].Add(item.ResourceNo, item);
        //        }
        //    }
        //    if (dic != null && dic.Count > 0)
        //    {
        //        instance.AddCache(CACHE_KEY_ROLE_AUTH, dic);
        //    }
        //    return dic;
        //}
    }
}
