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
    
    public partial class T_PurchaseOrders_Details
    {
        public int detailsid { get; set; }
        public Nullable<int> Purchaseorderid { get; set; }
        public Nullable<int> product_number { get; set; }
        public Nullable<int> rawmaterial_id { get; set; }
        public Nullable<decimal> rawmaterial_price { get; set; }
    
        public virtual T_Raw_Material T_Raw_Material { get; set; }
        public virtual T_PurchaseOrders T_PurchaseOrders { get; set; }
    }
}
