//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace zy_erp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Product_output
    {
        public int outputid { get; set; }
        public Nullable<int> productid { get; set; }
        public Nullable<int> product_inventory { get; set; }
        public Nullable<System.DateTime> product_date { get; set; }
        public string state { get; set; }
    
        public virtual T_Product T_Product { get; set; }
    }
}