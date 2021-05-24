using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers
{
    public class loginController : ApiController
    {
        zhongyi_ERPEntities db = new zhongyi_ERPEntities();
        // 用户登录
        public bool Post(T_Users user)
        {
            var data = db.T_Users.FirstOrDefault(p=>p.login==user.login&&p.pwd==user.pwd);
            if (data!=null)
            {
                //将用户数据写入缓存,设置过期时间为30分钟
                RedisHelper.StringSet((data.userid).ToString(),data,18000);
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}