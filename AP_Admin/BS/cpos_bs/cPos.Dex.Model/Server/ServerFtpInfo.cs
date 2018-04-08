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
    /// ServerFtp
    /// </summary>
    [Serializable]
    public class ServerFtpInfo
    {
        #region FtpId
        private string _FtpId;
        public string FtpId
        {
            get
            {
                return this._FtpId;
            }
            set
            {
                this._FtpId = value;
            }
        }
        #endregion

        #region FtpCode
        private string _FtpCode;
        public string FtpCode
        {
            get
            {
                return this._FtpCode;
            }
            set
            {
                this._FtpCode = value;
            }
        }
        #endregion

        #region FtpName
        private string _FtpName;
        public string FtpName
        {
            get
            {
                return this._FtpName;
            }
            set
            {
                this._FtpName = value;
            }
        }
        #endregion

        #region WritePath
        private string _WritePath;
        public string WritePath
        {
            get
            {
                return this._WritePath;
            }
            set
            {
                this._WritePath = value;
            }
        }
        #endregion

        #region FtpIp
        private string _FtpIp;
        public string FtpIp
        {
            get
            {
                return this._FtpIp;
            }
            set
            {
                this._FtpIp = value;
            }
        }
        #endregion

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

        #region FtpStatus
        private string _FtpStatus;
        public string FtpStatus
        {
            get
            {
                return this._FtpStatus;
            }
            set
            {
                this._FtpStatus = value;
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