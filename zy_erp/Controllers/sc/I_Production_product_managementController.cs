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
    /// 生产产品管理
    /// </summary>
    public class I_Production_product_managementController : ApiController
    {

        /// <summary>
        /// 分页查询正在生产的产品
        /// </summary>
        /// <param name="page"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public object Get(int page, int index)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelProduct_PlanPage {page},{index}";
            var data = db.Database.SqlQuery<P_SelProduct_PlanPage_Result>(sel);
            return data.ToList();
        }


        /// <summary>
        /// 生产总单
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.T_Production_Plan.Where(p => p.state != 0).ToList();
            return data.Count();
        }



        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="value"></param>
        public object Get(string keywords)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelPro_PlanKeywords '{keywords}'";
            var data = db.Database.SqlQuery<P_SelPro_PlanKeywords_Result>(sel).Where(p => p.state == 1);
            return data.ToList();
        }


        /// <summary>
        /// 附带（已完成生产）条件模糊查询
        /// </summary>
        /// <param name="value"></param>
        public object Get(string keywords, int dis)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelPro_PlanKeywords '{keywords}'";
            var data = db.Database.SqlQuery<P_SelPro_PlanKeywords_Result>(sel).Where(p => p.state == dis);
            return data.ToList();
        }


        /// <summary>
        /// 附带（已完成生产）条件模糊查询
        /// </summary>
        /// <param name="value"></param>
        public object Post()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.Database.SqlQuery<P_SelPro_PlanState_Result>("exec P_SelPro_PlanState");
            return data.ToList();
        }


        /// <summary>
        /// 用户是否能进行操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_Production_product_management/userIsOperation")]
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
        /// 修改生产产品状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public int Put(dynamic dy)
        {
            //获取用户id
            int userid = 0;
            //获取用户id
            userid = UserPermissions.UserJwt(dy.token.ToString());
            //判断是否非法登录
            if (userid == -1)
            {
                return userid;
            }
            else
            {
                if (UserPermissions.UserIsOperation(userid, 1, dy.type.ToString()))
                {
                    //获取修改状态和需要修改的id
                    int planid = int.Parse(dy.planid.ToString());
                    int state = int.Parse(dy.stateid.ToString());
                    zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                    T_Production_Plan production_Plan = db.T_Production_Plan.FirstOrDefault(p => p.Production_Planid == planid);
                    production_Plan.state = state;
                    if (db.SaveChanges() > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }

        

        [HttpPost]
        [Route("api/I_Production_product_management/InsertCP")]
        /// <summary>
        /// 生产完成入库
        /// </summary>
        /// <param name="dy"></param>
        public int InsertCP(dynamic dy)
        {
            //获取用户id
            int userid = 0;
            //获取用户id
            userid = UserPermissions.UserJwt(dy.token.ToString());
            //判断是否非法登录
            if (userid == -1)
            {
                return userid;
            }
            else
            {
                if (UserPermissions.UserIsOperation(userid, 1, dy.type.ToString())) //权限判断
                {
                    //获取修改状态和需要修改的id
                    int planid = int.Parse(dy.planid.ToString());
                    zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                    T_Production_Plan production_Plan = db.T_Production_Plan.FirstOrDefault(p => p.Production_Planid == planid);
                    production_Plan.state = 5;
                    if (db.SaveChanges() > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}