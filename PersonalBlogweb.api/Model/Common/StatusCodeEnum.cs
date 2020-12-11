using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlogweb.api
{
    /// <summary>
    /// 系统状态返回
    /// 0-成功
    /// 400-999：同HTTP状态
    /// 1000-1999：常规异常
    /// 21000-21999：认证授权模块自定义异常
    /// 22000-22999：心理测评模块自定义异常
    /// 23000-23999：教师招聘模块自定义异常
    /// </summary>
    public enum StatusCodeEnum : int
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 0,

        #region HTTP状态
        /// <summary>
        /// 等效于 HTTP 状态 400。 
        /// System.Net.HttpStatusCode.BadRequest 指示服务器未能识别请求。 
        /// 如果没有其他适用的错误，或者不知道准确的错误或错误没有自己的错误代码，则发送System.Net.HttpStatusCode.BadRequest。
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// 等效于 HTTP 状态 401。 
        /// System.Net.HttpStatusCode.Unauthorized 指示请求的资源要求身份验证。 
        /// WWW-Authenticate标头包含如何执行身份验证的详细信息。
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// 等效于 HTTP 状态 402。 
        /// 保留 System.Net.HttpStatusCode.PaymentRequired 以供将来使用。
        /// </summary>
        PaymentRequired = 402,

        /// <summary>
        /// 等效于 HTTP 状态 403。 
        /// System.Net.HttpStatusCode.Forbidden 指示服务器拒绝满足请求。
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 等效于 HTTP 状态 404。 
        /// System.Net.HttpStatusCode.NotFound 指示请求的资源不在服务器上。
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 等效于 HTTP 状态 405。 
        /// System.Net.HttpStatusCode.MethodNotAllowed 指示请求的资源上不允许请求方法（POST或 GET）。
        /// </summary>
        MethodNotAllowed = 405,

        /// <summary>
        /// 等效于 HTTP 状态 406。 
        /// System.Net.HttpStatusCode.NotAcceptable 指示客户端已用 Accept 标头指示将不接受资源的任何可用表示形式。
        /// </summary>
        NotAcceptable = 406,

        /// <summary>
        /// 等效于 HTTP 状态 407。 
        /// System.Net.HttpStatusCode.ProxyAuthenticationRequired 指示请求的代理要求身份验证。
        /// Proxy-authenticate 标头包含如何执行身份验证的详细信息。
        /// </summary>
        ProxyAuthenticationRequired = 407,

        /// <summary>
        /// 等效于 HTTP 状态 408。 
        /// System.Net.HttpStatusCode.RequestTimeout 指示客户端没有在服务器期望请求的时间内发送请求。
        /// </summary>
        RequestTimeout = 408,

        /// <summary>
        /// 等效于 HTTP 状态 409。 
        /// System.Net.HttpStatusCode.Conflict 指示由于服务器上的冲突而未能执行请求。
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// 等效于 HTTP 状态 410。 
        /// System.Net.HttpStatusCode.Gone 指示请求的资源不再可用。
        /// </summary>
        Gone = 410,

        /// <summary>
        /// 等效于 HTTP 状态 411。 
        /// System.Net.HttpStatusCode.LengthRequired 指示缺少必需的 Content-length标头。
        /// </summary>
        LengthRequired = 411,

        /// <summary>
        /// 等效于 HTTP 状态 412。 
        /// System.Net.HttpStatusCode.PreconditionFailed 指示为此请求设置的条件失败，且无法执行此请求。
        /// 条件是用条件请求标头（如If-Match、If-None-Match 或 If-Unmodified-Since）设置的。
        /// </summary>
        PreconditionFailed = 412,

        /// <summary>
        /// 等效于 HTTP 状态 413。 
        /// System.Net.HttpStatusCode.RequestEntityTooLarge 指示请求太大，服务器无法处理。
        /// </summary>
        RequestEntityTooLarge = 413,

        /// <summary>
        /// 等效于 HTTP 状态 414。 
        /// System.Net.HttpStatusCode.RequestUriTooLong 指示 URI 太长。
        /// </summary>
        RequestUriTooLong = 414,

        /// <summary>
        /// 等效于 HTTP 状态 415。 
        /// System.Net.HttpStatusCode.UnsupportedMediaType 指示请求是不受支持的类型。
        /// </summary>
        UnsupportedMediaType = 415,

        /// <summary>
        /// 等效于 HTTP 状态 416。 
        /// System.Net.HttpStatusCode.RequestedRangeNotSatisfiable 指示无法返回从资源请求的数据范围，因为范围的开头在资源的开头之前，或因为范围的结尾在资源的结尾之后。
        /// </summary>
        RequestedRangeNotSatisfiable = 416,

        /// <summary>
        /// 等效于 HTTP 状态 417。 
        /// System.Net.HttpStatusCode.ExpectationFailed 指示服务器未能符合 Expect标头中给定的预期值。
        /// </summary>
        ExpectationFailed = 417,

        /// <summary>
        /// 等效于 HTTP 状态 426。 
        /// System.Net.HttpStatusCode.UpgradeRequired 指示客户端应切换为诸如 TLS/1.0之类的其他协议。
        /// </summary>
        UpgradeRequired = 426,

        /// <summary>
        /// 等效于 HTTP 状态 500。 
        /// System.Net.HttpStatusCode.InternalServerError 指示服务器上发生了一般错误。
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// 等效于 HTTP 状态 501。 
        /// System.Net.HttpStatusCode.NotImplemented 指示服务器不支持请求的函数。
        /// </summary>
        NotImplemented = 501,

        /// <summary>
        /// 等效于 HTTP 状态 502。 
        /// System.Net.HttpStatusCode.BadGateway 指示中间代理服务器从另一代理或原始服务器接收到错误响应。
        /// </summary>
        BadGateway = 502,

        /// <summary>
        /// 等效于 HTTP 状态 503。 
        /// System.Net.HttpStatusCode.ServiceUnavailable 指示服务器暂时不可用，通常是由于过多加载或维护。
        /// </summary>
        ServiceUnavailable = 503,

        /// <summary>
        /// 等效于 HTTP 状态 504。 
        /// System.Net.HttpStatusCode.GatewayTimeout 指示中间代理服务器在等待来自另一个代理或原始服务器的响应时已超时。
        /// </summary>
        GatewayTimeout = 504,

        /// <summary>
        /// 等效于 HTTP 状态 505。 
        /// System.Net.HttpStatusCode.HttpVersionNotSupported 指示服务器不支持请求的HTTP 版本。
        /// </summary>
        HttpVersionNotSupported = 505,
        #endregion

        /// <summary>
        /// 失败
        /// </summary>
        [Description("操作失败")]
        ERROR = 1000,

        /// <summary>
        /// 参数异常
        /// </summary>
        [Description("参数异常")]
        PARA_ERROR = 1001,

        /// <summary>
        /// 登录失败
        /// </summary>
        [Description("登录失败")]
        LOGIN_ERROR = 21001,

        /// <summary>
        /// 未登录或者登录已过期
        /// </summary>
        [Description("未登录或者登录已过期")]
        LOGIN_EXPIRED = 21002,

        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("权限不足")]
        PERMISSION_DENIED = 21003,
    }
}
