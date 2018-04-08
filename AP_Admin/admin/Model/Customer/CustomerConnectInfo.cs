using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.Customer
{
    [Serializable]
    public class CustomerConnectInfo
    {
        public CustomerConnectInfo()
            : this(new CustomerInfo())
        { }

        public CustomerConnectInfo(CustomerInfo customer)
        {
            this.Customer = customer;
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public CustomerInfo Customer
        { get; set; }

        #region 数据库连接
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string DBServer
        { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName
        { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string DBUser
        { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string DBPassword
        { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string AccessURL
        { get; set; }

        /// <summary>
        /// WS访问地址
        /// </summary>
        public string WsUrl
        {
            get 
            {
                if (_WsUrl == null || _WsUrl.Trim().Length == 0)
                    return AccessURL;
                return _WsUrl;
            }
            set
            {
                this._WsUrl = value;
            }
        }
        private string _WsUrl;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DBConnectionString
        {
            get { return string.Format("user id={0};password={1};data source={2};database={3};", this.DBUser, this.DBPassword, this.DBServer, this.DBName); }
        }

        #endregion

        /// <summary>
        /// 密钥文件
        /// </summary>
        public string KeyFile
        { get; set; }

        /// <summary>
        /// 允许的最大门店数
        /// </summary>
        public int MaxShopCount
        { get; set; }

        /// <summary>
        /// 允许的最大用户数
        /// </summary>
        public int MaxUserCount
        { get; set; }

        /// <summary>
        /// 允许的最大终端数
        /// </summary>
        public int MaxTerminalCount
        { get; set; }
    }
}
