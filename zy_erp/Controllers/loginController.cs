using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers
{
    //登录接口
    public class loginController : ApiController
    {
        zhongyi_ERPEntities db = new zhongyi_ERPEntities();
        // 用户登录
        [HttpPost]
        [Route("api/login")]
        public string post(T_Users user)
        {
            var data = db.T_Users.FirstOrDefault(p=>p.login==user.login&&p.pwd==user.pwd);
            if (data!=null)
            {
                try
                {
                    //将用户数据写入缓存
                    RedisHelper.StringSet(data.userid.ToString(), data.userid);
                }
                catch (Exception)
                {

                    return "1";
                }
               
                //创建字典
                Dictionary<string, Object> userinfo = new Dictionary<string, Object>();
                T_Users userob = new T_Users()
                {
                    userid=data.userid,
                    username=data.username,
                    login=data.login,
                    pwd=data.pwd
                };
                userinfo.Add("userinfo", userob);
                return TokenHelper.GenerateToken(userinfo);
            }
            else
            {
                return "0";
            }
        }

       
    }
}