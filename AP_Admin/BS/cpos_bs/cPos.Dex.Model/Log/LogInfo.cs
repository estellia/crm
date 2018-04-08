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
    /// LogInfo
    /// </summary>
    [Serializable]
    public class LogInfo
    {
        #region LogId
        private string _LogId;
        /// <summary>
        /// 日志ID
        /// </summary>
        public string LogId
        {
            get
            {
                return this._LogId;
            }
            set
            {
                this._LogId = value;
            }
        }
        #endregion

        #region BizId
        private string _BizId;
        /// <summary>
        /// 业务ID
        /// </summary>
        public string BizId
        {
            get
            {
                return this._BizId;
            }
            set
            {
                this._BizId = value;
            }
        }
        #endregion

        #region BizName
        private string _BizName;
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BizName
        {
            get
            {
                return this._BizName;
            }
            set
            {
                this._BizName = value;
            }
        }
        #endregion

        #region LogTypeId
        private string _LogTypeId;
        /// <summary>
        /// 日志类型ID
        /// </summary>
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

        #region LogTypeCode
        private string _LogTypeCode;
        /// <summary>
        /// 日志类型代码
        /// </summary>
        public string LogTypeCode
        {
            get
            {
                return this._LogTypeCode;
            }
            set
            {
                this._LogTypeCode = value;
            }
        }
        #endregion

        #region LogCode
        private string _LogCode;
        /// <summary>
        /// 日志代码
        /// </summary>
        public string LogCode
        {
            get
            {
                return this._LogCode;
            }
            set
            {
                this._LogCode = value;
            }
        }
        #endregion

        #region LogBody
        private string _LogBody;
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogBody
        {
            get
            {
                return this._LogBody;
            }
            set
            {
                this._LogBody = value;
            }
        }
        #endregion

        #region CreateTime
        private string _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
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
        /// <summary>
        /// 创建人ID
        /// </summary>
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
        /// <summary>
        /// 修改时间
        /// </summary>
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
        /// <summary>
        /// 修改人ID
        /// </summary>
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

        #region CustomerCode
        private string _CustomerCode;
        /// <summary>
        /// 客户代码
        /// </summary>
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
        /// <summary>
        /// 客户ID
        /// </summary>
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

        #region UnitCode
        private string _UnitCode;
        /// <summary>
        /// 门店代码
        /// </summary>
        public string UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }
        #endregion

        #region UnitId
        private string _UnitId;
        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitId
        {
            get
            {
                return this._UnitId;
            }
            set
            {
                this._UnitId = value;
            }
        }
        #endregion

        #region UserCode
        private string _UserCode;
        /// <summary>
        /// 用户代码
        /// </summary>
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

        #region UserId
        private string _UserId;
        /// <summary>
        /// 用户ID
        /// </summary>
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

        #region IfCode
        private string _IfCode;
        /// <summary>
        /// 接口代码
        /// </summary>
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

        #region AppCode
        private string _AppCode;
        /// <summary>
        /// 平台代码
        /// </summary>
        public string AppCode
        {
            get
            {
                return this._AppCode;
            }
            set
            {
                this._AppCode = value;
            }
        }
        #endregion
    }
}