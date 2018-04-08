using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.Model
{
    public class InterfaceInfo
    {
        #region IfId
        private string _IfId;
        public string IfId
        {
            get
            {
                return this._IfId;
            }
            set
            {
                this._IfId = value;
            }
        }
        #endregion

        #region IfCode
        private string _IfCode;
        public string IfCode
        {
            get
            {
                return this._IfCode;
            }
            set
            {
                this._IfCode = value;
            }
        }
        #endregion

        #region IfName
        private string _IfName;
        public string IfName
        {
            get
            {
                return this._IfName;
            }
            set
            {
                this._IfName = value;
            }
        }
        #endregion

        #region IfUrl
        private string _IfUrl;
        public string IfUrl
        {
            get
            {
                return this._IfUrl;
            }
            set
            {
                this._IfUrl = value;
            }
        }
        #endregion

        #region IfDesc
        private string _IfDesc;
        public string IfDesc
        {
            get
            {
                return this._IfDesc;
            }
            set
            {
                this._IfDesc = value;
            }
        }
        #endregion

        #region IfStatus
        private string _IfStatus;
        public string IfStatus
        {
            get
            {
                return this._IfStatus;
            }
            set
            {
                this._IfStatus = value;
            }
        }
        #endregion

        #region IfTypeId
        private string _IfTypeId;
        public string IfTypeId
        {
            get
            {
                return this._IfTypeId;
            }
            set
            {
                this._IfTypeId = value;
            }
        }
        #endregion

        #region QueueId
        private string _QueueId;
        public string QueueId
        {
            get
            {
                return this._QueueId;
            }
            set
            {
                this._QueueId = value;
            }
        }
        #endregion

        #region RefreshTime
        private string _RefreshTime;
        public string RefreshTime
        {
            get
            {
                return this._RefreshTime;
            }
            set
            {
                this._RefreshTime = value;
            }
        }
        #endregion

        #region SortFlag
        private string _SortFlag;
        public string SortFlag
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

        #region PerMaxCount
        private string _PerMaxCount;
        public string PerMaxCount
        {
            get
            {
                return this._PerMaxCount;
            }
            set
            {
                this._PerMaxCount = value;
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