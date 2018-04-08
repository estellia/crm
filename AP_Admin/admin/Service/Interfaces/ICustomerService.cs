using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Component;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Model.Customer;

namespace cPos.Admin.Service.Interfaces
{
    /// <summary>
    /// 客户相关接口
    /// </summary>
    public interface ICustomerService
    {
        #region 用户
        /// <summary>
        /// 获取满足查询条件的用户的记录总数
        /// </summary>
        /// <param name="condition">CustomerID：客户ID；CustomerName：客户；CUAccount：登录帐号；CUName：姓名；CUStatus：状态；</param>
        /// <returns></returns>
        int SelectUserListCount(Hashtable condition);
        /// <summary>
        /// 获取满足查询条件的用户的某页上的所有用户
        /// </summary>
        /// <param name="condition">CustomerID：客户ID；CustomerName：客户；CUAccount：登录帐号；CUName：姓名；CUStatus：状态；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的用户的列表</returns>
        IList<CustomerUserInfo> SelectUserList(Hashtable condition, int maxRowCount, int startRowIndex);

        /// <summary>
        /// 获取单个用户的信息
        /// </summary>
        /// <param name="cuID">要获取信息的用户ID</param>
        /// <returns>用户的详细信息</returns>
        CustomerUserInfo GetCustomerUserByID(string cuID);

        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="customerCode">客户编码</param>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ht">验证通过后返回的数据(Token:token;CustomerID:客户的ID;AccessURL:客户的访问地址)</param>
        /// <param name="customerURL">验证通过后的</param>
        /// <returns>-1:用户不存在;-2:用户被停用;-3:密码不正确;-4:离线;-5:超过使用期限;-11:客户不存在;-12:客户被停用;1:成功;</returns>
        int ValidateUser(string customerCode, string account, string password, out Hashtable ht);
        
        /// <summary>
        /// 根据token获取登录用户的ID
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>登录用户的ID</returns>
        string GetLoginCustomerUserIDByToken(string token);

        /// <summary>
        /// 插入一个用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool InsertCustomerUserFromCP(Model.Customer.CustomerUserInfo user);

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool ModifyCustomerUserFromCP(Model.Customer.CustomerUserInfo user);

        /// <summary>
        /// 启用一个用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool EnableCustomerUserFromCP(Model.Customer.CustomerUserInfo user);

        /// <summary>
        /// 停用一个用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool DisableCustomerUserFromCP(Model.Customer.CustomerUserInfo user);

        /// <summary>
        /// 修改一个用户的密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        bool ModifyCustomerUserPasswordFromCP(Model.Customer.CustomerUserInfo user);
        #endregion

        #region 客户
        /// <summary>
        /// 获取满足查询条件的客户的记录总数
        /// </summary>
        /// <param name="condition">Code:编码;Name:名称;Contacter:联系人;Status:状态;</param>
        /// <returns></returns>
        int SelectCustomerListCount(Hashtable condition);
        /// <summary>
        /// 获取满足查询条件的客户的某页上的所有客户
        /// </summary>
        /// <param name="condition">Code:编码;Name:名称;Contacter:联系人;Status:状态;</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的客户的列表</returns>
        IList<CustomerInfo> SelectCustomerList(Hashtable condition, int maxRowCount, int startRowIndex);

        /// <summary>
        /// 查询所有的客户列表
        /// </summary>
        /// <param name="condition">Status:状态;</param>
        /// <returns></returns>
        IList<CustomerInfo> GetAllCustomerList(Hashtable condition);

        /// <summary>
        /// 根据客户ID获取单个客户的信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="getConnectInfo">是否需要查询客户下的连接信息</param>
        /// <param name="getMenuListInfo">是否需要查询客户下的可操作菜单列表的信息</param>
        /// <returns></returns>
        CustomerInfo GetCustomerByID(string customerID, bool getConnectInfo, bool getMenuListInfo);

        /// <summary>
        /// 检查客户编码是否已经存在
        /// </summary>
        /// <param name="customerID">如果是校验一个已经存在的客户,则传入该客户的ID,否则为空</param>
        /// <param name="customerCode">客户编码</param>
        /// <returns></returns>
        bool ExistCustomerCode(string customerID, string customerCode);

        /// <summary>
        /// 添加一个客户
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="customer">客户信息</param>
        void InsertCustomer(LoggingSessionInfo loggingSession, CustomerInfo customer);

        /// <summary>
        /// 修改一个客户
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="customer">客户信息</param>
        void ModifyCustomer(LoggingSessionInfo loggingSession, CustomerInfo customer);

        /// <summary>
        /// 是否能够在某个客户下新创建一个用户
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool CanCreateUser(string customerID);

        /// <summary>
        /// 是否能够在某个客户下新创建一个终端
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool CanCreateTerminal(string customerID);

        /// <summary>
        /// 是否能够在某个客户下新创建一个门店
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool CanCreateShop(string customerID);
		///// <summary>
		///// 新建商户的限制门店数
		///// </summary>
		///// <param name="customerID"></param>
		///// <returns></returns>
		//bool CreateCustomerLimiUnit(string customerID);
		///// <summary>
		///// 更新商户的限制门店数
		///// </summary>
		///// <param name="customerID"></param>
		///// <returns></returns>
		//bool UpdateCustomerLimiUnit(string customerID);
		#endregion

		#region 终端
		/// <summary>
		/// 获取满足查询条件的终端的记录总数
		/// </summary>
		/// <param name="condition">CustomerID：客户ID；CustomerName：客户；CTHoldType:终端持有方式；CTType：终端类型；CTCode：终端编码；CTsn：终端序列号；CTSoftwareVersion：软件版本；CTPurchaseDateBegin：起始购买日期；CTPurchaseDateEnd：终止购买日期；CTInsuranceDateBegin：起始出保日期；CTInsuranceDateBegin：起始出保日期；</param>
		/// <returns></returns>
		int SelectTerminalListCount(Hashtable condition);
        /// <summary>
        /// 获取满足查询条件的终端的某页上的所有终端
        /// </summary>
        /// <param name="condition">CustomerID：客户ID；CustomerName：客户；CTHoldType:终端持有方式；CTType：终端类型；CTCode：终端编码；CTsn：终端序列号；CTSoftwareVersion：软件版本；CTPurchaseDateBegin：起始购买日期；CTPurchaseDateEnd：终止购买日期；CTInsuranceDateBegin：起始出保日期；CTInsuranceDateBegin：起始出保日期；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的门店的列表</returns>
        IList<CustomerTerminalInfo> SelectTerminalList(Hashtable condition, int maxRowCount, int startRowIndex);

        /// <summary>
        /// 获取单个终端的信息
        /// </summary>
        /// <param name="ctID">要获取信息的终端ID</param>
        /// <returns>用户的详细信息</returns>
        CustomerTerminalInfo GetCustomerTerminalByID(string ctID);

        /// <summary>
        /// 检查终端编码是否已经存在
        /// </summary>
        /// <param name="customerTerminalID">如果是校验一个已经存在的终端,则传入该终端的ID,否则为空</param>
        /// <param name="customerTerminalCode">终端编码</param>
        /// <returns></returns>
        bool ExistCustomerTerminalCode(string terminalID, string terminalCode);

        /// <summary>
        /// 插入一个终端
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="terminal">终端信息</param>
        /// <returns></returns>
        bool InsertCustomerTerminal(LoggingSessionInfo loggingSession, Model.Customer.CustomerTerminalInfo terminal);

        /// <summary>
        /// 更新一个终端
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="terminal">终端信息</param>
        /// <returns></returns>
        bool ModifyCustomerTerminal(LoggingSessionInfo loggingSession, Model.Customer.CustomerTerminalInfo terminal);

        /// <summary>
        /// 插入一个终端
        /// </summary>
        /// <param name="terminal">终端信息</param>
        /// <returns></returns>
        bool InsertCustomerTerminalFromCP(Model.Customer.CustomerTerminalInfo terminal);

        /// <summary>
        /// 更新一个终端
        /// </summary>
        /// <param name="terminal">终端信息</param>
        /// <returns></returns>
        bool ModifyCustomerTerminalFromCP(Model.Customer.CustomerTerminalInfo terminal);
        #endregion


        #region 门店
        /// <summary>
        /// 获取满足查询条件的门店的记录总数
        /// </summary>
        /// <param name="condition">CustomerID：客户ID；CustomerName：客户；CSCode：门店编码；CSName：门店名称；CSStatus：状态；CSContact：联系人；CSTel：电话；</param>
        /// <returns></returns>
        int SelectShopListCount(Hashtable condition);
        /// <summary>
        /// 获取满足查询条件的门店的某页上的所有门店
        /// </summary>
        /// <param name="condition">CustomerID：客户ID；CustomerName：客户；CSCode：门店编码；CSName：门店名称；CSStatus：状态；CSContact：联系人；CSTel：电话；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的门店的列表</returns>
        IList<CustomerShopInfo> SelectShopList(Hashtable condition, int maxRowCount, int startRowIndex);

        /// <summary>
        /// 查询某个客户下的所有门店列表
        /// </summary>
        /// <param name="condition">CustomerID:客户ID;ShopStatus:门店状态</param>
        /// <returns></returns>
        IList<CustomerShopInfo> GetAllShopList(Hashtable condition);

        /// <summary>
        /// 获取单个门店的信息
        /// </summary>
        /// <param name="csID">要获取信息的门店ID</param>
        /// <returns>用户的详细信息</returns>
        CustomerShopInfo GetCustomerShopByID(string csID);

        /// <summary>
        /// 插入一个门店
        /// </summary>
        /// <param name="shop">门店信息</param>
        /// <returns></returns>
        bool InsertCustomerShopFromCP(Model.Customer.CustomerShopInfo shop);

        /// <summary>
        /// 更新一个门店
        /// </summary>
        /// <param name="shop">门店信息</param>
        /// <returns></returns>
        bool ModifyCustomerShopFromCP(Model.Customer.CustomerShopInfo shop);

        /// <summary>
        /// 启用一个门店
        /// </summary>
        /// <param name="shop">门店信息</param>
        /// <returns></returns>
        bool EnableCustomerShopFromCP(Model.Customer.CustomerShopInfo shop);

        /// <summary>
        /// 停用一个门店
        /// </summary>
        /// <param name="shop">门店信息</param>
        /// <returns></returns>
        bool DisableCustomerShopFromCP(Model.Customer.CustomerShopInfo shop);
        #endregion

        #region 连接

        /// <summary>
        /// 根据客户ID获取单个客户的连接信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        CustomerConnectInfo GetCustomerConnectByID(string customerID);

        /// <summary>
        /// 获取单个客户的数据加密的密钥文件
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        string GetDataKeyFileByCustomerID(string customerID);

        #endregion

        #region 数据交换
        /// <summary>
        /// 下发所有的应用系统信息至客户平台
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool PublishAppInfo(string customerID);

        /// <summary>
        /// 下发客户的菜单信息至客户平台
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        bool PublishMenuInfo(string customerID);
        #endregion

        #region 移动手机终端
        /// <summary>
        /// 根据用户标识，获取用户信息，门店信息，客户信息
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        Model.Customer.CustomerUserInfo GetCustomerUserInfoByMobileUser(string user_id);

        /// <summary>
        /// 根据手机用户标识获取相关的客户集合
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        IList<CustomerInfo> GetMobileCustomerList(string user_id);
        /// <summary>
        /// 根据手机用户标识获取相关的门店集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        IList<Model.Customer.CustomerShopInfo> SelectMobileShopList(string user_id);
        /// <summary>
        /// 处理客户或者门店注册
        /// </summary>
        /// <param name="customerInfo">客户对象</param>
        /// <param name="customerShopInfo">门店对象</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        bool SetMobileCustomerInfo(CustomerInfo customerInfo, CustomerShopInfo customerShopInfo, out string strError);
        #endregion

        #region
        /// <summary>
        /// 客户建立门店系统20130201
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool SetBSSystemStart(string customer_id, out string strError);
        #endregion

        IList<TDataDeployInfo> GetTDataDeployList(Hashtable condition, int maxRowCount, int startRowIndex);
        int GetTDataDeployListCount(Hashtable condition);
        string GetTDataDeployIdByCustomerId(string customerId);

        /// <summary>
        /// 根据行业版本获取商户列表
        /// </summary>
        /// <param name="vocaVerMappingID"></param>
        /// <returns></returns>
        IList<CustomerInfo> SelectCustomerByVersionID(string vocaVerMappingID); 
    }
}
