using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Component.SqlMappers;
using IBatisNet.DataMapper;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Right;

namespace cPos.Admin.Service.Implements
{
    public class CustomerService : BaseService , cPos.Admin.Service.Interfaces.ICustomerService
    {

        #region 用户

        public int SelectUserListCount(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Customer.User.SelectUserListCount", condition);
        }

        public IList<Model.Customer.CustomerUserInfo> SelectUserList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);
            IList<CustomerUserInfo> customer_user_lst = 
                MSSqlMapper.Instance().QueryForList<CustomerUserInfo>("Customer.User.SelectUserList", condition);
            return customer_user_lst;
        }

        public CustomerUserInfo GetCustomerUserByID(string cuID)
        {
            if (string.IsNullOrEmpty(cuID))
                return null;
            else
                return MSSqlMapper.Instance().QueryForObject<CustomerUserInfo>("Customer.User.SelectUserByID", cuID);
        }

        public int ValidateUser(string customerCode, string account, string password, out Hashtable ht)
        {

            try
            {
                #region
                ht = new Hashtable();
                if (string.IsNullOrEmpty(customerCode))
                {
                    throw new ArgumentNullException("customerCode");
                }

                if (string.IsNullOrEmpty(account))
                {
                    throw new ArgumentNullException("account");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException("password");
                }

                string encrypted_pwd = HashManager.Hash(password, HashProviderType.MD5);

                CustomerInfo customer = MSSqlMapper.Instance().QueryForObject<CustomerInfo>("Customer.Customer.SelectByCode", customerCode);
                if (customer == null)
                {
                    //客户不存在
                    return -11;
                }

                if (customer.Status == -1)
                {
                    //客户被停用
                    return -12;
                }

                Hashtable ht_temp = new Hashtable();
                ht_temp.Add("CustomerID", customer.ID);
                ht_temp.Add("UserAccount", account);
                CustomerUserInfo user = MSSqlMapper.Instance().QueryForObject<CustomerUserInfo>("Customer.User.SelectUserByAccount", ht_temp);
                if (user == null)
                {
                    //用户不存在
                    return -1;
                }
                if (user.Status == -1)
                {
                    //用户被停用
                    return -2;
                }
                if (Convert.ToInt32(user.ExpiredDate.Replace("-", "")) < Convert.ToInt32(this.GetDate2(DateTime.Today)))
                {
                    //用户过期
                    return -5;
                }
                #endregion
                if (user.Password == encrypted_pwd)
                {
                    //生成token
                    string tokenID = this.NewGUID();
                    ht_temp.Clear();
                    ht_temp.Add("TokenID", tokenID);
                    ht_temp.Add("CustomerUserID", user.ID);
                    ht_temp.Add("LoginIP", SessionManager.CurrentIP);
                    ht_temp.Add("SessionID", SessionManager.SessionID);
                    try
                    {
                        MSSqlMapper.Instance().BeginTransaction();
                        MSSqlMapper.Instance().Insert("Customer.User.InsertCustomerUserLoginToken", ht_temp);
                        MSSqlMapper.Instance().Insert("Customer.User.InsertLoginLog", ht_temp);
                        MSSqlMapper.Instance().CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        MSSqlMapper.Instance().RollBackTransaction();
                        throw ex;
                    }
                    ht.Add("Token", tokenID);
                    //取客户的访问地址
                    CustomerConnectInfo customerConnect = MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", customer.ID);
                    if (customerConnect != null)
                    {
                        ht.Add("AccessURL", customerConnect.AccessURL);
                    }
                    ht.Add("CustomerID", customer.ID);
                    return 1;
                }
                else
                {
                    //密码不正确
                    return -3;
                }
            }
            catch (Exception ex)
            {
                ht = new Hashtable();
                ht.Add("error", ex.Message);
                //密码不正确
                return -3;
            }         
        }

        public string GetLoginCustomerUserIDByToken(string token)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Token", token);
#if DEBUG
            //测试时，保留token为一年
            ht.Add("ValidSeconds", 365 * 24 * 60 * 60);
#else
            //正式发布时，保留token为5秒
            ht.Add("ValidSeconds", 5);
#endif
            string cu_id = MSSqlMapper.Instance().QueryForObject<string>("Customer.User.GetLoginCustomerUserIDByToken", ht);
#if DEBUG
            ;
#else
            //正式发布时，token第一次访问后，即删除
            if (!string.IsNullOrEmpty(cu_id))
            {
                MSSqlMapper.Instance().Delete("Customer.User.DeleteUserLoginToken", token);
            }
#endif
            return cu_id;
        }

        public bool InsertCustomerUserFromCP(Model.Customer.CustomerUserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            
            if (string.IsNullOrEmpty(user.ID))
            {
                throw new ArgumentNullException("user_id");
            }
            if (string.IsNullOrEmpty(user.StatusDescription))
            {
                throw new ArgumentNullException("user_status_desc");
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentNullException("user_name");
            }
            if (string.IsNullOrEmpty(user.Account))
            {
                throw new ArgumentNullException("user_account");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("user_pwd");
            }
            if (string.IsNullOrEmpty(user.ExpiredDate))
            {
                throw new ArgumentNullException("user_expired_date");
            }

            Model.Customer.CustomerUserInfo cu = this.GetCustomerUserByID(user.ID);
            if (cu != null)
            {
                return this.ModifyCustomerUserFromCP(user);
            }
            else
            {
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //插入用户
                    MSSqlMapper.Instance().Insert("Customer.User.InsertCustomerUserFromCP", user);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_USER,
                        user.ID, Model.Bill.BillActionFlagType.Create, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return true;
            }
        }

        public bool ModifyCustomerUserFromCP(Model.Customer.CustomerUserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(user.ID))
            {
                throw new ArgumentNullException("user_id");
            }
            if (string.IsNullOrEmpty(user.StatusDescription))
            {
                throw new ArgumentNullException("user_status_desc");
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentNullException("user_name");
            }
            if (string.IsNullOrEmpty(user.Account))
            {
                throw new ArgumentNullException("user_account");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("user_pwd");
            }
            if (string.IsNullOrEmpty(user.ExpiredDate))
            {
                throw new ArgumentNullException("user_expired_date");
            }

            Model.Customer.CustomerUserInfo cu = this.GetCustomerUserByID(user.ID);
            if (cu == null)
            {
                return this.InsertCustomerUserFromCP(user);
            }
            else
            {
                int ret = 0;
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //修改用户
                    ret = MSSqlMapper.Instance().Update("Customer.User.UpdateCustomerUserFromCP", user);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_USER,
                        user.ID, Model.Bill.BillActionFlagType.Modify, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return ret == 1;
            }
        }

        public bool EnableCustomerUserFromCP(Model.Customer.CustomerUserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(user.ID))
            {
                throw new ArgumentNullException("user_id");
            }
            if (string.IsNullOrEmpty(user.StatusDescription))
            {
                throw new ArgumentNullException("user_status_desc");
            }

            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改用户 
                ret = MSSqlMapper.Instance().Update("Customer.User.EnableCustomerUserFromCP", user);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_USER,
                    user.ID, Model.Bill.BillActionFlagType.Reserve, 1, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }

        public bool DisableCustomerUserFromCP(Model.Customer.CustomerUserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(user.ID))
            {
                throw new ArgumentNullException("user_id");
            }
            if (string.IsNullOrEmpty(user.StatusDescription))
            {
                throw new ArgumentNullException("user_status_desc");
            }
            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改用户
                ret = MSSqlMapper.Instance().Update("Customer.User.DisableCustomerUserFromCP", user);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_USER,
                user.ID, Model.Bill.BillActionFlagType.Reserve, 2, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }

        public bool ModifyCustomerUserPasswordFromCP(Model.Customer.CustomerUserInfo user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(user.ID))
            {
                throw new ArgumentNullException("user_id");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("user_pwd");
            }
            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改用户
                ret = MSSqlMapper.Instance().Update("Customer.User.ModifyCustomerUserPasswordFromCP", user);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_USER,
                user.ID, Model.Bill.BillActionFlagType.Reserve, 3, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }
        #endregion

        #region 客户

        public int SelectCustomerListCount(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.SelectCustomerListCount", condition);
        }

        public IList<CustomerInfo> SelectCustomerList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            return MSSqlMapper.Instance().QueryForList<CustomerInfo>("Customer.Customer.SelectCustomerList", condition);
        }

        public IList<CustomerInfo> GetAllCustomerList(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForList<CustomerInfo>("Customer.Customer.GetAllCustomerList", condition);
        }

        public CustomerInfo GetCustomerByID(string customerID, bool getConnectInfo, bool getMenuListInfo)
        {
            if (string.IsNullOrEmpty(customerID))
                return null;

            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //取客户基本信息
                CustomerInfo customer = MSSqlMapper.Instance().QueryForObject<CustomerInfo>("Customer.Customer.SelectByID", customerID);
                if (customer != null)
                {
                    //取客户的连接信息
                    if (getConnectInfo)
                    {
                        CustomerConnectInfo customer_connect = 
                            MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", customerID);
                        customer_connect.Customer = customer;
                        customer.Connect = customer_connect;
                    }
                    //取客户的可操作菜单列表
                    if (getMenuListInfo)
                    {
                        IList<CustomerMenuInfo> customer_menu_lst =
                            MSSqlMapper.Instance().QueryForList<CustomerMenuInfo>("Customer.Menu.SelectByCustomer", customerID);
                        foreach (CustomerMenuInfo customer_menu in customer_menu_lst)
                        {
                            customer_menu.Customer = customer;
                        }
                        customer.Menus = customer_menu_lst;
                    }
                }
                MSSqlMapper.Instance().CommitTransaction();
                return customer;
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public bool ExistCustomerCode(string customerID, string customerCode)
        {
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(customerID))
                ht.Add("CustomerID", customerID);
            ht.Add("CustomerCode", customerCode);
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.ExistCustomerCode", ht);
            return ret > 0;
        }

        public void InsertCustomer(LoggingSessionInfo loggingSession, CustomerInfo customer)
        {
            if (string.IsNullOrEmpty(customer.ID))
            {
                customer.ID = this.NewGUID();
            }
            customer.Creater.ID = loggingSession.UserID;
            customer.Creater.Name = loggingSession.UserName;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                
                //添加客户
                MSSqlMapper.Instance().Insert("Customer.Customer.Insert", customer);
				//添加客户限制门店数
				MSSqlMapper.Instance().Insert("Customer.Customer.CreateCustomerLimiUnit", customer);
				//添加客户的连接
				customer.Connect.Customer = customer;
                MSSqlMapper.Instance().Insert("Customer.Connect.Insert", customer.Connect);

                Hashtable htDepMap = new Hashtable();
                htDepMap["CustomerId"] = customer.ID;
                htDepMap["LastUpdateTime"] = GetNow();
                htDepMap["LastUpdateBy"] = loggingSession.UserID;
                //删除之前的商户与连接信息设置
                MSSqlMapper.Instance().Update("Customer.Customer.DeleteTCustomerDataDeployMappingByCustomerId", htDepMap);

                var depMapObj = new TCustomerDataDeployMappingInfo();
                depMapObj.MappingId = NewGUID();
                depMapObj.CustomerId = customer.ID;
                depMapObj.DataDeployId = customer.DataDeployId;//客户与数据库配置的关系
                depMapObj.CreateTime = GetNow();
                depMapObj.CreateBy = loggingSession.UserID;
                depMapObj.LastUpdateTime = GetNow();
                depMapObj.LastUpdateBy = loggingSession.UserID;
                depMapObj.IsALD = customer.IsALD;
                depMapObj.UnitId = customer.UnitId;//运营商id
                MSSqlMapper.Instance().Insert("Customer.Customer.InsertTCustomerDataDeployMapping", depMapObj);

                //添加客户可操作的菜单列表
                if (customer.Menus != null)
                {
                    foreach (CustomerMenuInfo customer_menu in customer.Menus)
                    {
                        customer_menu.Customer = customer;
                        customer_menu.ID = this.NewGUID();
                        MSSqlMapper.Instance().Insert("Customer.Menu.Insert", customer_menu);
                    }
                }
                //插入操作
                this.InsertBillActionLogWithoutFlow(loggingSession, Model.Bill.BillKindInfo.CODE_CUSTOMER,
                    customer.ID, Model.Bill.BillActionFlagType.Create, 1, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public void InsertCustomer(LoggingSessionInfo loggingSession, CustomerInfo customer,bool IsTran)
        {
            if (string.IsNullOrEmpty(customer.ID))
            {
                customer.ID = this.NewGUID();
            }
            customer.Creater.ID = loggingSession.UserID;
            customer.Creater.Name = loggingSession.UserName;
            try
            {
                if(IsTran) MSSqlMapper.Instance().BeginTransaction();

                //添加客户
                MSSqlMapper.Instance().Insert("Customer.Customer.Insert", customer);
                //添加客户的连接
                customer.Connect.Customer = customer;
                MSSqlMapper.Instance().Insert("Customer.Connect.Insert", customer.Connect);
                //添加客户可操作的菜单列表
                if (customer.Menus != null)
                {
                    foreach (CustomerMenuInfo customer_menu in customer.Menus)
                    {
                        customer_menu.Customer = customer;
                        customer_menu.ID = this.NewGUID();
                        MSSqlMapper.Instance().Insert("Customer.Menu.Insert", customer_menu);
                    }
                }
                //插入操作
                this.InsertBillActionLogWithoutFlow(loggingSession, Model.Bill.BillKindInfo.CODE_CUSTOMER,
                    customer.ID, Model.Bill.BillActionFlagType.Create, 1, null);

                if (IsTran) MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                if (IsTran) MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public void ModifyCustomer(LoggingSessionInfo loggingSession, CustomerInfo customer)
        {
            customer.LastEditor.ID = loggingSession.UserID;
            customer.LastEditor.Name = loggingSession.UserName;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();

                //更新客户
                MSSqlMapper.Instance().Update("Customer.Customer.Update", customer);
				//更新客户门店数
				MSSqlMapper.Instance().Update("Customer.Customer.UpdateCustomerLimiUnit", customer);
				//更新客户的连接
				customer.Connect.Customer = customer;
                MSSqlMapper.Instance().Update("Customer.Connect.Update", customer.Connect);


                Hashtable htDepMap = new Hashtable();
                htDepMap["CustomerId"] = customer.ID;
                htDepMap["LastUpdateTime"] = GetNow();
                htDepMap["LastUpdateBy"] = loggingSession.UserID;
                MSSqlMapper.Instance().Update("Customer.Customer.DeleteTCustomerDataDeployMappingByCustomerId", htDepMap);

                var depMapObj = new TCustomerDataDeployMappingInfo();
                depMapObj.MappingId = NewGUID();
                depMapObj.CustomerId = customer.ID;
                depMapObj.DataDeployId = customer.DataDeployId;
                depMapObj.CreateTime = GetNow();
                depMapObj.CreateBy = loggingSession.UserID;
                depMapObj.LastUpdateTime = GetNow();
                depMapObj.LastUpdateBy = loggingSession.UserID;
                depMapObj.IsALD = customer.IsALD;
                depMapObj.UnitId = customer.UnitId;
                MSSqlMapper.Instance().Insert("Customer.Customer.InsertTCustomerDataDeployMapping", depMapObj);


                //删除客户原先的可操作的菜单列表
                MSSqlMapper.Instance().Delete("Customer.Menu.DeleteByCustomer", customer.ID);
                //添加客户可操作的菜单列表
                if (customer.Menus != null)
                {
                    foreach (CustomerMenuInfo customer_menu in customer.Menus)
                    {
                        customer_menu.Customer = customer;
                        customer_menu.ID = this.NewGUID();
                        MSSqlMapper.Instance().Insert("Customer.Menu.Insert", customer_menu);
                    }
                }
                //插入操作
                this.InsertBillActionLogWithoutFlow(loggingSession, Model.Bill.BillKindInfo.CODE_CUSTOMER,
                    customer.ID, Model.Bill.BillActionFlagType.Modify, 1, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public bool CanCreateUser(string customerID)
        {
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CanCreateUser", customerID);
            return ret == 1;
        }

        public bool CanCreateTerminal(string customerID)
        {
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CanCreateTerminal", customerID);
            return ret == 1;
        }

        public bool CanCreateShop(string customerID)
        {
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CanCreateShop", customerID);
            return ret == 1;
        }
        /// <summary>
        /// 根据客户号，获取客户标识
        /// </summary>
        /// <param name="customer_code"></param>
        /// <returns></returns>
        public string GetCustomerIdByCode(string customer_code)
        {
            return MSSqlMapper.Instance().QueryForObject<string>("Customer.Customer.SelectIdByCode", customer_code);
        }
		/// <summary>
		/// 新建商户的限制门店数
		/// </summary>
		/// <param name="customerID"></param>
		/// <returns></returns>
		public bool CreateCustomerLimiUnit(string customerID) {
			int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CreateCustomerLimiUnit", customerID);
			return ret == 1;
		}
		/// <summary>
		/// 更新商户的限制门店数
		/// </summary>
		/// <param name="customerID"></param>
		/// <returns></returns>
		public bool UpdateCustomerLimiUnit(string customerID) {
			int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.UpdateCustomerLimiUnit", customerID);
			return ret == 1;
		}
		#endregion

		#region 连接

		public CustomerConnectInfo GetCustomerConnectByID(string customerID)
        {
            if (string.IsNullOrEmpty(customerID))
                return null;
            else
                return MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", customerID);
        }

        public string GetDataKeyFileByCustomerID(string customerID)
        {
            return MSSqlMapper.Instance().QueryForObject<string>("Customer.Connect.SelectDataKeyByID", customerID);
        }

        #endregion

        #region 数据交换

        public bool PublishAppInfo(string customerID)
        {
            CustomerConnectInfo customer_connect = MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", customerID);
            if (customer_connect == null)
            {
                return false;
            }

            IList<AppInfo> app_list = MSSqlMapper.Instance().QueryForList<AppInfo>("Right.App.GetCustomerVisibleAppList", null);

            string app_xml = XMLGenerator.Serialize(app_list);
            app_xml = XMLGenerator.ReplaceElementName(app_xml, "ArrayOfAppInfo", "ArrayOfAppSysModel");
            app_xml = XMLGenerator.ReplaceElementName(app_xml, "AppInfo", "AppSysModel");
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "PublishAppInfo", "DecryptApplications", app_xml);
#if DATA_ENCRYPT
            //取客户密钥文件
            string app_output = cPos.Admin.Component.CryptManager.EncryptString(customer_connect.KeyFile, app_xml);
#else
            string app_output = app_xml;
#endif
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "PublishAppInfo", "EncryptApplications", app_output);
            string service_url = customer_connect.AccessURL + "/" +
                System.Configuration.ConfigurationManager.AppSettings["cp_customer_ws_url"];
            CP.AuthBsWebServices service = new CP.AuthBsWebServices();
            service.Url = service_url;
            bool ret = service.SetAppSysInfos(app_output, customerID);
            return ret;
        }

        public bool PublishMenuInfo(string customerID)
        {
            CustomerConnectInfo customer_connect = MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", customerID);
            if (customer_connect == null)
            {
                return false;
            }

            IList<MenuInfo> menu_list = MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetAllMenuListByCustomer", customerID);

            string menu_xml = XMLGenerator.Serialize(menu_list);
            menu_xml = XMLGenerator.ReplaceElementName(menu_xml, "ArrayOfMenuInfo", "ArrayOfMenuModel");
            menu_xml = XMLGenerator.ReplaceElementName(menu_xml, "MenuInfo", "MenuModel");
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "PublishMenuInfo", "DecryptMenus", menu_xml);
#if DATA_ENCRYPT
            //取客户密钥文件
            string menu_output = cPos.Admin.Component.CryptManager.EncryptString(customer_connect.KeyFile, menu_xml); 
#else
            string menu_output = menu_xml;
#endif

            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "PublishMenuInfo", "EncryptMenus", menu_output);
            string service_url = customer_connect.AccessURL + "/" +
                System.Configuration.ConfigurationManager.AppSettings["cp_customer_ws_url"];
            CP.AuthBsWebServices service = new CP.AuthBsWebServices();
            service.Url = service_url;
            bool ret = service.SetMenuInfos(menu_output, customerID);
            return ret;
        }


        private bool synCustomerTerminalToCP(int type, CustomerTerminalInfo terminal)
        {
            
#if SYN_CP
            CustomerConnectInfo customerConnect = MSSqlMapper.Instance().QueryForObject<CustomerConnectInfo>("Customer.Connect.SelectByID", terminal.Customer.ID);
            string s1 = XMLGenerator.Serialize(terminal);
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "synCustomerTerminalToCP", "DecryptCustomerTerminal", s1);
#if DATA_ENCRYPT
            string s2 = this.EncryptStringByKeyFile(customerConnect.KeyFile, s1);
#else
            string s2 = s1;
#endif
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "synCustomerTerminalToCP", "EncryptCustomerTerminal", s2);
            CP.AuthBsWebServices cp_service = new CP.AuthBsWebServices();
            cp_service.Url = customerConnect.AccessURL + "/WebService/AuthBsWebServices.asmx";
            LogManager.Log(Interfaces.LogLevel.DEBUG, "ap", "CustomerService", "synCustomerTerminalToCP", "url", cp_service.Url);
            return cp_service.SetPosInfo(s2, terminal.Customer.ID, type);
#else
            return true;
#endif
        }

        #endregion

        #region 门店
        public int SelectShopListCount(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Customer.Shop.SelectShopListCount", condition);
        }

        public IList<Model.Customer.CustomerShopInfo> SelectShopList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            return MSSqlMapper.Instance().QueryForList<CustomerShopInfo>("Customer.Shop.SelectShopList", condition);
        }

        public IList<CustomerShopInfo> GetAllShopList(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForList<CustomerShopInfo>("Customer.Shop.GetAllShopList", condition);
        }
        /// <summary>
        /// 根据客户号码与门店号码获取门店编号
        /// </summary>
        /// <param name="customer_code"></param>
        /// <param name="customer_shop_code"></param>
        /// <returns></returns>
        public string GetCustomerShopIdByCode(string customer_code, string customer_shop_code)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerCode", customer_code);
            _ht.Add("CustomerShopCode",customer_shop_code);
            return MSSqlMapper.Instance().QueryForObject<string>("Customer.Shop.SelectIdByCode", _ht);
        }

        public CustomerShopInfo GetCustomerShopByID(string csID)
        {
            if (string.IsNullOrEmpty(csID))
                return null;
            else
                return MSSqlMapper.Instance().QueryForObject<CustomerShopInfo>("Customer.Shop.SelectShopByID", csID);
        }

        public bool InsertCustomerShopFromCP(Model.Customer.CustomerShopInfo shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException("shop");
            }

            if (string.IsNullOrEmpty(shop.ID))
            {
                throw new ArgumentNullException("unit_id");
            }
            if (string.IsNullOrEmpty(shop.StatusDescription))
            {
                throw new ArgumentNullException("unit_status_desc");
            }
            if (string.IsNullOrEmpty(shop.Name))
            {
                throw new ArgumentNullException("unit_name");
            }

            Model.Customer.CustomerShopInfo cs = this.GetCustomerShopByID(shop.ID);
            if (cs != null)
            {
                return this.ModifyCustomerShopFromCP(shop);
            }
            else
            {
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //添加门店
                    MSSqlMapper.Instance().Insert("Customer.Shop.InsertCustomerShopFromCP", shop);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_SHOP,
                        shop.ID, Model.Bill.BillActionFlagType.Create, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return true;
            }
        }

        public bool ModifyCustomerShopFromCP(Model.Customer.CustomerShopInfo shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException("shop");
            }

            if (string.IsNullOrEmpty(shop.ID))
            {
                throw new ArgumentNullException("unit_id");
            }
            if (string.IsNullOrEmpty(shop.StatusDescription))
            {
                throw new ArgumentNullException("unit_status_desc");
            }
            if (string.IsNullOrEmpty(shop.Name))
            {
                throw new ArgumentNullException("unit_name");
            }

            Model.Customer.CustomerShopInfo cs = this.GetCustomerShopByID(shop.ID);
            if (cs == null)
            {
                return this.InsertCustomerShopFromCP(shop);
            }
            else
            {
                int ret = 0;
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //修改门店
                    ret = MSSqlMapper.Instance().Update("Customer.Shop.UpdateCustomerShopFromCP", shop);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_SHOP,
                        shop.ID, Model.Bill.BillActionFlagType.Modify, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return ret == 1;
            }
        }

        public bool EnableCustomerShopFromCP(Model.Customer.CustomerShopInfo shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException("shop");
            }

            if (string.IsNullOrEmpty(shop.ID))
            {
                throw new ArgumentNullException("unit_id");
            }
            if (string.IsNullOrEmpty(shop.StatusDescription))
            {
                throw new ArgumentNullException("unit_status_desc");
            }

            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改门店
                ret = MSSqlMapper.Instance().Update("Customer.Shop.EnableCustomerShopFromCP", shop);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_SHOP,
                   shop.ID, Model.Bill.BillActionFlagType.Reserve, 1, null);
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }

        public bool DisableCustomerShopFromCP(Model.Customer.CustomerShopInfo shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException("shop");
            }

            if (string.IsNullOrEmpty(shop.ID))
            {
                throw new ArgumentNullException("unit_id");
            }
            if (string.IsNullOrEmpty(shop.StatusDescription))
            {
                throw new ArgumentNullException("unit_status_desc");
            }
            
            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改门店
                ret = MSSqlMapper.Instance().Update("Customer.Shop.DisableCustomerShopFromCP", shop);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_SHOP,
                    shop.ID, Model.Bill.BillActionFlagType.Reserve, 2, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }
        #endregion

        #region 终端
        public int SelectTerminalListCount(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Customer.Terminal.SelectTerminalListCount", condition);
        }

        public IList<Model.Customer.CustomerTerminalInfo> SelectTerminalList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            return MSSqlMapper.Instance().QueryForList<CustomerTerminalInfo>("Customer.Terminal.SelectTerminalList", condition);
        }

        public CustomerTerminalInfo GetCustomerTerminalByID(string ctID)
        {
            if (string.IsNullOrEmpty(ctID))
                return null;
            else
                return MSSqlMapper.Instance().QueryForObject<CustomerTerminalInfo>("Customer.Terminal.SelectTerminalByID", ctID);
        }

        public bool ExistCustomerTerminalCode(string terminalID, string terminalCode)
        {
            if (string.IsNullOrEmpty(terminalCode))
                return false;

            Hashtable ht = new Hashtable();
            ht.Add("TerminalCode", terminalCode);
            if (!string.IsNullOrEmpty(terminalID))
            {
                ht.Add("TerminalID", terminalID);
            }
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Customer.Terminal.ExistCustomerTerminalCode", ht);
            return ret >= 1;
        }

        public bool InsertCustomerTerminal(LoggingSessionInfo loggingSession, Model.Customer.CustomerTerminalInfo terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException("terminal");
            }
            if (string.IsNullOrEmpty(terminal.ID))
            {
                terminal.ID = this.NewGUID();
            }
            terminal.Creater.ID = loggingSession.UserID;
            terminal.Creater.Name = loggingSession.UserName;

            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //添加终端
                MSSqlMapper.Instance().Insert("Customer.Terminal.Insert", terminal);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_TERMINAL,
                    terminal.ID, Model.Bill.BillActionFlagType.Create, 1, null);
                //从数据库重查一遍
                terminal = MSSqlMapper.Instance().QueryForObject<CustomerTerminalInfo>("Customer.Terminal.SelectTerminalByID", terminal.ID);
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }

            //同步到业务平台
            return this.synCustomerTerminalToCP(1, terminal);
        }

        public bool ModifyCustomerTerminal(LoggingSessionInfo loggingSession, Model.Customer.CustomerTerminalInfo terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException("terminal");
            }
            terminal.LastEditor.ID = loggingSession.UserID;
            terminal.LastEditor.Name = loggingSession.UserName;

            int ret = 0;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();
                //修改终端
                ret = MSSqlMapper.Instance().Update("Customer.Terminal.Update", terminal);
                //记录操作
                this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_TERMINAL,
                    terminal.ID, Model.Bill.BillActionFlagType.Modify, 1, null);
                //从数据库重取终端信息(主要是为了客户ID)
                terminal = this.GetCustomerTerminalByID(terminal.ID);
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
            //同步到业务平台
            return this.synCustomerTerminalToCP(2, terminal);
        }

        public bool InsertCustomerTerminalFromCP(Model.Customer.CustomerTerminalInfo terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException("terminal");
            }

            if (string.IsNullOrEmpty(terminal.ID))
            {
                throw new ArgumentNullException("terminal_id");
            }
            if (string.IsNullOrEmpty(terminal.SN))
            {
                throw new ArgumentNullException("terminal_sn");
            }
            if (string.IsNullOrEmpty(terminal.HoldType))
            {
                throw new ArgumentNullException("terminal_hold_type");
            }
            if (string.IsNullOrEmpty(terminal.Type))
            {
                throw new ArgumentNullException("terminal_type");
            }

            Model.Customer.CustomerTerminalInfo ct = this.GetCustomerTerminalByID(terminal.ID);
            if (ct != null)
            {
                return this.ModifyCustomerTerminalFromCP(terminal);
            }
            else
            {
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //添加终端
                    MSSqlMapper.Instance().Insert("Customer.Terminal.InsertCustomerTerminalFromCP", terminal);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_TERMINAL,
                        terminal.ID, Model.Bill.BillActionFlagType.Create, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return true;
            }
        }

        public bool ModifyCustomerTerminalFromCP(Model.Customer.CustomerTerminalInfo terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException("terminal");
            }

            if (string.IsNullOrEmpty(terminal.ID))
            {
                throw new ArgumentNullException("terminal_id");
            }
            if (string.IsNullOrEmpty(terminal.SN))
            {
                throw new ArgumentNullException("terminal_sn");
            }
            if (string.IsNullOrEmpty(terminal.Type))
            {
                throw new ArgumentNullException("terminal_type");
            }
            Model.Customer.CustomerTerminalInfo ct = this.GetCustomerTerminalByID(terminal.ID);
            if (ct == null)
            {
                return this.ModifyCustomerTerminalFromCP(terminal);
            }
            else
            {
                int ret = 0;
                try
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    //修改终端
                    ret = MSSqlMapper.Instance().Update("Customer.Terminal.UpdateCustomerTerminalFromCP", terminal);
                    //记录操作
                    this.InsertBillActionLogWithoutFlow(null, Model.Bill.BillKindInfo.CODE_CUSTOMER_TERMINAL,
                        terminal.ID, Model.Bill.BillActionFlagType.Modify, 1, null);

                    MSSqlMapper.Instance().CommitTransaction();
                }
                catch (Exception ex)
                {
                    MSSqlMapper.Instance().RollBackTransaction();
                    throw ex;
                }
                return ret == 1;
            }
        }
        #endregion

        #region 品牌客户
        /// <summary>
        /// 品牌客户保存
        /// </summary>
        /// <param name="models">models</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns>Hashtable: 
        ///  status(成功：true, 失败：false)
        ///  error(错误描述)
        /// </returns>
        public Hashtable SaveBrandCustomerInfoList(bool IsTrans, IList<cPos.Admin.Model.BrandCustomerInfo> models)
        {
            Hashtable ht = new Hashtable();
            ht["status"] = false;
            try
            {
                if (IsTrans) MSSqlMapper.Instance().BeginTransaction();
                foreach (var model in models)
                {
                    if (!CheckExistBrandCustomerById(model.brand_customer_id))
                    {
                        MSSqlMapper.Instance().Insert("Customer.Customer.InsertBrandCustomer", model);
                    }
                    else
                    {
                        MSSqlMapper.Instance().Update("Customer.Customer.UpdateBrandCustomer", model);
                    }
                }

                if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                ht["status"] = true;
            }
            catch (Exception ex)
            {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            return ht;
        }

        public Hashtable SaveBrandCustomerInfo(cPos.Admin.Model.BrandCustomerInfo model)
        {
            var list = new List<cPos.Admin.Model.BrandCustomerInfo>();
            list.Add(model);
            return SaveBrandCustomerInfoList(true, list);
        }

        /// <summary>
        /// 检查品牌客户是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckExistBrandCustomer(cPos.Admin.Model.BrandCustomerInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CheckExistBrandCustomer", model);
            return count > 0 ? true : false;
        }

        public bool CheckExistBrandCustomer(string id, string code)
        {
            var obj = new cPos.Admin.Model.BrandCustomerInfo();
            obj.brand_customer_id = id;
            obj.brand_customer_code = code;
            return CheckExistBrandCustomer(obj);
        }

        public bool CheckExistBrandCustomerById(string id)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.CheckExistBrandCustomerById", id);
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 品牌客户查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="brand_customer_code">编号</param>
        /// <param name="brand_customer_name">名称</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public cPos.Admin.Model.BrandCustomerInfo SearchBrandCustomerList(
            cPos.Model.LoggingSessionInfo loggingSessionInfo
            , string brand_customer_code
            , string brand_customer_name
            , int maxRowCount
            , int startRowIndex
            )
        {
            Hashtable ht = new Hashtable();
            ht["brand_customer_code"] = brand_customer_code;
            ht["brand_customer_name"] = brand_customer_name;
            ht["StartRow"] = startRowIndex;
            ht["EndRow"] = startRowIndex + maxRowCount;

            cPos.Admin.Model.BrandCustomerInfo obj = new cPos.Admin.Model.BrandCustomerInfo();
            IList<cPos.Admin.Model.BrandCustomerInfo> list = new List<cPos.Admin.Model.BrandCustomerInfo>();
            list = MSSqlMapper.Instance().QueryForList<cPos.Admin.Model.BrandCustomerInfo>(
                "Customer.Customer.SearchBrandCustomerList", ht);
            obj.icount = list.Count;
            obj.List = list;
            return obj;
        }

        public cPos.Admin.Model.BrandCustomerInfo GetBrandCustomerById(string id)
        {
            return MSSqlMapper.Instance().QueryForObject<cPos.Admin.Model.BrandCustomerInfo>(
                "Customer.Customer.GetBrandCustomerById", id);
        }
        #endregion

        #region 界面通过客户设置业务平台开户
        /// <summary>
        /// 设置客户初始化门店信息
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public bool SetBSSystemStart(string customer_id,out string strError)
        {
            Model.Customer.CustomerInfo customerInfo = new CustomerInfo();
            customerInfo = GetCustomerByID(customer_id, true, true);
            Model.Customer.CustomerShopInfo unitInfo= new CustomerShopInfo();
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerID", customerInfo.ID);
            int shopCount = SelectShopListCount(_ht);
            if (shopCount > 0)
            {
                strError = "店铺系统已经初始化，不能重复初始化.";
                return false;
            }
            else {
                unitInfo.ID = NewGUID();   //创建一个门店******Code和Name都等于商户的标识和名称******
                unitInfo.Code = customerInfo.Code;
                unitInfo.Name = customerInfo.Name;
                unitInfo.Status = 1;
                unitInfo.customer_id = customerInfo.ID;
            }

            return SetMobileCustomerInfo(customerInfo,unitInfo,out strError);
        }
        #endregion

        #region 手机客户
        /// <summary>
        /// 根据用户标识，获取用户信息，门店信息，客户信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public Model.Customer.CustomerUserInfo GetCustomerUserInfoByMobileUser(string user_id)
        {
            Model.Customer.CustomerUserInfo customerUserInfo = new CustomerUserInfo();
            Model.User.UserInfo userInfo = new Model.User.UserInfo();
            userInfo = new MobileUserService().GetUserInfoById(user_id);
            if (userInfo != null && !userInfo.ID.Equals(""))
            {
                customerUserInfo.ID = userInfo.ID;
                customerUserInfo.Name = userInfo.Name;
                customerUserInfo.Account = userInfo.Account;
                customerUserInfo.Password = userInfo.Password;
                customerUserInfo.Status = userInfo.Status;
                customerUserInfo.StatusDescription = userInfo.StatusDescription;

            }
            customerUserInfo.CustomerList = GetMobileCustomerList(user_id);
            customerUserInfo.CustomerShopList = SelectMobileShopList(user_id);
            return customerUserInfo;
        }

        /// <summary>
        /// 根据手机用户标识获取相关的客户集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public IList<CustomerInfo> GetMobileCustomerList(string user_id)
        {
            IList<CustomerInfo> customerInfoList = new List<CustomerInfo>();
            customerInfoList = MSSqlMapper.Instance().QueryForList<CustomerInfo>("Customer.Customer.SelectByMobileUser", user_id);
            return customerInfoList;
        }
        /// <summary>
        /// 根据手机用户标识获取相关的门店集合
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public IList<Model.Customer.CustomerShopInfo> SelectMobileShopList(string user_id)
        {
            IList<Model.Customer.CustomerShopInfo> customerShopInfoList = new List<Model.Customer.CustomerShopInfo>();
            customerShopInfoList = MSSqlMapper.Instance().QueryForList<CustomerShopInfo>("Customer.Shop.SelectMobileShopList", user_id);
            
            return customerShopInfoList;
        }

        /// <summary>
        /// 处理客户或者门店注册
        /// </summary>
        /// <param name="customerInfo">客户对象[必须]</param>
        /// <param name="customerShopInfo">门店对象【必须】</param>
        /// <param name="strError">错误信息输出</param>
        /// <returns></returns>
        public bool SetMobileCustomerInfo(CustomerInfo customerInfo, CustomerShopInfo customerShopInfo, out string strError)
        {
            try
            {
                string strMenu = string.Empty;
                LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                string typeId = string.Empty;//typeId=1(总部与门店一起处理);typeId=2（只处理总部，不处理门店）;typeId=3（只处理门店，不处理总部）
                #region
                //if (customerShopInfo == null || customerShopInfo.Code.Equals(""))
                //{
                //    typeId = "2";
                //}
                //if (customerInfo == null || customerInfo.Code.Equals("")) { typeId = "3"; }
                //if (customerInfo != null && !customerInfo.Code.Equals("") && customerShopInfo != null && !customerShopInfo.Code.Equals(""))
                //{
                //    typeId = "1";
                //}
                //处理总部信息
                //if (typeId.Equals("1") || typeId.Equals("2"))
                //{
                    //CustomerInfo cInfo = new CustomerInfo();
                    //cInfo = GetCustomerByID(customerShopInfo.customer_id, true, false);
                    //if (cInfo == null || cInfo.ID.Equals(""))
                    //{
                    //    //获取连接数据库信息
                    //    Model.Customer.CustomerConnectInfo customerConnectInfo = new CustomerConnectInfo();
                    //    customerConnectInfo = GetCustomerConnectByID("2241b59b81ba4684b2a3095c66a489db");//作为公共参考
                    //    customerInfo.Connect = customerConnectInfo;

                    //    //新建客户
                    //    InsertCustomer(loggingSessionInfo, customerInfo, false);
                    //}
                    //else {
                    //    customerInfo = GetCustomerByID(customerShopInfo.customer_id, true, false);
                    //}
                    ////MSSqlMapper.Instance().CommitTransaction();
                //}
                //else {
                //    customerInfo = GetCustomerByID(customerShopInfo.customer_id, true, false);
                //}
                //处理菜单
                //if (typeId.Equals("1"))
                //{
                #endregion
                #region 获取菜单集合
                List<cPos.Admin.Model.Right.MenuInfo> menuInfoList = new List<cPos.Admin.Model.Right.MenuInfo>();
                menuInfoList = new RightService().GetAllMenuListByAppID("7C7CC257927D44BD8CF4F9CD5AC5BDCD").ToList();//--先获取云终端

                List<cPos.Admin.Model.Right.MenuInfo> menuInfoList2 = new List<cPos.Admin.Model.Right.MenuInfo>();
                menuInfoList2 = new RightService().GetAllMenuListByAppID("D8C5FF6041AA4EA19D83F924DBF56F93").ToList();//--再获取O2O业务系统

                menuInfoList.AddRange(menuInfoList2);//把云终端的和O2O业务系统的合在一起了
                    foreach(MenuInfo menuInfo in menuInfoList)
                    {
                        menuInfo.ID = menuInfo.ID + customerInfo.ID;
                        if (menuInfo.ParentMenuID.Equals("--"))
                        {
                            menuInfo.ParentMenuID = menuInfo.ParentMenuID;
                        }
                        else
                        {
                            menuInfo.ParentMenuID = menuInfo.ParentMenuID + customerInfo.ID;
                        }
                    }
                    strMenu = XMLGenerator.Serialize(menuInfoList);
                    strMenu = XMLGenerator.ReplaceElementName(strMenu, "ArrayOfMenuInfo", "ArrayOfMenuModel");//序列化了菜单数据
                    strMenu = XMLGenerator.ReplaceElementName(strMenu, "MenuInfo", "MenuModel");
                #endregion
                    //}

                string sCustomerInfo = XMLGenerator.Serialize(customerInfo);
                string strUnitInfo = string.Empty;
                if (customerShopInfo != null && !customerShopInfo.ID.Equals(""))
                {
                    strUnitInfo = XMLGenerator.Serialize(customerShopInfo);//在管理平台创建门店信息,门店code就是商户的code
                }

                CustomerWebServices.CustomerInfoInit customerWebService= new CustomerWebServices.CustomerInfoInit();
                //根据商户链接信息的AccessURL去调用远程服务器上的webservice
                if (customerInfo.Connect.AccessURL.ToString().Substring(customerInfo.Connect.AccessURL.ToString().Length - 1, 1).Equals("/"))
                {
                    //临时特殊处理的，主要是解决两个平台之间（业务平台与店铺平台）
                    //customerWebService.Url = customerInfo.Connect.AccessURL.Replace("8400","8201") + "webservice/CustomerInfoInit.asmx"; //customerInfo.Connect.AccessURL  "http://localhost:8021" 
                    customerWebService.Url = customerInfo.Connect.AccessURL+ "webservice/CustomerInfoInit.asmx";
                }
                else {
                    //customerWebService.Url = customerInfo.Connect.AccessURL.Replace("8400", "8201") + "/webservice/CustomerInfoInit.asmx"; //
                    customerWebService.Url = customerInfo.Connect.AccessURL + "/webservice/CustomerInfoInit.asmx";
                }
                //临时调试用的,用于在本地调试
//#if DEBUG
//                //customerWebService.Url = "http://localhost:2332" + "/webservice/CustomerInfoInit.asmx";
//#endif

                //customerWebService.Url = "http://localhost:56808/webservice/CustomerInfoInit.asmx?op=SetCustomerInfoInit";
                bool bReturn = customerWebService.SetCustomerInfoInit(sCustomerInfo, strUnitInfo,strMenu, typeId);
                if (bReturn) {
                    //Jermyn20140318 添加业务库需要初始化的信息
                    bReturn = SetCustomerInfo(customerInfo.ID);
                    strError = "ok";
                }
                else { strError = "失败"; }
                strError = "ok";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }

        #region 客户初始化信息 Jermyn20140319
        /// <summary>
        /// 客户初始化信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">单据标识</param>
        /// <returns></returns>
        public bool SetCustomerInfo(string customerId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("CustomerID", customerId);
                MSSqlMapper.Instance().Update("Customer.Customer.SetCustomerInfo", _ht);  //最后一步调用了存储过程spBasicSetting
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        /// <summary>
        /// 批量处理客户与门店注册信息
        /// </summary>
        /// <param name="customerInfoList">客户集合</param>
        /// <param name="customerShopInfoList">门店集合</param>
        /// <param name="strError">错误信息输出</param>
        /// <returns></returns>
        public bool SetMobileCustomerInfoBatch(IList<CustomerInfo> customerInfoList, IList<CustomerShopInfo> customerShopInfoList, out string strError)
        {
            try
            {
                LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                string typeId = string.Empty;//typeId=1(总部与门店一起处理);typeId=2（只处理总部，不处理门店）;typeId=3（只处理门店，不处理总部）
                bool bReturn = false;
                #region 处理客户信息
                if (customerInfoList != null && customerInfoList.Count > 0)
                {
                    MSSqlMapper.Instance().BeginTransaction();
                    try
                    {
                        //获取连接数据库信息
                        Model.Customer.CustomerConnectInfo customerConnectInfo = new CustomerConnectInfo();
                        customerConnectInfo = GetCustomerConnectByID("2241b59b81ba4684b2a3095c66a489db");//作为公共参考

                        foreach (CustomerInfo customerInfo in customerInfoList)
                        {
                            Hashtable _ht = new Hashtable();
                            _ht.Add("Code", customerInfo.Code);
                            int icount = SelectCustomerListCount(_ht);
                            if (icount.Equals(0))
                            {
                                //插入客户信息
                                customerInfo.Connect = customerConnectInfo;
                                //新建客户
                                InsertCustomer(loggingSessionInfo, customerInfo, false);
                            }
                        }
                        MSSqlMapper.Instance().CommitTransaction();
                    }
                    catch (Exception ex2) { MSSqlMapper.Instance().RollBackTransaction(); throw (ex2); }
                }
                #endregion

                #region 注册门店
                if (customerInfoList != null && customerInfoList.Count > 0)
                {
                    foreach (CustomerShopInfo customerShopInfo in customerShopInfoList)
                    {
                        CustomerInfo customerInfo = GetCustomerByID(customerShopInfo.customer_id, true, false);
                        if (customerInfo != null && !customerInfo.ID.Equals(""))
                        {
                            CustomerShopInfo cShopInfo = GetCustomerShopByID(customerShopInfo.ID);
                            if (cShopInfo == null || cShopInfo.ID.Equals(""))
                            {
                                bReturn = SetMobileCustomerInfo(customerInfo, customerShopInfo, out strError);
                            }
                        }
                    }
                }
                #endregion
                strError = "ok";
                return bReturn;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 手机客户数据打包
        /// <summary>
        /// 获取Mobile未打包的Customer数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包数量</returns>
        public int GetMobileCustomerNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            if (User_Id == null || User_Id.Trim().Length == 0) return 0;
            Hashtable condition = new Hashtable();
            condition["Customer_Id"] = Customer_Id;
            condition["User_Id"] = User_Id;
            condition["Unit_Id"] = Unit_Id;
            return MSSqlMapper.Instance().QueryForObject<int>(
                "Customer.Customer.GetMobileCustomerNotPackagedCount", condition);
        }

        /// <summary>
        /// 获取Mobile需要打包的Customer集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包数据集合</returns>
        public IList<CustomerInfo> GetMobileCustomerListNotPackaged(string Customer_Id, string User_Id,
            string Unit_Id, int startRow, int rowsCount)
        {
            if (User_Id == null || User_Id.Trim().Length == 0) return null;
            Hashtable condition = new Hashtable();
            condition["Customer_Id"] = Customer_Id;
            condition["User_Id"] = User_Id;
            condition["Unit_Id"] = Unit_Id;
            condition["StartRow"] = startRow;
            condition["EndRow"] = startRow + rowsCount;
            return MSSqlMapper.Instance().QueryForList<CustomerInfo>(
                "Customer.Customer.GetMobileCustomerListNotPackaged", condition);
        }

        /// <summary>
        /// 设置Mobile记录Customer打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="dataList">数据集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetMobileCustomerBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id,
            IList<CustomerInfo> dataList)
        {
            if (dataList == null || dataList.Count == 0) return true;
            CustomerInfo obj = new CustomerInfo();
            obj.BatId = bat_id;
            obj.CustomerList = dataList;
            obj.ModifyUserId = User_Id;
            obj.ModifyTime = GetNow();
            MSSqlMapper.Instance().Update("Customer.Customer.SetMobileCustomerBatInfo", obj);
            return true;
        }

        /// <summary>
        /// 更新Mobile Customer表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool UpdateMobileCustomerNotPackagedIfFlag(string Customer_Id, 
            string User_Id, string Unit_Id, string bat_id)
        {
            if (bat_id == null || bat_id.Trim().Length == 0) return true;
            CustomerInfo obj = new CustomerInfo();
            obj.BatId = bat_id;
            obj.ModifyUserId = User_Id;
            obj.ModifyTime = GetNow();
            MSSqlMapper.Instance().Update("Customer.Customer.UpdateMobileCustomerNotPackagedIfFlag", obj);
            return true;
        }
        #endregion

        #region 手机门店数据打包
        /// <summary>
        /// 获取Mobile未打包的CustomerShop数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns>未打包数量</returns>
        public int GetMobileCustomerShopNotPackagedCount(string Customer_Id, string User_Id, string Unit_Id)
        {
            if (User_Id == null || User_Id.Trim().Length == 0) return 0;
            Hashtable condition = new Hashtable();
            condition["Customer_Id"] = Customer_Id;
            condition["User_Id"] = User_Id;
            condition["Unit_Id"] = Unit_Id;
            return MSSqlMapper.Instance().QueryForObject<int>(
                "Customer.Shop.GetMobileCustomerShopNotPackagedCount", condition);
        }

        /// <summary>
        /// 获取Mobile需要打包的CustomerShop集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="strartRow">开始行</param>
        /// <param name="rowsCount">每页行数</param>
        /// <returns>未打包数据集合</returns>
        public IList<CustomerShopInfo> GetMobileCustomerShopListNotPackaged(string Customer_Id, string User_Id,
            string Unit_Id, int startRow, int rowsCount)
        {
            if (User_Id == null || User_Id.Trim().Length == 0) return null;
            Hashtable condition = new Hashtable();
            condition["Customer_Id"] = Customer_Id;
            condition["User_Id"] = User_Id;
            condition["Unit_Id"] = Unit_Id;
            condition["StartRow"] = startRow;
            condition["EndRow"] = startRow + rowsCount;
            return MSSqlMapper.Instance().QueryForList<CustomerShopInfo>(
                "Customer.Shop.GetMobileCustomerShopListNotPackaged", condition);
        }

        /// <summary>
        /// 设置Mobile记录CustomerShop打包批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="dataList">数据集合</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetMobileCustomerShopBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id,
            IList<CustomerShopInfo> dataList)
        {
            if (dataList == null || dataList.Count == 0) return true;
            CustomerShopInfo obj = new CustomerShopInfo();
            obj.BatId = bat_id;
            obj.CustomerShopList = dataList;
            obj.ModifyUserId = User_Id;
            obj.ModifyTime = GetNow();
            MSSqlMapper.Instance().Update("Customer.Shop.SetMobileCustomerShopBatInfo", obj);
            return true;
        }

        /// <summary>
        /// 更新Mobile CustomerShop表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool UpdateMobileCustomerShopNotPackagedIfFlag(
            string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            if (bat_id == null || bat_id.Trim().Length == 0) return true;
            CustomerShopInfo obj = new CustomerShopInfo();
            obj.BatId = bat_id;
            obj.ModifyUserId = User_Id;
            obj.ModifyTime = GetNow();
            MSSqlMapper.Instance().Update("Customer.Shop.UpdateMobileCustomerShopNotPackagedIfFlag", obj);
            return true;
        }
        #endregion

        public IList<TDataDeployInfo> GetTDataDeployList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            return MSSqlMapper.Instance().QueryForList<TDataDeployInfo>("Customer.Customer.GetTDataDeployList", condition);
        }

        public int GetTDataDeployListCount(Hashtable condition)
        {
            return MSSqlMapper.Instance().QueryForObject<int>("Customer.Customer.GetTDataDeployListCount", condition);
        }

        public string GetTDataDeployIdByCustomerId(string customerId)
        {
            return MSSqlMapper.Instance().QueryForObject<string>("Customer.Customer.GetTDataDeployIdByCustomerId", customerId);
        }

        /// <summary>
        /// 根据行业版本获取商户列表
        /// </summary>
        /// <param name="vocaVerMappingID"></param>
        /// <returns></returns>
        public IList<CustomerInfo> SelectCustomerByVersionID(string vocaVerMappingID)
        {
            return MSSqlMapper.Instance().QueryForList<CustomerInfo>("Customer.Customer.SelectCustomerByVersionID", vocaVerMappingID);
        }

    }
}
