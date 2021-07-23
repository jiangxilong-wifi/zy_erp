using JWT.Net.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.kc
{
    /// <summary>
    /// 产品入库
    /// </summary>
    public class I_RstorageController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 产品入库
        /// </summary>
        /// <param dy="动态数据类型"></param>
        public bool Post(dynamic dy)
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
                else
                {
                    if (!UserPermissions.UserIsOperation(userid, 2, "insert"))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //获取操作人和经办人
            string rk_human = db.T_Users.FirstOrDefault(p => p.userid == userid).username;
            string rk_jsr = dy.op_rk_jsr.ToString();
            //随机数确定主订单行
            Random rd = new Random();
            double ran =(double)(rd.Next(100))*0.3;
            double ran2 =(double)(rd.Next(100))*0.5;
            double ran3 =(double)(rd.Next(100))*0.2;
            //新增入库订单
            T_Product_output t_put = new T_Product_output()
            {
                product_inventory=0,
                product_date=DateTime.Now,
                state=0,
                Operation= rk_human,
                agent= rk_jsr,
                output_sate= 0,
                state_human="",
                random=(decimal)ran,
                random2= (decimal)ran2,
                random3= (decimal)ran3
            };
            db.T_Product_output.Add(t_put);
            if (db.SaveChanges() > 0)
            {
                //取出对象
                var pro_data = dy.product_item;
                //根据随机数查询刚刚新增的主订单的订单id
                int outputid_t_pro = db.T_Product_output.FirstOrDefault(p => p.random == (decimal)ran&&p.random2== (decimal)ran2 &&p.random3==(decimal)ran3).outputid;
                //新增入库详情单
                foreach (var item in pro_data)
                {
                    //取出数值
                    int productid_i = int.Parse(item.id.ToString());
                    int product_inventory_i = int.Parse(item.number.ToString());
                    T_Product_output_Details t_pro_d = new T_Product_output_Details()
                    {
                        outputid = outputid_t_pro,
                        productid = productid_i,
                        product_inventory = product_inventory_i
                    };
                    db.T_Product_output_Details.Add(t_pro_d);
                    if (db.SaveChanges() > 0)
                    {
                        //更新主订单数量
                        T_Product_output pro_output= db.T_Product_output.FirstOrDefault(p => p.outputid == outputid_t_pro);
                        pro_output.product_inventory += product_inventory_i;
                        db.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else {
                return false;
            }
            
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