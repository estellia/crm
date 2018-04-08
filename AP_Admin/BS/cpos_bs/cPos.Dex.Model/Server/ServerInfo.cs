using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.Model
{
    /// <summary>
    /// Server
    /// </summary>
    [Serializable]
    public class ServerInfo
    {
        #region ServerId
        private string _ServerId;
        public string ServerId
        {
            get
            {
                return this._ServerId;
            }
            set
            {
                this._ServerId = value;
            }
        }
        #endregion

        #region ServerCode
        private string _ServerCode;
        public string ServerCode
        {
            get
            {
                return this._ServerCode;
            }
            set
            {
                this._ServerCode = value;
            }
        }
        #endregion

        #region ServerName
        private string _ServerName;
        public string ServerName
        {
            get
            {
                return this._ServerName;
            }
            set
            {
                this._ServerName = value;
            }
        }
        #endregion

        #region ServerIp
        private string _ServerIp;
        public string ServerIp
        {
            get
            {
                return this._ServerIp;
            }
            set
            {
                this._ServerIp = value;
            }
        }
        #endregion

        #region ServerStatus
        private string _ServerStatus;
        public string ServerStatus
        {
            get
            {
                return this._ServerStatus;
            }
            set
            {
                this._ServerStatus = value;
            }
        }
        #endregion

        #region SortFlag
        private int _SortFlag;
        public int SortFlag
        {
            get
            {
                return this._SortFlag;
            }
            set
            {
                this._SortFlag = value;
            }
        }
        #endregion

        #region CreateTime
        private string _CreateTime;
        public string CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this._CreateTime = value;
            }
        }
        #endregion

        #region CreateUserId
        private string _CreateUserId;
        public string CreateUserId
        {
            get
            {
                return this._CreateUserId;
            }
            set
            {
                this._CreateUserId = value;
            }
        }
        #endregion

        #region ModifyTime
        private string _ModifyTime;
        public string ModifyTime
        {
            get
            {
                return this._ModifyTime;
            }
            set
            {
                this._ModifyTime = value;
            }
        }
        #endregion

        #region ModifyUserId
        private string _ModifyUserId;
        public string ModifyUserId
        {
            get
            {
                return this._ModifyUserId;
            }
            set
            {
                this._ModifyUserId = value;
            }
        }
        #endregion
    }
}