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
    /// 供应商页面处理
    /// </summary>
    public class I_SupplieInfoController : ApiController
    {

        /// <summary>
        /// 分页查询所有供应商信息
        /// </summary>
        /// <param name="页码"></param>
        /// <param name="显示行数"></param>
        /// <returns></returns>
        public object Get(int? page, int? index)//页数，显示行数
        {
            using (zhongyi_ERPEntities db = new zhongyi_ERPEntities())
            {
                string sel = $"exec P_SelectSupplierPage {page},{index}";
                var data = db.Database.SqlQuery<P_SelectSupplierPage_Result>(sel);
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
                var data = db.T_Supplier.ToList();
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
                string sel = string.Format($"exec P_selSuplier_keywords '{keywords}'");
                var data = db.Database.SqlQuery<P_selSuplier_keywords_Result>(sel).ToList();
                return data;

            }
        }


        /// <summary>
        /// 根据供应商id查询供应商信息
        /// </summary>
        /// <param name="供应商id"></param>
        /// <returns></returns>
        public object Get(int? supplierid)
        {
            //定义错误信息
            object obj = new
            {
                message = "错误"
            };
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (supplierid == null)
            {

                return obj;
            }
            else
            {
                var data = db.T_Supplier.FirstOrDefault(p => p.supplierid == supplierid);
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
        [Route("api/I_SupplieInfo/userIsOperation")]
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
        /// 用户对供应商数据进行修改
        /// </summary>
        /// <param name="产品表字段"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_SupplieInfo/updated")]
        public bool updated(T_Supplier suppliered)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            if (suppliered == null)
            {
                return false;
            }
            //查询当前产品号
            T_Supplier supplier = db.T_Supplier.FirstOrDefault(p => p.supplierid == suppliered.supplierid);
            //修改当前产品值
            supplier.supplier_name = suppliered.supplier_name;
            supplier.address = suppliered.address;
            supplier.phone = suppliered.phone;
            supplier.email = suppliered.email;
            supplier.bankcard = suppliered.bankcard;
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
        [Route("api/I_SupplieInfo/DelPro")]
        public int DelPro(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //获取传输数值
            int userid = UserPermissions.UserJwt(dy.token.ToString());
            //获取操作类型
            string type = dy.type.ToString();
            //获取id
            int pid = int.Parse(dy.supplier.ToString());
            //判断能否进行删除操作
            if (UserPermissions.UserIsOperation(userid, 1, type))
            {
                try
                {
                    var data = db.T_Supplier.FirstOrDefault(p => p.supplierid == pid);
                    db.T_Supplier.Remove(data);
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
        /// 新增供应商
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
                T_Supplier supplier = new T_Supplier()
                {
                    supplier_name = (dy.suppliers.name).ToString(),
                    phone= (dy.suppliers.lxtype).ToString(),
                    email = (dy.suppliers.email).ToString(),
                    bankcard = (dy.suppliers.backcare).ToString(),
                    address = (dy.suppliers.address).ToString()

                };
                db.T_Supplier.Add(supplier);
                //操作同步至数据库
                if(db.SaveChanges() > 0){
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