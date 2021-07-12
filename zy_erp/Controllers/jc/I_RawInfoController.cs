using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.jc
{
    public class I_RawInfoController : ApiController
    {
        /// <summary>
        /// 查询原材料表
        /// </summary>
        /// <returns></returns>
        public object Get(int page,int index)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelRawPage {page},{index}";
            var data = db.Database.SqlQuery<P_SelRawPage_Result>(sel);
            return data.ToList();
        }

        /// <summary>
        /// 查询总材料数
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            return db.T_Raw_Material.Count();
        }


        /// <summary>
        /// 根据id查询原材料信息
        /// </summary>
        /// <param name="rawid"></param>
        /// <returns></returns>
        public object Get(int rawid)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.T_Raw_Material.FirstOrDefault(p => p.rawmaterialtid == rawid);
            return data;
        }


        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public object Get(string keywords)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelRawPageKeywords '{keywords}'";
            var data = db.Database.SqlQuery<P_SelRawPageKeywords_Result>(sel);
            return data;
        }
        /// <summary>
        /// 用户是否能进行操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_RawInfo/userIsOperation")]
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
        /// 新增原材料
        /// </summary>
        /// <param name="material"></param>
        public bool Post(T_Raw_Material material)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            db.T_Raw_Material.Add(material);
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
        /// 修改
        /// </summary>
        /// <param name="material"></param>
        public void Put(T_Raw_Material material)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}