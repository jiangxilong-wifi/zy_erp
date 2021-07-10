using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.kc
{
    /// <summary>
    /// 原材料入库
    /// </summary>
    public class I_Rstorage2Controller : ApiController
    {
        /// <summary>
        /// 查询原材料已到货订单
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();

            List<object> objlist = new List<object>();
            List<object> objlist2 = new List<object>();
            //已到货订单且为入库订单
            var data = db.T_PurchaseOrders.Where(p => p.state_ok == 1&&p.output_ok==0).ToList();

            foreach (var item in data)
            {
                object obje = new
                {
                    id = item.purchaseorderid,
                    price = item.purchaseorder_price,
                    num = item.purchaseorder_numberMax,
                    date = item.date,
                    pruch_ = item.T_PurchaseOrders_Details
                };
                objlist.Add(obje);
            }
            return objlist;
        }

        /// <summary>
        /// 新增已到货原材料入库
        /// </summary>
        /// <param name="value"></param>
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
            string rk_human = dy.op_rk_human.ToString();
            string rk_jsr = dy.op_rk_human.ToString();
            //获取订单id
            int ordid = int.Parse(dy.orderid.ToString());
            //根据订单号查询总数量
            int num = int.Parse(db.T_PurchaseOrders.FirstOrDefault(p => p.purchaseorderid == ordid).purchaseorder_numberMax.ToString());
            //随机数确定主订单行
            Random rd = new Random();
            //生成随机总数 
            float ran = (float)(rd.Next(100)*0.341);
            float ran2 = (float)((rd.Next(100)) * 0.2);
            float ran3 = (float)((rd.Next(100)) * 0.3);
            //新增入库订单
            T_Raw_Material_output t_put = new T_Raw_Material_output()
            {
                rawmaterial_inventory = num,
                rawmaterial_date = DateTime.Now,
                state = 0,
                Operation = rk_human,
                agent = rk_jsr,
                output_sate = 0,
                state_human = "",
                random = (decimal)ran,
                random2=(decimal)ran2,
                random3=(decimal)ran3
            };
            db.T_Raw_Material_output.Add(t_put);
            if (db.SaveChanges() > 0)
            {
                //根据随机数查询刚刚新增的主订单的订单id和总数量
                int outputid_t_pro = int.Parse(db.T_Raw_Material_output.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3).outputid.ToString());
                int outputid_t_num = int.Parse(db.T_Raw_Material_output.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3).rawmaterial_inventory.ToString());
                //取出对象
                var pro_data = dy.Raw_Material;
                //新增入库详情单
                foreach (var item in pro_data)
                {
                    //取出每一条订单id值(主订单id)
                    int productid_i = int.Parse(item.id.ToString());
                    //商品id
                    int rawmateridal_id = int.Parse(item.pro_id.ToString());
                    T_Raw_Material_output_Details t_pro_d = new T_Raw_Material_output_Details()
                    {
                        outputid = outputid_t_pro,
                        rawmaterialid = rawmateridal_id,
                        rawmaterial_inventory = outputid_t_num
                    };
                    db.T_Raw_Material_output_Details.Add(t_pro_d);
                    //数据同步数据库
                    db.SaveChanges();
                    //修改主订单状态为已经提交入库申请（-1）
                    T_PurchaseOrders data = db.T_PurchaseOrders.FirstOrDefault(p => p.purchaseorderid == productid_i);
                    data.output_ok = -1;
                    db.SaveChanges();
                   
                }

                return true;
            }
            else
            {
                return false;
            }

        }

    }
}