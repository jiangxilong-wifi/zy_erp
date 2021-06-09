using JWT.Net.Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers
{
    //权限模块请求
    public class PermissionsController : ApiController
    {
        zhongyi_ERPEntities db = new zhongyi_ERPEntities();
        public static string Get()
        {
            return "-1";
        }
        //主页加载
        //判断用户权限类型(优先判断是否登录)
        //public string Post(PostRequest tokens)
        //{
        //    string token = tokens.token;
        //    return UserPermissions.UserIsMenu(token);
        //}
        [HttpPost]
        public string Post(dynamic tokens)
        {
            string token = tokens.jwt;
            return UserPermissions.UserIsMenu(token);
        }

    }
}