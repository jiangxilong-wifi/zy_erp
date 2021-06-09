using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zy_erp.Models
{
    /// <summary>
    /// 用户POST请求数据接收类
    /// </summary>
    public class PostRequest
    {
        /// <summary>
        /// 用户请求携带的的加密串
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 用户对页面操作的类型
        /// </summary>
        public string type { get; set; }
    }
}