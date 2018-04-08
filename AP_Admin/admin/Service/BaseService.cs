using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Component;
using cPos.Admin.Model.Base;
using IBatisNet.DataMapper;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Service.Implements;

namespace cPos.Admin.Service
{
    public class BaseService
    {
        /// <summary>
        /// 生成GUID(不含-)
        /// </summary>
        /// <returns></returns>
        public string NewGUID()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }

        /// <summary>
        /// 生成当前日期(yyyy-mm-dd)
        /// </summary>
        /// <returns></returns>
        public string GetDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 生成当前日期(yyyymmdd)
        /// </summary>
        /// <returns></returns>
        public string GetDate2(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }

        ///// <summary>
        ///// 生成当前时间(yyyy-mm-dd hh:mi:ss ffffff)
        ///// </summary>
        ///// <returns></returns>
        //public string GetDateTime(DateTime dt)
        //{
        //    return dt.ToString("yyyy-MM-dd HH:mm:ss ffffff");
        //}

        #region GetNow
        public static string GetNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetNowWithMillisecond()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion

        /// <summary>
        /// 根据客户ID加密数据
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="input">明文</param>
        /// <returns>密文</returns>
        public string EncryptStringByCustomer(string customerID, string input)
        {
            string keyFile = Component.SqlMappers.MSSqlMapper.Instance().QueryForObject<string>("Customer.Connect.SelectDataKeyByID", customerID);
            return EncryptStringByKeyFile(keyFile, input);
        }

        /// <summary>
        /// 根据密钥文件加密数据
        /// </summary>
        /// <param name="keyFile">密钥文件</param>
        /// <param name="input">明文</param>
        /// <returns>密文</returns>
        public string EncryptStringByKeyFile(string keyFile, string input)
        {
#if DATA_ENCRYPT
            string output = Component.CryptManager.EncryptString(keyFile, input);
            return output; 
#else
            return input;
#endif
        }

        /// <summary>
        /// 根据客户ID解密数据
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="input">密文</param>
        /// <returns>明文</returns>
        public string DecryptStringByCustomer(string customerID, string input)
        {
            string keyFile = Component.SqlMappers.MSSqlMapper.Instance().QueryForObject<string>("Customer.Connect.SelectDataKeyByID", customerID);
            return DecryptStringByKeyFile(keyFile, input);
        }

        /// <summary>
        /// 根据密钥文件解密数据
        /// </summary>
        /// <param name="keyFile">密钥文件</param>
        /// <param name="input">密文</param>
        /// <returns>明文</returns>
        public string DecryptStringByKeyFile(string keyFile, string input)
        {
#if DATA_ENCRYPT
            string output = Component.CryptManager.DecryptString(keyFile, input);
            return output;
#else
            return input;
#endif
        }

        /// <summary>
        /// 插入一个表单的操作历史(不含流程)
        /// </summary>
        /// <param name="loggingSession">当前登录用户的信息</param>
        /// <param name="billKindCode">表单类型编码</param>
        /// <param name="billID">表单ID</param>
        /// <param name="actionFlagType">表单操作的标志的类型</param>
        /// <param name="actionFlagValue">表单操作的标志的值</param>
        /// <param name="comment">备注</param>
        protected void InsertBillActionLogWithoutFlow(LoggingSessionInfo loggingSession, string billKindCode, string billID,
            Model.Bill.BillActionFlagType actionFlagType, int actionFlagValue, string comment)
        {
            Hashtable ht = new Hashtable();
            ht.Add("BillID", billID);
            ht.Add("Comment", comment);
            ht.Add("BillActionFlagType", (int)actionFlagType);
            ht.Add("BillActionFlagValue", actionFlagValue);
            if (loggingSession == null)
            {
                ht.Add("UserID", "--");
            }
            else
            {
                ht.Add("UserID", loggingSession.UserID);
            }
            ht.Add("BillKindCode", billKindCode);

            Component.SqlMappers.MSSqlMapper.Instance().Insert("Bill.ActionLog.InsertBillActionLogWithoutFlow", ht);
        }

        /// <summary>
        /// 根据字典的编码，查询字典的明细列表
        /// </summary>
        /// <param name="code">字典的编码</param>
        /// <returns></returns>
        public IList<DictionaryDetailInfo> SelectDictionaryDetailListByDictionaryCode(string code)
        {
            return Component.SqlMappers.MSSqlMapper.Instance().QueryForList<DictionaryDetailInfo>("Base.DictionaryDetail.SelectByDictionaryCode", code);
        }


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
        public string GetNo(cPos.Model.LoggingSessionInfo loggingSessionInfo, string prefix)
        {
            Int64 val = MSSqlMapper.Instance().QueryForObject<Int64>("SysParameter.GetNo", null);
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
        public cPos.Model.LoggingSessionInfo GetLoggingSessionInfoByCustomerId(string Customer_Id)
        {
            var loggingSessionInfo = new cPos.Model.LoggingSessionInfo();

            //取连接
            var service = new CustomerService();
            var cConnect = service.GetCustomerConnectByID(Customer_Id);
            if (cConnect == null)
            {
                return null;
                //this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "CustomerConnect", "未找到");
                //this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
                //return "";
            }

            //生成XML
            //var user = new cPos.Model.LoggingSessionInfo();
            //user.CurrentLoggingManager.Customer_Id = Customer_Id;
            //user.CustomerCode = cConnect.Customer.Code;
            //user.CustomerName = cConnect.Customer.Name;
            //user.ConnectionString = cConnect.DBConnectionString;
            //string s = XMLGenerator.Serialize(user);

            //LoggingManager loggingManagerInfo = new LoggingManager();
            //获取数据库连接字符串
            //cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
            //AuthWebService.Url = System.Configuration.ConfigurationManager.AppSettings["sso_url"] + "/authservice.asmx";
            //this.Log(LogLevel.DEBUG, "BS", "", "", "url", AuthWebService.Url);
            //this.Log(LogLevel.DEBUG, "BS", "", "", "customer_id", Customer_Id);
            //string str = AuthWebService.GetCustomerDBConnectionString(Customer_Id);//"0b3b4d8b8caa4c71a7c201f53699afcc"

            //loggingManagerInfo = (LoggingManager)cXMLService.Deserialize(s, typeof(cPos.Model.LoggingManager));

            var user = new cPos.Model.LoggingManager();
            user.Customer_Id = Customer_Id;
            user.Customer_Code = cConnect.Customer.Code;
            user.Customer_Name = cConnect.Customer.Name;
            user.Connection_String = cConnect.DBConnectionString;

            loggingSessionInfo.CurrentLoggingManager = user;
            //loggingManagerInfo.Customer_Id = Customer_Id;
            //loggingSessionInfo.CurrentLoggingManager.Connection_String = cConnect.DBConnectionString;
            return loggingSessionInfo;
        }

        #endregion

        /// <summary>
        /// 分离当前角色ID(角色Id,单位Id)
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <returns></returns>
        protected string GetBasicRoleId(cPos.Model.LoggingSessionInfo loggingSession)
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
        protected string GenInsertUnitTemporaryTableSQL(cPos.Model.LoggingSessionInfo loggingSession,
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
        protected void InsertUnitTemporaryTable(ISqlMapper sqlMap, cPos.Model.LoggingSessionInfo loggingSession,
            cPos.Model.Unit.UnitQueryCondition condition)
        {
            sqlMap.QueryForObject("Pos.Operate.InsertUnitTemporaryTable", this.GenInsertUnitTemporaryTableSQL(loggingSession, condition));
        }

        ///// <summary>
        ///// 记录日志信息
        ///// </summary>
        ///// <param name="level">日志级别</param>
        ///// <param name="systemName">系统名称</param>
        ///// <param name="moduleName">模块名称</param>
        ///// <param name="functionName">方法名称</param>
        ///// <param name="messageType">信息类型</param>
        ///// <param name="message">信息</param>
        //public void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message)
        //{
        //    FileLogService log_service = new FileLogService();
        //    log_service.Log(level, systemName, moduleName, functionName, messageType, message);
        //}
    }
}
