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
    public class PermissionsController : ApiController
    {
        zhongyi_ERPEntities db = new zhongyi_ERPEntities();
        public static string Get()
        {
            return "123";
        }
        //判断用户权限类型(优先判断是否登录)
        public HttpResponseMessage Get(int userid)
        {
            string usersel = "exec P_Select_User_Menu "+userid;
            List<string> menulist = new List<string>();
            var data = db.Database.SqlQuery<P_Select_User_Menu_Result>(usersel).ToList();
            foreach (var item in data)
            {
                menulist.Add(item.menu_name);
            }
            string json= JsonConvert.SerializeObject(menulist);
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        /// <summary> 
        /// 对象集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += item+ ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }

        

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}