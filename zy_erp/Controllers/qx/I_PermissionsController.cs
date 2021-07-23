using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using zy_erp.Models;

namespace zy_erp.Controllers.qx
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class I_PermissionsController : ApiController
    {
        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <returns></returns>
        public object Get(int page, int index)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.Database.SqlQuery<P_SelRolePage_Result>($"exec P_SelRolePage {page},{index}").ToList();
            return data;
        }

        /// <summary>
        /// 查询所有用户数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Get()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            int count = db.T_Users.Count();
            return count;
        }


        /// <summary>
        /// 模糊搜索用户角色
        /// </summary>
        /// <param name="keywords"></param>
        public object Get(string keywords)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            var data = db.Database.SqlQuery<P_SelRoleKeywords_Result>($"exec P_SelRoleKeywords '{keywords}'").ToList();
            return data;
        }


        /// <summary>
        /// 用户是否能进行操作
        /// </summary>
        /// <param name="动态类"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/I_Permissions/userIsOperation")]
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



        [HttpGet]
        [Route("api/I_Permissions/SelRoleName")]
        /// <summary>
        /// 查询所有角色名称
        /// </summary>
        public object SelRoleName()
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            List<string> namearr = new List<string>();
            var data = db.T_Role;
            foreach (var item in data)
            {
                namearr.Add(item.role_name);
            }
            return namearr;
        }



        [HttpPost]
        [Route("api/I_Permissions/PwdValidation")]
        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="dy"></param>
        public bool PwdValidation(dynamic dy)
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
                else
                {
                    //获取登录密码
                    zhongyi_ERPEntities db = new zhongyi_ERPEntities();
                    string userpwd = db.T_Users.FirstOrDefault(p => p.userid == userid).pwd;
                    //获取传输的密码
                    string pwd = dy.pwd.ToString();
                    if (userpwd == pwd)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {

                return false;
            }
        }




        /// <summary>
        /// 修改用户的角色
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
        public bool Put(dynamic dy)
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
                else
                {
                    if (!UserPermissions.UserIsOperation(userid, 2, "insert"))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            int newuserid = int.Parse(dy.userid.ToString());
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //通过选中的角色名获取角色id
            string role_name = dy.rolename.ToString();
            //需要更改的用户新角色id
            int newrole_id = db.T_Role.FirstOrDefault(p => p.role_name == role_name).roleid;
            //查询用户原角色id
            int valrole_id = (int)db.T_User_Role.FirstOrDefault(p => p.userid == newuserid).roleid;
            //获取原用户角色对象
            T_User_Role user_Role = db.T_User_Role.FirstOrDefault(p => p.userid == newuserid);
            user_Role.roleid = newrole_id;
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }





        [HttpGet]
        [Route("api/I_Permissions/RoleInterface")]
        /// <summary>
        /// 根据角色id查询对界面的增删改查信息
        /// </summary>
        /// <returns></returns>
        public object RoleInterface(string rolename)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            string[] menu = new string[] { "基础管理", "库存管理", "采购管理", "生产管理", "销售管理", "订单管理", "审批管理", "权限管理" };
            //根据rolename查询roleid
            int roleid = db.T_Role.FirstOrDefault(p => p.role_name == rolename).roleid;
            Dictionary<string, List<string>> name = new Dictionary<string, List<string>>();
            for (int i = 0; i < menu.Length; i++)
            {
                var data = db.Database.SqlQuery<P_SelRolePermissions_Result>($"exec P_SelRolePermissions {roleid},'{menu[i]}'").ToList();
                List<string> typelist = new List<string>();
                typelist.Add("");
                typelist.Add("");
                typelist.Add("");
                typelist.Add("");
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[j].permissions_type == "insert")
                    {
                        typelist[0] = data[j].permissions_type;
                        continue;
                    }
                    else if (data[j].permissions_type == "delete")
                    {
                        typelist[1] = data[j].permissions_type;
                        continue;
                    }
                    else if (data[j].permissions_type == "update")
                    {
                        typelist[2] = data[j].permissions_type;
                        continue;

                    }
                    else if (data[j].permissions_type == "select")
                    {
                        typelist[3] = data[j].permissions_type;
                        continue;

                    }

                }
                name.Add(menu[i], typelist);
            }

            return name.ToList();

        }


        /// <summary>
        /// 为某个角色新增某个页面的某个权限
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
        public bool Post(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //根据页面查询页面id
            string menuname = dy.menuname.ToString();
            int menuid = db.T_Menu.FirstOrDefault(p => p.menu_name == menuname).menuid;
            //根据权限名称查询权限id
            string permisstype = dy.permisstype.ToString();
            int permissid = db.T_Permissions.FirstOrDefault(p => p.permissions_type == permisstype).permissionsid;
            //根据角色名称查询角色id
            string rolename = dy.rolename.ToString();
            int roleid = db.T_Role.FirstOrDefault(p => p.role_name == rolename).roleid;
            //新增角色权限记录
            T_Role_Permissions_Menu _Role_Permissions_Menu = new T_Role_Permissions_Menu()
            {
                roleid = roleid,
                menuid = menuid,
                permissionsid = permissid
            };
            db.T_Role_Permissions_Menu.Add(_Role_Permissions_Menu);
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
        /// 删除某个角色的某个页面的权限
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
        public bool Delete(dynamic dy)
        {
            zhongyi_ERPEntities db = new zhongyi_ERPEntities();
            //根据页面查询页面id
            string menuname = dy.menuname.ToString();
            int menuid = db.T_Menu.FirstOrDefault(p => p.menu_name == menuname).menuid;
            //根据权限名称查询权限id
            string permisstype = dy.permisstype.ToString();
            int permissid = db.T_Permissions.FirstOrDefault(p => p.permissions_type == permisstype).permissionsid;
            //根据角色名称查询角色id
            string rolename = dy.rolename.ToString();
            int roleid = db.T_Role.FirstOrDefault(p => p.role_name == rolename).roleid;
            T_Role_Permissions_Menu rolemenuper = db.T_Role_Permissions_Menu.FirstOrDefault(p => p.menuid == menuid && p.permissionsid == permissid && p.roleid == roleid);
            if (rolemenuper != null)
            {
                db.T_Role_Permissions_Menu.Remove(rolemenuper);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}