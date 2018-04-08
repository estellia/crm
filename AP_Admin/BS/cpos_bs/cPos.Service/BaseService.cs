using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.Components.SqlMappers;
using IBatisNet.DataMapper;

namespace cPos.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// 分离角色ID(角色Id,单位Id)
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        protected string GetBasicRoleId(string roleID)
        {
            string[] arr_role = roleID.Split(new char[] { ',' });
            return arr_role[0];
        }

         /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDateTime()
        {
            return GetDateTime(DateTime.Now);
        }
        public DateTime GetCurrentDateTime2()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取单据号
        /// </summary>
        /// <param name="loggingSessionInfo">model</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public string GetNo(LoggingSessionInfo loggingSessionInfo,string prefix)
        {
            Int64 val = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<Int64>("SysParameter.GetNo", null);
            return prefix + val.ToString().PadLeft(8, '0');
        }

        /// <summary>
        /// 生成guid
        /// </summary>
        /// <returns></returns>
        protected string NewGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }

        #region 根据Customer_Id拼接成登录类
        /// <summary>
        /// 根据Customer_Id获取登录Model
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public LoggingSessionInfo GetLoggingSessionInfoByCustomerId(string Customer_Id)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            LoggingManager loggingManagerInfo = new LoggingManager();
            //获取数据库连接字符串
            cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
            AuthWebService.Url = System.Configuration.ConfigurationManager.AppSettings["sso_url"] + "/authservice.asmx";
            this.Log(LogLevel.DEBUG, "BS", "", "", "url", AuthWebService.Url);
            this.Log(LogLevel.DEBUG, "BS", "", "", "customer_id", Customer_Id);
            string str = AuthWebService.GetCustomerDBConnectionString(Customer_Id);//"0b3b4d8b8caa4c71a7c201f53699afcc"

            loggingManagerInfo = (cPos.Model.LoggingManager)cXMLService.Deserialize(str, typeof(cPos.Model.LoggingManager));

 
            loggingManagerInfo.Customer_Id = Customer_Id;
            loggingSessionInfo.CurrentLoggingManager = loggingManagerInfo;
            return loggingSessionInfo;
        }

        #endregion

        /// <summary>
        /// 分离当前角色ID(角色Id,单位Id)
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <returns></returns>
        protected string GetBasicRoleId(LoggingSessionInfo loggingSession)
        {
            string[] arr_role = loggingSession.CurrentUserRole.RoleId.Split(new char[] { ',' });
            return arr_role[0];
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        protected string GenInsertUnitTemporaryTableSQL(LoggingSessionInfo loggingSession,
            cPos.Model.Unit.UnitQueryCondition condition)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("declare @tmp_unit table(unit_id varchar(32));");
            sb.AppendLine("insert into @tmp_unit");
            //表
            //sb.AppendLine("select a2.unit_id from t_unit_level a1, t_unit_level a2 ");
            //视图
            sb.AppendLine("select a2.unit_id from vw_unit_level a1, vw_unit_level a2 ");
            if (condition.SuperUnitIDs.Count == 0)
            {
                sb.Append(",(select distinct unit_id from t_user_role where ");
                sb.Append(string.Format(" user_id='{0}' ", loggingSession.CurrentUser.User_Id));
                sb.AppendLine(string.Format(" and role_id='{0}') a3 ", this.GetBasicRoleId(loggingSession.CurrentUserRole.RoleId)));
                sb.AppendLine("where a3.unit_id=a1.unit_id ");
            }
            else
            {
                sb.Append(string.Format("where (a1.unit_id='{0}' ", condition.SuperUnitIDs[0]));
                for (int i = 1; i < condition.SuperUnitIDs.Count; i++)
                {
                    sb.Append(string.Format("or a1.unit_id='{0}' ", condition.SuperUnitIDs[i]));
                }
                sb.AppendLine(")");

            }
            //表
            //sb.AppendLine(" and a2.path_unit_no like a1.path_unit_no + '%' ");
            //视图
            sb.AppendLine(" and a2.path_unit_id like a1.path_unit_id + '%' ");
            sb.AppendLine("group by a2.unit_id; ");

            return sb.ToString();
        }

        /// <summary>
        /// 根据单位的查询条件，将符合条件的单位的ID插入到临时表中
        /// </summary>
        /// <param name="sqlMap">数据库连接</param>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">单位的查询条件</param>
        /// <returns></returns>
        protected void InsertUnitTemporaryTable(ISqlMapper sqlMap, LoggingSessionInfo loggingSession, 
            cPos.Model.Unit.UnitQueryCondition condition)
        {
            sqlMap.QueryForObject("Pos.Operate.InsertUnitTemporaryTable", this.GenInsertUnitTemporaryTableSQL(loggingSession, condition));
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="functionName">方法名称</param>
        /// <param name="messageType">信息类型</param>
        /// <param name="message">信息</param>
        protected void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message)
        {
            FileLogService log_service = new FileLogService();
            log_service.Log(level, systemName, moduleName, functionName, messageType, message);
        }

    }
}
