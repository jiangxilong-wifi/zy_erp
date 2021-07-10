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
    /// <summary>
    /// 商品信息页面的处理
    /// </summary>
    public class I_ShopInfoController : ApiController
    {
        /// <summary>
        /// 分页查询所有商品信息
        /// </summary>
        /// <param name="页码"></param>
        /// <param name="显示行数"></param>
        /// <returns></returns>
        public object Get(int ?page,int ?index)//页数，显示行数
        {
            using(zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = $"exec P_SelectProductPage {page},{index}";
                var data = db.Database.SqlQuery<P_SelectProductPage_Result>(sel);
                return data.ToList();
                
            }
        }

        /// <summary>
        /// 查询总商品数
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                var data = db.T_Product_Inventory.ToList();
                return data.Count();
                
            }
        }


        /// <summary>
        /// 产品表模糊查询
        /// </summary>
        /// <param name="模糊查询的字段"></param>
        /// <returns></returns>
        public object Get(string keywords)
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = string.Format($"exec P_sel_keywords '{keywords}'");
                var data = db.Database.SqlQuery<P_sel_keywords_Result>(sel).ToList();
                return data;

            }
        }


        /// <summary>
        /// 根据商品id查询商品信息
        /// </summary>
        /// <param name="商品id"></param>
        /// <returns></returns>
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



        /// <summary>
        /// 用户是否能进行操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_ShopInfo/userIsOperation")]
        public bool userIsOperation(dynamic dy)
        {
            //获取用户id
            int userid = 0;
            try
            {
                //获取用户id
                userid = UserPermissions.UserJwt(dy.token.ToString());
                //判断是否非法登录
                if (userid == -1)
                {
                    return false;
                }
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


        /// <summary>
        /// 用户对数据进行修改
        /// </summary>
        /// <param name="产品表字段"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_ShopInfo/updated")]
        public bool updated(T_Product product)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (product == null)
            {
                return false;
            }
            //查询当前产品号
            T_Product producted = db.T_Product.FirstOrDefault(p => p.productid == product.productid);
            //修改当前产品值
            producted.product_name = product.product_name;
            producted.product_introduce = product.product_introduce;
            producted.product_use= product.product_use;
            producted.product_composition= product.product_composition;
            producted.product_weaving= product.product_weaving;
            producted.product_process = product.product_process;
            producted.product_price = product.product_price;
            //将修改同步到数据库
            if (db.SaveChanges()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_ShopInfo/DelPro")]
        public int DelPro(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //获取传输数值
            int userid = UserPermissions.UserJwt(dy.token.ToString());
            //获取操作类型
            string type = dy.type.ToString();
            //获取id
            int pid = int.Parse(dy.productid.ToString());
            //判断能否进行删除操作
            if (UserPermissions.UserIsOperation(userid,1,type))
            {
                try
                {
                    var data = db.T_Product.FirstOrDefault(p => p.productid == pid);
                    db.T_Product.Remove(data);
                    if (db.SaveChanges() > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception)
                {

                    return -1;
                }
               
            }
            else
            {
                return 0;
            }
            
        }

    }
}