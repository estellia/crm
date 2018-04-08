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
    /// LogType
    /// </summary>
    [Serializable]
    public class LogTypeInfo
    {
        #region LogTypeId
        private string _LogTypeId;
        public string LogTypeId
        {
            get
            {
                return this._LogTypeId;
            }
            set
            {
                this._LogTypeId = value;
            }
        }
        #endregion

        #region TypeCode
        private string _TypeCode;
        public string TypeCode
        {
            get
            {
                return this._TypeCode;
            }
            set
            {
                this._TypeCode = value;
            }
        }
        #endregion

        #region TypeName
        private string _TypeName;
        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                this._TypeName = value;
            }
        }
        #endregion

        #region TypeStatus
        private string _TypeStatus;
        public string TypeStatus
        {
            get
            {
                return this._TypeStatus;
            }
            set
            {
                this._TypeStatus = value;
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