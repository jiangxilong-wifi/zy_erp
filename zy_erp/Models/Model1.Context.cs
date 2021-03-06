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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class zhongyi_ERPEntities : DbContext
    {
        public zhongyi_ERPEntities()
            : base("name=zhongyi_ERPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_Customer> T_Customer { get; set; }
        public virtual DbSet<T_Menu> T_Menu { get; set; }
        public virtual DbSet<T_Operation> T_Operation { get; set; }
        public virtual DbSet<T_Permissions> T_Permissions { get; set; }
        public virtual DbSet<T_Product> T_Product { get; set; }
        public virtual DbSet<T_PurchaseOrders_Details> T_PurchaseOrders_Details { get; set; }
        public virtual DbSet<T_Role> T_Role { get; set; }
        public virtual DbSet<T_Role_Permissions_Menu> T_Role_Permissions_Menu { get; set; }
        public virtual DbSet<T_SalesOrders> T_SalesOrders { get; set; }
        public virtual DbSet<T_SalesOrders_Details> T_SalesOrders_Details { get; set; }
        public virtual DbSet<T_Supplier> T_Supplier { get; set; }
        public virtual DbSet<T_User_Role> T_User_Role { get; set; }
        public virtual DbSet<T_Users> T_Users { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<T_Product_output> T_Product_output { get; set; }
        public virtual DbSet<T_Product_output_Details> T_Product_output_Details { get; set; }
        public virtual DbSet<T_Raw_Material_Inventory> T_Raw_Material_Inventory { get; set; }
        public virtual DbSet<T_Raw_Material> T_Raw_Material { get; set; }
        public virtual DbSet<T_Raw_Material_output> T_Raw_Material_output { get; set; }
        public virtual DbSet<T_Raw_Material_output_Details> T_Raw_Material_output_Details { get; set; }
        public virtual DbSet<T_PurchaseOrders> T_PurchaseOrders { get; set; }
        public virtual DbSet<T_Product_Inventory> T_Product_Inventory { get; set; }
        public virtual DbSet<T_Production_Plan> T_Production_Plan { get; set; }
    
        public virtual ObjectResult<P_Sel_Customer_OrdersDetails_Result> P_Sel_Customer_OrdersDetails(Nullable<int> customerid, Nullable<int> salesorderid)
        {
            var customeridParameter = customerid.HasValue ?
                new ObjectParameter("customerid", customerid) :
                new ObjectParameter("customerid", typeof(int));
    
            var salesorderidParameter = salesorderid.HasValue ?
                new ObjectParameter("salesorderid", salesorderid) :
                new ObjectParameter("salesorderid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Sel_Customer_OrdersDetails_Result>("P_Sel_Customer_OrdersDetails", customeridParameter, salesorderidParameter);
        }
    
        public virtual ObjectResult<P_sel_keywords_Result> P_sel_keywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_sel_keywords_Result>("P_sel_keywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_Sel_product_sales_top_Result> P_Sel_product_sales_top(Nullable<int> tops, Nullable<System.DateTime> olddate, Nullable<System.DateTime> newdate)
        {
            var topsParameter = tops.HasValue ?
                new ObjectParameter("tops", tops) :
                new ObjectParameter("tops", typeof(int));
    
            var olddateParameter = olddate.HasValue ?
                new ObjectParameter("olddate", olddate) :
                new ObjectParameter("olddate", typeof(System.DateTime));
    
            var newdateParameter = newdate.HasValue ?
                new ObjectParameter("newdate", newdate) :
                new ObjectParameter("newdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Sel_product_sales_top_Result>("P_Sel_product_sales_top", topsParameter, olddateParameter, newdateParameter);
        }
    
        public virtual ObjectResult<P_selCustomer_keywords_Result> P_selCustomer_keywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_selCustomer_keywords_Result>("P_selCustomer_keywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelectCustomerPage_Result> P_SelectCustomerPage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelectCustomerPage_Result>("P_SelectCustomerPage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelectProductPage_Result> P_SelectProductPage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelectProductPage_Result>("P_SelectProductPage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelectSupplierPage_Result> P_SelectSupplierPage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelectSupplierPage_Result>("P_SelectSupplierPage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_selSuplier_keywords_Result> P_selSuplier_keywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_selSuplier_keywords_Result>("P_selSuplier_keywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_user_menu_Permissions_Result> P_user_menu_Permissions(Nullable<int> userid, Nullable<int> menu)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var menuParameter = menu.HasValue ?
                new ObjectParameter("menu", menu) :
                new ObjectParameter("menu", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_user_menu_Permissions_Result>("P_user_menu_Permissions", useridParameter, menuParameter);
        }
    
        public virtual ObjectResult<P_userSelMenu_Result> P_userSelMenu(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_userSelMenu_Result>("P_userSelMenu", useridParameter);
        }
    
        public virtual ObjectResult<P_SelectPurchaseOrdersPage_Result> P_SelectPurchaseOrdersPage(Nullable<int> pagesize, Nullable<int> pageindex, Nullable<int> ok)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            var okParameter = ok.HasValue ?
                new ObjectParameter("ok", ok) :
                new ObjectParameter("ok", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelectPurchaseOrdersPage_Result>("P_SelectPurchaseOrdersPage", pagesizeParameter, pageindexParameter, okParameter);
        }
    
        public virtual ObjectResult<P_Sel_RawMaterial_Inventory_pages_Result> P_Sel_RawMaterial_Inventory_pages(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Sel_RawMaterial_Inventory_pages_Result>("P_Sel_RawMaterial_Inventory_pages", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelRaw_Material_keywords_Result> P_SelRaw_Material_keywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRaw_Material_keywords_Result>("P_SelRaw_Material_keywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelRawmaterialAndCount_Result> P_SelRawmaterialAndCount(Nullable<int> index)
        {
            var indexParameter = index.HasValue ?
                new ObjectParameter("index", index) :
                new ObjectParameter("index", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRawmaterialAndCount_Result>("P_SelRawmaterialAndCount", indexParameter);
        }
    
        public virtual ObjectResult<P_SelProductInventory_Result> P_SelProductInventory(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelProductInventory_Result>("P_SelProductInventory", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelProductPlan_Result> P_SelProductPlan(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelProductPlan_Result>("P_SelProductPlan", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelProductPlanKeywords_Result> P_SelProductPlanKeywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelProductPlanKeywords_Result>("P_SelProductPlanKeywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelRawPage_Result> P_SelRawPage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRawPage_Result>("P_SelRawPage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelRawPageKeywords_Result> P_SelRawPageKeywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRawPageKeywords_Result>("P_SelRawPageKeywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelPlannumber_Result> P_SelPlannumber()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelPlannumber_Result>("P_SelPlannumber");
        }
    
        public virtual ObjectResult<P_SelPlannumberKeywored_Result> P_SelPlannumberKeywored(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelPlannumberKeywored_Result>("P_SelPlannumberKeywored", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelRawPlanKeywords_Result> P_SelRawPlanKeywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRawPlanKeywords_Result>("P_SelRawPlanKeywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelProduct_PlanPage_Result> P_SelProduct_PlanPage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelProduct_PlanPage_Result>("P_SelProduct_PlanPage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelPro_PlanKeywords_Result> P_SelPro_PlanKeywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelPro_PlanKeywords_Result>("P_SelPro_PlanKeywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelPro_PlanState_Result> P_SelPro_PlanState()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelPro_PlanState_Result>("P_SelPro_PlanState");
        }
    
        public virtual ObjectResult<P_SelRolePage_Result> P_SelRolePage(Nullable<int> pagesize, Nullable<int> pageindex)
        {
            var pagesizeParameter = pagesize.HasValue ?
                new ObjectParameter("pagesize", pagesize) :
                new ObjectParameter("pagesize", typeof(int));
    
            var pageindexParameter = pageindex.HasValue ?
                new ObjectParameter("pageindex", pageindex) :
                new ObjectParameter("pageindex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRolePage_Result>("P_SelRolePage", pagesizeParameter, pageindexParameter);
        }
    
        public virtual ObjectResult<P_SelRoleKeywords_Result> P_SelRoleKeywords(string keywords)
        {
            var keywordsParameter = keywords != null ?
                new ObjectParameter("keywords", keywords) :
                new ObjectParameter("keywords", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRoleKeywords_Result>("P_SelRoleKeywords", keywordsParameter);
        }
    
        public virtual ObjectResult<P_SelRolePermissions_Result> P_SelRolePermissions(Nullable<int> roleid, string munname)
        {
            var roleidParameter = roleid.HasValue ?
                new ObjectParameter("roleid", roleid) :
                new ObjectParameter("roleid", typeof(int));
    
            var munnameParameter = munname != null ?
                new ObjectParameter("munname", munname) :
                new ObjectParameter("munname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_SelRolePermissions_Result>("P_SelRolePermissions", roleidParameter, munnameParameter);
        }
    }
}
