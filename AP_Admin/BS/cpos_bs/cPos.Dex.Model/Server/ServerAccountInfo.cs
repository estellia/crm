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
    /// ServerAccount
    /// </summary>
    [Serializable]
    public class ServerAccountInfo
    {
        #region AccountId
        private string _AccountId;
        public string AccountId
        {
            get
            {
                return this._AccountId;
            }
            set
            {
                this._AccountId = value;
            }
        }
        #endregion

        #region AccountTypeId
        private string _AccountTypeId;
        public string AccountTypeId
        {
            get
            {
                return this._AccountTypeId;
            }
            set
            {
                this._AccountTypeId = value;
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

        #region AccountName
        private string _AccountName;
        public string AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }
        #endregion

        #region AccountPwd
        private string _AccountPwd;
        public string AccountPwd
        {
            get
            {
                return this._AccountPwd;
            }
            set
            {
                this._AccountPwd = value;
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