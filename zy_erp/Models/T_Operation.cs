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
    
    public partial class T_Operation
    {
        public int operationid { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> permissionsid { get; set; }
    
        public virtual T_Permissions T_Permissions { get; set; }
        public virtual T_Users T_Users { get; set; }
    }
}
