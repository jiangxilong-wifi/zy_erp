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
    
    public partial class T_User_Role
    {
        public Nullable<int> roleid { get; set; }
        public Nullable<int> userid { get; set; }
        public int recordid { get; set; }
    
        public virtual T_Role T_Role { get; set; }
        public virtual T_Users T_Users { get; set; }
    }
}
