using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace zy_erp.Controllers
{
    //主页首页导航的http请求
    public class IndexController : ApiController
    {
        //进入个人中心
        public IEnumerable<string> GetUserInfo()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        
    }
}