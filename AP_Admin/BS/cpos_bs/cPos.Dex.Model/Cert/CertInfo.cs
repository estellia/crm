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
    /// 用户凭证
    /// </summary>
    [Serializable]
    public class CertInfo
    {
        #region CertId
        private string _CertId;
        public string CertId
        {
            get
            {
                return this._CertId;
            }
            set
            {
                this._CertId = value;
            }
        }
        #endregion

        #region CertTypeId
        private string _CertTypeId;
        public string CertTypeId
        {
            get
            {
                return this._CertTypeId;
            }
            set
            {
                this._CertTypeId = value;
            }
        }
        #endregion

        #region UserId
        private string _UserId;
        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }
        #endregion

        #region UserCode
        private string _UserCode;
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
        #endregion

        #region UserName
        private string _UserName;
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }
        #endregion

        #region CustomerCode
        private string _CustomerCode;
        public string CustomerCode
        {
            get
            {
                return this._CustomerCode;
            }
            set
            {
                this._CustomerCode = value;
            }
        }
        #endregion

        #region CustomerId
        private string _CustomerId;
        public string CustomerId
        {
            get
            {
                return this._CustomerId;
            }
            set
            {
                this._CustomerId = value;
            }
        }
        #endregion

        #region CertPwd
        private string _CertPwd;
        public string CertPwd
        {
            get
            {
                return this._CertPwd;
            }
            set
            {
                this._CertPwd = value;
            }
        }
        #endregion

        #region CertStatus
        private string _CertStatus;
        public string CertStatus
        {
            get
            {
                return this._CertStatus;
            }
            set
            {
                this._CertStatus = value;
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