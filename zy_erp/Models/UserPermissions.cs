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


        //用户是否能对当前菜单进行修改
        public static bool UserIsOperation(int userid,int menuid,string per)
        {
            //实例化db
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //调用存储过程
            string sql = $"exec P_user_menu_Permissions {userid},{menuid}";
            var data = db.Database.SqlQuery<P_user_menu_Permissions_Result>(sql).ToList();
            //定义判断变量，不为0则为true
            int i = 0;
            if (data == null)
            {
                return false;
            }
            else
            {
                while (true)
                {
                    if (data[i].permissions_type == per)
                    {
                        return true;
                    }
                    i++;
                    if (i == 4)
                    {
                        //当前用户无修改权限
                        return false;
                    }
                }
            }
            
        }

       

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
            return int.Parse(userid.ToString());
        }

    }
}