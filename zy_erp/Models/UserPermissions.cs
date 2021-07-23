using JWT.Net.Newtonsoft.Json;
using JWT.Net.Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zy_erp.Models
{
    /// <summary>
    /// 用户权限操作
    /// </summary>
    public static class UserPermissions
    {
        /// <summary>
        /// 返回用户所能访问的菜单项，返回类型为json字符串
        /// </summary>
        /// <param name="JWT加密串"></param>
        /// <returns></returns>
        //返回用户所能访问的菜单
        public static string UserIsMenu(string token)
        {
                zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //是否携带用户凭证
            if (token == null)
            {
                return "-1";
            }
            string jsonstr = "";
            try
            {
                //解析jwt加密信息
                jsonstr = TokenHelper.GetDecodingToken(token);
            }
            catch (Exception)
            {
                return "-1";
            }


            //将解析信息转为json对象
            JObject obj = JObject.Parse(jsonstr);
            //获取当前信息的用户id
            string userid = obj["userinfo"]["userid"].ToString();
            string usersel = "exec P_userSelMenu " + int.Parse(userid);

            List<string> menulist = new List<string>();
            var data = db.Database.SqlQuery<P_userSelMenu_Result>(usersel).ToList();

            foreach (var item in data)
            {
                menulist.Add(item.menu_name);
            }
            //List<JObject> m = menulist.ConvertAll(s => (JObject)s);
            var json = JsonConvert.SerializeObject(menulist);
            return json;
        }

        /// <summary>
        /// 用户是否能对当前菜单进行修改
        /// </summary>
        /// <param name="用户id"></param>
        /// <param name="页面id(参考页面文档)"></param>
        /// <param name="操作类似(列如'update')"></param>
        /// <returns></returns>
        public static bool UserIsOperation(int userid, int menuid, string per)
        {
            //实例化db
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //调用存储过程
            string sql = $"exec P_user_menu_Permissions {userid},{menuid}";
            var data = db.Database.SqlQuery<P_user_menu_Permissions_Result>(sql).ToList();
            if (data == null)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].permissions_type == per)
                    {
                        return true;
                    }
                }
                //当前用户无修改权限
                return false;
            }

        }


        /// <summary>
        /// 判断是否非法登录并返回用户id，非法登录返回-1
        /// </summary>
        /// <param name="JWT加密串"></param>
        /// <returns></returns>
        //解析JWT密钥返回用户id
        public static int UserJwt(string token)
        {

            string jsonstr = "";
            try
            {
                //解析jwt加密信息
                jsonstr = TokenHelper.GetDecodingToken(token);
            }
            catch (Exception)
            {
                return -1;
            }

            //将解析信息转为json对象
            JObject obj = JObject.Parse(jsonstr);
            //获取当前信息的用户id
            string userid = obj["userinfo"]["userid"].ToString();
            int tempid = int.Parse(userid.ToString());

            //进入缓存查找判断是否非法登录
            if (RedisHelper.StringGet(userid) == null)
            {
                //非法登录
                return -1;
            }
            else
            {
                return tempid;
            }
        }




    }
}