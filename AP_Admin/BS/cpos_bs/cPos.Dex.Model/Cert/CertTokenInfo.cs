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
    /// 令牌
    /// </summary>
    [Serializable]
    public class CertTokenInfo
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

        #region CertToken
        private string _CertToken;
        public string CertToken
        {
            get
            {
                return this._CertToken;
            }
            set
            {
                this._CertToken = value;
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

        #region UpdateTime
        private string _UpdateTime;
        public string UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }
        #endregion

        #region CycleTime
        /// <summary>
        /// 周期（单位：毫秒）
        /// </summary>
        private int _CycleTime = 10000;
        public int CycleTime
        {
            get
            {
                return this._CycleTime;
            }
            set
            {
                this._CycleTime = value;
            }
        }
        #endregion

        #region IsOverTime
        /// <summary>
        /// 是否过期
        /// </summary>
        private bool _IsOverTime = false;
        public bool IsOverTime
        {
            get
            {
                return this._IsOverTime;
            }
            set
            {
                this._IsOverTime = value;
            }
        }
        #endregion
    }
}