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
    /// 客户信息处理
    /// </summary>
    public class I_CustomerInfoController : ApiController
    {
        /// <summary>
        /// 分页查询所有客户信息
        /// </summary>
        /// <param name="页码"></param>
        /// <param name="显示行数"></param>
        /// <returns></returns>
        public object Get(int? page, int? index)//页数，显示行数
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = $"exec P_SelectCustomerPage {page},{index}";
                var data = db.Database.SqlQuery<P_SelectCustomerPage_Result>(sel);
                return data.ToList();

            }
        }

        /// <summary>
        /// 查询总供应商数量
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                var data = db.T_Customer.ToList();
                return data.Count();

            }
        }


        /// <summary>
        /// 供应商表模糊查询
        /// </summary>
        /// <param name="模糊查询的字段"></param>
        /// <returns></returns>
        public object Get(string keywords)
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = string.Format($"exec P_selCustomer_keywords '{keywords}'");
                var data = db.Database.SqlQuery<P_selCustomer_keywords_Result>(sel).ToList();
                return data;

            }
        }


        /// <summary>
        /// 根据供应商id查询供应商信息
        /// </summary>
        /// <param name="供应商id"></param>
        /// <returns></returns>
        public object Get(int? customerid)
        {
            //定义错误信息
            object obj = new
            {
                message = "错误"
            };
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (customerid == null)
            {

                return obj;
            }
            else
            {
                var data = db.T_Customer.FirstOrDefault(p => p.customerid == customerid);
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
        [Route("api/I_CustomerInfo/userIsOperation")]
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
        /// 用户对客户数据进行修改
        /// </summary>
        /// <param name="产品表字段"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_CustomerInfo/updated")]
        public bool updated(T_Customer customers)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (customers == null)
            {
                return false;
            }
            //查询当前产品号
            T_Customer customer = db.T_Customer.FirstOrDefault(p => p.customerid == customers.customerid);
            //修改当前产品值
            customer.customer_name = customers.customer_name;
            customer.address = customers.address;
            customer.phone = customers.phone;
            customer.email = customers.email;
            customer.lx_human = customers.lx_human;
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


        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_CustomerInfo/DelCustomer")]
        public int DelCustomer(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //获取传输数值
            int userid = UserPermissions.UserJwt(dy.token.ToString());
            //获取操作类型
            string type = dy.type.ToString();
            //获取id
            int cid = int.Parse(dy.customerid.ToString());
            //判断能否进行删除操作
            if (UserPermissions.UserIsOperation(userid, 1, type))
            {
                try
                {
                    var data = db.T_Customer.FirstOrDefault(p => p.customerid == cid);
                    db.T_Customer.Remove(data);
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


        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <returns></returns>
        public bool post(dynamic dy)
        {
            //获取用户id
            int userid = UserPermissions.UserJwt(dy.token.ToString());
            //判断是否非法登录
            if (userid == -1)
            {
                return false;
            }
            //获取操作类型
            string type = dy.type.ToString();
            //判断能否进行删除操作
            if (UserPermissions.UserIsOperation(userid, 1, type))
            {
                zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                //新增供应商数据
                T_Customer customer = new T_Customer()
                {
                    customer_name = (dy.customers.customer_name).ToString(),
                    phone = (dy.customers.phone).ToString(),
                    email = (dy.customers.email).ToString(),
                    lx_human = (dy.customers.lx_human).ToString(),
                    address = (dy.customers.address).ToString()

                };
                db.T_Customer.Add(customer);
                //操作同步至数据库
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
                return false;
            }
        }
    }
}