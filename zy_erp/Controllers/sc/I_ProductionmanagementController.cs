using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.sc
{
    /// <summary>
    /// 生产计划管理
    /// </summary>
    public class I_ProductionmanagementController : ApiController
    {
        /// <summary>
        /// 查询所有生产计划
        /// </summary>
        /// <returns></returns>
        public object Get(int page,int index)
        {
            //实例化db
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelProductPlan {page},{index}";
            var data = db.Database.SqlQuery<P_SelProductPlan_Result>(sel).ToList();
            return data;
        }

        /// <summary>
        /// 总生产计划数量
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            return db.T_Production_Plan.Count();
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <returns></returns>
        public object Get(string keywords)
        {
            //实例化db
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelProductPlanKeywords '{keywords}'";
            var data = db.Database.SqlQuery<P_SelProductPlanKeywords_Result>(sel).ToList();
            return data;
        }
        

        /// <summary>
        /// 新增生产计划
        /// </summary>
        /// <param name="value"></param>
        public int Post(dynamic dy)
        {
            // 获取用户id
            int userid = 0;
            try
            {
                //获取用户id
                userid = UserPermissions.UserJwt(dy.token.ToString());
                //判断是否非法登录
                if (userid == -1)
                {
                    return userid;
                }
                else
                {
                    if (!UserPermissions.UserIsOperation(userid, 2, "insert"))
                    {
                        return -2;
                    }
                }
            }
            catch (Exception)
            {

                return 0;
            }


            try
            {
                //实例化db
                zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                string username = db.T_Users.FirstOrDefault(p => p.userid == userid).username;
                //获取新增产品和数量
                string name = dy.name.ToString();
                int productid = db.T_Product.FirstOrDefault(p => p.product_name == name).productid;
                int number = int.Parse(dy.number.ToString());
                //新增生产计划单
                T_Production_Plan production_Plan = new T_Production_Plan()
                {
                    productid = productid,
                    Production_quantity = number,
                    head = username,
                    date = DateTime.Now,
                    state = 0
                };
                db.T_Production_Plan.Add(production_Plan);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
            

        }


        [HttpGet]
        [Route("api/I_Productionmanagement/SelProduct")]
        /// <summary>
        /// 查询前几个商品
        /// </summary>
        /// <returns></returns>
        public object SelProduct()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.T_Product.ToList();
            List<string> rawlist = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                rawlist.Add(data[i].product_name);
            }
            return rawlist;
        }


        [HttpGet]
        [Route("api/I_Productionmanagement/Keywords")]
        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Keywords(string name)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_sel_keywords '{name}'";
            var data = db.Database.SqlQuery<P_sel_keywords_Result>(sel).ToList();
            List<string> raw_name = new List<string>();
            foreach (var item in data)
            {
                raw_name.Add(item.product_name);
            }
            return raw_name;
        }
    }
}