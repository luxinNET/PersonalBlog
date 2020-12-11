using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Common
{
    /// <summary>
    /// OSS配置
    /// </summary>
    public class OssConfigVO
    {
        /// <summary>
        /// 访问节点-服务端使用
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 非阿里云访问节点-用于生成外部访问地址
        /// </summary>
        public string EndpointInternet { get; set; }
        /// <summary>
        /// 阿里内网访问节点-用于生成外部访问地址
        /// </summary>
        public string EndpointInternal { get; set; }
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string BucketName { get; set; }
        /// <summary>
        /// 阿里云文件夹
        /// </summary>
        public string RootPath { get; set; }
    }
}
