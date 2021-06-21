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
    /// 出库管理
    /// </summary>
    public class I_CstorageController : ApiController
    {
        

        /// <summary>
        /// 产品出库
        /// </summary>
        /// <param name="dy"></param>
        public bool Post(dynamic dy)
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
                    return false;
                }
                else
                {
                    if (!UserPermissions.UserIsOperation(userid, 2, "update"))
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
            string rk_human = dy.op_rk_human.ToString();
            string rk_jsr = dy.op_rk_human.ToString();
            //随机数确定主订单行
            Random rd = new Random();
            float ran = (float)(rd.Next(10)) * 4;
            float ran2 = (float)(rd.Next(50)) * 2;
            float ran3 = (float)(rd.Next(40)) * 3;
            //新增出库订单
            T_Product_output t_put = new T_Product_output()
            {
                product_inventory = 0,
                product_date = DateTime.Now,
                state = 0,
                Operation = rk_human,
                agent = rk_jsr,
                output_sate = 1,//1为出库
                state_human = "",
                random = (decimal)ran,
                random2 = (decimal)ran2,
                random3 = (decimal)ran3
            };
            db.T_Product_output.Add(t_put);
            if (db.SaveChanges() > 0)
            {
                //取出对象
                var pro_data = dy.product_item;
                //根据随机数查询刚刚新增的主订单的订单id
                int outputid_t_pro = db.T_Product_output.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3).outputid;
                //新增出库详情单
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
                        T_Product_output pro_output = db.T_Product_output.FirstOrDefault(p => p.outputid == outputid_t_pro);
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
            else
            {
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