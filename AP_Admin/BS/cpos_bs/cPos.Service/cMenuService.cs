using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.Components.SqlMappers;
using System.Collections;
using IBatisNet.DataMapper;

namespace cPos.Service
{
    /// <summary>
    /// 菜单类
    /// </summary>
    public class cMenuService
    {
        /// <summary>
        /// 设置菜单信息
        /// </summary>
        /// <param name="strXML">字符串</param>
        /// <param name="Customer_Id">客户标识1</param>
        /// <returns></returns>
        public bool SetMenuInfo(string strXML,string Customer_Id)
        { 
            bool bReturn = true;
            try
            {
                
                //反序列化
                IList<MenuModel> menuInfoList = (IList<cPos.Model.MenuModel>)cXMLService.Deserialize(strXML,typeof(List<cPos.Model.MenuModel>));
                foreach (MenuModel menuInfo in menuInfoList)
                {
                    menuInfo.customer_id = Customer_Id;
                }
                //转成hash
                var args = new Hashtable
                       {
                           {"MenuModels", menuInfoList}
                       };
                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);

                //提交
                ISqlMapper sqlmap = cSqlMapper.Instance(loggingManager);
                try
                {
                    sqlmap.BeginTransaction();
                    //同步菜单
                    sqlmap.Update("Menu.InsertMenu", args);
                    //同步管理平台下的系统管理员的菜单
                    sqlmap.Update("RoleMenu.UpdateAdminRoleMenu", Customer_Id);
                    sqlmap.CommitTransaction();
                }
                catch (Exception ex)
                {
                    sqlmap.RollBackTransaction();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                throw (ex);
            }
            return bReturn;
        }

        public bool SetMenuInfo(string strXML, string Customer_Id,bool IsTran)
        {
            bool bReturn = true;
            try
            {
                if (strXML.Equals(""))
                {
                    throw new Exception("菜单字符窜为空失败"); 
                }
                if (Customer_Id.Equals(""))
                {
                    throw new Exception("客户标识为空为空失败");
                }
                IList<MenuModel> menuInfoList = new List<MenuModel>();
                //反序列化
                try
                {
                    menuInfoList = (IList<cPos.Model.MenuModel>)cXMLService.Deserialize(strXML, typeof(List<cPos.Model.MenuModel>));
                }
                catch (Exception ex1) { throw (ex1); }
                foreach (MenuModel menuInfo in menuInfoList)
                {
                    menuInfo.customer_id = Customer_Id;
                }
                //转成hash
                var args = new Hashtable
                       {
                           {"MenuModels", menuInfoList}
                       };
                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);

                //提交
                if(IsTran) cSqlMapper.Instance(loggingManager).BeginTransaction();
                try
                {
                    if (IsTran) cSqlMapper.Instance(loggingManager).BeginTransaction();
                    //同步菜单
                    cSqlMapper.Instance(loggingManager).Update("Menu.InsertMenu", args);
                    //同步管理平台下的系统管理员的菜单
                    cSqlMapper.Instance(loggingManager).Update("RoleMenu.UpdateAdminRoleMenu", Customer_Id);

                    if (IsTran) cSqlMapper.Instance(loggingManager).CommitTransaction();
                    return bReturn;
                }
                catch (Exception ex)
                {
                    if (IsTran) cSqlMapper.Instance(loggingManager).RollBackTransaction();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }
    }
}
