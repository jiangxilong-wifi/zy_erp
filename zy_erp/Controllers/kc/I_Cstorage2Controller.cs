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
    /// 原材料出库
    /// </summary>
    public class I_Cstorage2Controller : ApiController
    {
        /// <summary>
        /// 查询原材料库存信息
        /// </summary>
        /// <returns></returns>
        public object Get(int page, int index)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_Sel_RawMaterial_Inventory_pages {page},{index}";
            var data = db.Database.SqlQuery<P_Sel_RawMaterial_Inventory_pages_Result>(sel);
            return data.ToList();
        }


        /// <summary>
        /// 原材料提交出库
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
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
                    if (!UserPermissions.UserIsOperation(userid, 2, "update"))
                    {
                        return -1;
                    }
                }
            }
            catch (Exception)
            {

                return -1;
            }
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //获取操作人和经办人
            string rk_human = dy.op_rk_human.ToString();
            string rk_jsr = dy.op_rk_human.ToString();
            //随机数确定主订单行
            Random rd = new Random();
            //生成随机总数 
            float ran = (float)(rd.Next(100) * 0.341);
            float ran2 = (float)((rd.Next(100)) * 0.2);
            float ran3 = (float)((rd.Next(100)) * 0.3);
            //新增出库单
            T_Raw_Material_output t_put = new T_Raw_Material_output()
            {
                rawmaterial_inventory = 0,
                rawmaterial_date = DateTime.Now,
                state = 0,
                Operation = rk_human,
                agent = rk_jsr,
                output_sate = 0,
                state_human = "",
                random = (decimal)ran,
                random2 = (decimal)ran2,
                random3 = (decimal)ran3
            };
            db.T_Raw_Material_output.Add(t_put);
            if (db.SaveChanges() > 0)
            {
                //根据随机数查询刚刚新增的主订单的订单id
                int outputid_t_pro = int.Parse(db.T_Raw_Material_output.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3).outputid.ToString());
                //取出对象
                var pro_data = dy.Raw_Material;
                //新增入库详情单
                foreach (var item in pro_data)
                {
                    //获取原材料id
                    int rawid = int.Parse(item.rawid.ToString());
                    //取出每一条订单id值
                    int productid_i = int.Parse(item.id.ToString());
                    //获取取出数量
                    int number = int.Parse(item.number.ToString());
                    T_Raw_Material_output_Details t_pro_d = new T_Raw_Material_output_Details()
                    {
                        outputid = outputid_t_pro,
                        rawmaterialid = rawid,
                        rawmaterial_inventory = number
                    };
                    db.T_Raw_Material_output_Details.Add(t_pro_d);
                    //数据同步数据库
                    db.SaveChanges();

                    //修改库存的库存数
                    T_Raw_Material_Inventory _Raw_Material_Inventory = db.T_Raw_Material_Inventory.FirstOrDefault(p => p.Inventoryid == productid_i);
                    //修改库存数
                    _Raw_Material_Inventory.stockcount -= number;
                    db.SaveChanges();

                    //修改原材料出库单为已申请出库，累加出库数量
                    T_Raw_Material_output data = db.T_Raw_Material_output.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3);
                    data.rawmaterial_inventory += number;
                    data.state = -1;
                    db.SaveChanges();

                }

                return 1;
            }
            else
            {
                return 0;
            }

        }


    }
}