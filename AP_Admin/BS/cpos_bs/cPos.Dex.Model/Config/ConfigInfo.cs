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
    /// Config
    /// </summary>
    [Serializable]
    public class ConfigInfo
    {
        #region CfgId
        private string _CfgId;
        public string CfgId
        {
            get
            {
                return this._CfgId;
            }
            set
            {
                this._CfgId = value;
            }
        }
        #endregion

        #region CfgKey
        private string _CfgKey;
        public string CfgKey
        {
            get
            {
                return this._CfgKey;
            }
            set
            {
                this._CfgKey = value;
            }
        }
        #endregion

        #region CfgValue
        private string _CfgValue;
        public string CfgValue
        {
            get
            {
                return this._CfgValue;
            }
            set
            {
                this._CfgValue = value;
            }
        }
        #endregion

        #region CfgStatus
        private string _CfgStatus;
        public string CfgStatus
        {
            get
            {
                return this._CfgStatus;
            }
            set
            {
                this._CfgStatus = value;
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