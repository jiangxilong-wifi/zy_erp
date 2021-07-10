using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.cg
{
    /// <summary>
    /// 采购单操作
    /// </summary>
    public class I_Purchase_requestController : ApiController
    {
        /// <summary>
        /// 随机查询原材料
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.T_Raw_Material.ToList();
            List<string> rawlist = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                rawlist.Add(data[i].rawmaterial_name);
            }
            return rawlist;
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelRaw_Material_keywords '{name}'";
            var data = db.Database.SqlQuery<P_SelRaw_Material_keywords_Result>(sel).ToList();
            List<string> raw_name = new List<string>();
            foreach (var item in data)
            {
                raw_name.Add(item.rawmaterial_name);
            }
            return raw_name;
        }


        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <returns></returns>
        [Route("api/I_Purchase_request/SelSupplier")]
        [HttpPost]
        public object SelSupplier()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.T_Supplier.ToList();
            List<string> supplist = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                supplist.Add(data[i].supplier_name);
            }
            return supplist;
        }
        /// <summary>
        /// 获取原材料名查询对应原材料数据
        /// </summary>
        /// <param name="dy"></param>
        public object Post(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //原材料名称
            string name = dy.name.ToString();
            T_Raw_Material data = db.T_Raw_Material.FirstOrDefault(p => p.rawmaterial_name == name);

            if (data != null)
            {
                object rawdata = new
                {
                    id = data.rawmaterialtid,
                    name = data.rawmaterial_name,
                    price = (double)data.rawmaterial_price
                };
                return rawdata;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        [Route("api/I_Purchase_request/PurInsert")]
        public int PurInsert(dynamic ts)
        {
            if (ts != null)
            {
                zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                //获取供应商id
                string suppname = ts.suppname.ToString();
                int suppid = db.T_Supplier.FirstOrDefault(p => p.supplier_name == suppname).supplierid;
                //随机数确定主订单行
                Random rd = new Random();
                //生成随机总数 
                float ran = (float)(rd.Next(100) * 0.34);
                float ran2 = (float)((rd.Next(100)) * 0.2);
                float ran3 = (float)((rd.Next(100)) * 0.3);
                //新增主订单数据
                T_PurchaseOrders purchaseOrders = new T_PurchaseOrders()
                {
                    supplierid = suppid,
                    purchaseorder_price = 0,
                    purchaseorder_numberMax = 0,
                    state = 0,
                    date = DateTime.Now,
                    state_ok = 0,
                    output_ok = 0,
                    random = (decimal)ran,
                    random2 = (decimal)ran2,
                    random3 = (decimal)ran3
                };
                db.T_PurchaseOrders.Add(purchaseOrders);
                //同步数据库
                db.SaveChanges();
                //获取主订单id
                int purchaseOrdersid = db.T_PurchaseOrders.FirstOrDefault(p => p.random == (decimal)ran && p.random2 == (decimal)ran2 && p.random3 == (decimal)ran3).purchaseorderid;
                //累加器存储总数量和价格
                int sumnumber = 0;
                float sumprice = 0;
                //获取数据
                foreach (var item in ts.t_PurchaseOrders_s)
                {
                    sumnumber += int.Parse(item.number.ToString());
                    sumprice += float.Parse(item.price.ToString());
                    //生成订单详情
                    T_PurchaseOrders_Details orders_Details = new T_PurchaseOrders_Details()
                    {
                        Purchaseorderid= purchaseOrdersid,
                        product_number= int.Parse(item.number.ToString()),
                        rawmaterial_id=int.Parse(item.id.ToString()),
                        rawmaterial_price= decimal.Parse(item.price.ToString())
                    };
                    db.T_PurchaseOrders_Details.Add(orders_Details);
                    //同步数据库
                    db.SaveChanges();
                }
                //同步主订单
                //T_PurchaseOrders t_PurchaseOrders = db.T_PurchaseOrders.FirstOrDefault(p => p.purchaseorderid== purchaseOrdersid);
                purchaseOrders.purchaseorder_price = (decimal)sumprice;
                purchaseOrders.purchaseorder_numberMax = sumnumber;
                //db.T_PurchaseOrders.Add(t_PurchaseOrders);
                //同步数据库
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}