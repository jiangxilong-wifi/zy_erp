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
    
    public partial class T_SalesOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_SalesOrders()
        {
            this.T_SalesOrders_Details = new HashSet<T_SalesOrders_Details>();
        }
    
        public int salesorderid { get; set; }
        public Nullable<int> customerid { get; set; }
        public Nullable<decimal> salesorder_price { get; set; }
        public string salesorder__number { get; set; }
        public Nullable<int> state { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual T_Customer T_Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SalesOrders_Details> T_SalesOrders_Details { get; set; }
    }
}
