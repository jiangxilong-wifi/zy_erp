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
    
    public partial class T_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Role()
        {
            this.T_Role_Permissions_Menu = new HashSet<T_Role_Permissions_Menu>();
            this.T_User_Role = new HashSet<T_User_Role>();
        }
    
        public int roleid { get; set; }
        public string role_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Role_Permissions_Menu> T_Role_Permissions_Menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_User_Role> T_User_Role { get; set; }
    }
}
