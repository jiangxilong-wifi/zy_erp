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
    /// 采购统计
    /// </summary>
    public class I_ProcurementStatisticsController : ApiController
    {
        /// <summary>
        /// 查询采购统计数据
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string sel = $"exec P_SelRawmaterialAndCount {6}";
            var data = db.Database.SqlQuery<P_SelRawmaterialAndCount_Result>(sel);
            return data.ToList();
        }


        //public o
        
    }
}