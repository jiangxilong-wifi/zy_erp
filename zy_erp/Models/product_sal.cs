using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zy_erp.Models
{
    public class product_sal
    {
        //商品名
        public string pro_name { get; set; }
        //上月销量
        public int old_sal { get; set; }
        //本月销量
        public int new_sal { get; set; }
    }
}