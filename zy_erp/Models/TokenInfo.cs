using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zy_erp.Models
{
    public class TokenInfo
    {
        public TokenInfo()
        {
            iss = "COM.Web";
            iat = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            exp = iat + 100000;
            aud = "";
            sub = "COM.Web";
            jti = "COM.Web." + DateTime.Now.ToString("yyyyMMddhhmmssfff");
        }

        /// <summary>
        /// jwt签发者
        /// </summary>
        public string iss { get; set; }
        /// <summary>
        /// jwt的签发时间
        /// </summary>
        public double iat { get; set; }
        /// <summary>
        /// jwt的过期时间，这个过期时间必须要大于签发时间
        /// </summary>
        public double exp { get; set; }
        /// <summary>
        /// 接收jwt的一方
        /// </summary>
        public string aud { get; set; }
        /// <summary>
        /// 定义在什么时间之前，该jwt都是不可用的
        /// </summary>
        public double nbf { get; set; }
        /// <summary>
        /// jwt所面向的用户
        /// </summary>
        public string sub { get; set; }
        /// <summary>
        /// jwt的唯一身份标识，主要用来作为一次性token，从而回避重放攻击
        /// </summary>
        public string jti { get; set; }
    
    }
}