using JWT.Net.Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers
{
    public class I_ShopInfoController : ApiController
    {
        //处理商品信息
        
        //分页查询所有商品信息
        public object Get(int ?page,int ?index)//页数，显示行数
        {
            using(zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = $"exec P_SelectProductPage {page},{index}";
                var data = db.Database.SqlQuery<P_SelectProductPage_Result>(sel);
                return data.ToList();
                
            }
        }

        //查询总商品数
        public int Get()
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                var data = db.T_Product.ToList();
                return data.Count();
                
            }
        }


        //查询总商品数
        public object Get(string keywords)
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = string.Format($"exec P_sel_keywords '{keywords}'");
                var data = db.Database.SqlQuery<P_sel_keywords_Result>(sel).ToList();
                return data;

            }
        }

        //根据商品id查询商品信息
        public object Get(int? productid)
        {
            //定义错误信息
            object obj = new
            {
                message = "错误"
            };
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (productid == null)
            {
               
                return obj;
            }
            else
            {
                var data = db.T_Product.FirstOrDefault(p => p.productid == productid);
                if (data == null)
                {
                    return obj;
                }
                else
                {
                    return data;
                }
                
            }
            
        }

        [HttpPost]
        [Route("api/I_ShopInfo/userIsOperation")]
        //用户是否能进行操作
        public bool userIsOperation(dynamic dy)
        {
            //获取用户id
            int userid = 0;
            try
            {

                userid = UserPermissions.UserJwt(dy.token.ToString());
            }
            catch (Exception)
            {

                return false;
            }
            if (UserPermissions.UserIsOperation(userid, 1, dy.type.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //用户对数据进行修改


    }
}