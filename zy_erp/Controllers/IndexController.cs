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
    /// 主页首页导航的http请求
    /// </summary>
    public class IndexController : ApiController
    {
        [Route("api/Index/UserInfo")]
        //进入个人中心
        public IEnumerable<string> UserInfo()
        {
            return new string[] { "value1", "value2" };
        }

        public bool Post(dynamic dy)
        {
            int userid=UserPermissions.UserJwt(dy.token.ToString());
            if (userid == -1)
            {
                //非法访问
                return false;
            }
            else
            {
                zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                var data = db.T_Users.FirstOrDefault(p => p.userid == userid);
                //获取传入的原密码
                string oldpwd = dy.oldpwd.ToString();
                //获取传入的新密码
                string newpwd = dy.newpwd.ToString();
                if (oldpwd==data.pwd) {
                    data.pwd = newpwd;
                    T_Users user = data;
                    //将修改同步到数据库
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //原密码不正确
                    return false;
                }
            }
        }
        // 查询销量靠前的产品（折线图）
        public object Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //本月日期
            string date = DateTime.Now.ToString();
            string sel = $"exec P_Sel_product_sales_top {10},'{date}'";
            var data = db.Database.SqlQuery<P_Sel_product_sales_top_Result>(sel).ToList();
            return data;
        }


        //不同产品本月和上月不同销量对比图
        public object Get(int index)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //上个月月初
            DateTime old_chu= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1);
            //上个月月末
            DateTime old_mo = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-30")).AddMonths(-1);
            //本月初
            DateTime new_chu = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            //本月末(截至日前)
            DateTime new_mo = DateTime.Now;
            //本月销量(本月初-目前时间)
            string sel = $"exec P_Sel_product_sales_top {5},'{new_chu}','{new_mo}'";
            var new_data = db.Database.SqlQuery<P_Sel_product_sales_top_Result>(sel).ToList();
            //上月销量
            string sel2 = $"exec P_Sel_product_sales_top {5},'{old_chu}','{old_mo}'";
            var old_data = db.Database.SqlQuery<P_Sel_product_sales_top_Result>(sel).ToList();
            List<product_sal> objlist = new List<product_sal>();
            //存储上月销量靠前的产品
            foreach (var item in old_data)
            {
                foreach (var item2 in new_data)
                {
                    //本月与上月均有该商品出售
                    if (item.product_name.Equals(item2.product_name))
                    {
                        product_sal sal = new product_sal();
                        sal.pro_name = item.product_name;//商品名
                        sal.old_sal = (int)item.xiaoliang;//上月销量
                        sal.new_sal = (int)item2.xiaoliang;//本月销量
                        objlist.Add(sal);
                    }
                }
                
            }
            return objlist;
        }
        
    }
}