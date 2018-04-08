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
    /// PackageInfo
    /// </summary>
    [Serializable]
    public class PackageInfo
    {
        #region PkgId
        private string _PkgId;
        public string PkgId
        {
            get
            {
                return this._PkgId;
            }
            set
            {
                this._PkgId = value;
            }
        }
        #endregion

        #region PkgTypeId
        private string _PkgTypeId;
        public string PkgTypeId
        {
            get
            {
                return this._PkgTypeId;
            }
            set
            {
                this._PkgTypeId = value;
            }
        }
        #endregion

        #region PkgTypeCode
        private string _PkgTypeCode;
        public string PkgTypeCode
        {
            get
            {
                return this._PkgTypeCode;
            }
            set
            {
                this._PkgTypeCode = value;
            }
        }
        #endregion

        #region PkgGenTypeId
        private string _PkgGenTypeId;
        public string PkgGenTypeId
        {
            get
            {
                return this._PkgGenTypeId;
            }
            set
            {
                this._PkgGenTypeId = value;
            }
        }
        #endregion

        #region PkgGenTypeCode
        private string _PkgGenTypeCode;
        public string PkgGenTypeCode
        {
            get
            {
                return this._PkgGenTypeCode;
            }
            set
            {
                this._PkgGenTypeCode = value;
            }
        }
        #endregion

        #region AppTypeId
        private string _AppTypeId;
        public string AppTypeId
        {
            get
            {
                return this._AppTypeId;
            }
            set
            {
                this._AppTypeId = value;
            }
        }
        #endregion

        #region AppTypeCode
        private string _AppTypeCode;
        public string AppTypeCode
        {
            get
            {
                return this._AppTypeCode;
            }
            set
            {
                this._AppTypeCode = value;
            }
        }
        #endregion

        #region UnitId
        private string _UnitId;
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

        #region PkgName
        private string _PkgName;
        public string PkgName
        {
            get
            {
                return this._PkgName;
            }
            set
            {
                this._PkgName = value;
            }
        }
        #endregion

        #region PkgDesc
        private string _PkgDesc;
        public string PkgDesc
        {
            get
            {
                return this._PkgDesc;
            }
            set
            {
                this._PkgDesc = value;
            }
        }
        #endregion

        #region PkgSeq
        private Int64 _PkgSeq;
        public Int64 PkgSeq
        {
            get
            {
                return this._PkgSeq;
            }
            set
            {
                this._PkgSeq = value;
            }
        }
        #endregion

        #region PkgStatus
        private string _PkgStatus;
        public string PkgStatus
        {
            get
            {
                return this._PkgStatus;
            }
            set
            {
                this._PkgStatus = value;
            }
        }
        #endregion

        #region BatId
        private string _BatId;
        public string BatId
        {
            get
            {
                return this._BatId;
            }
            set
            {
                this._BatId = value;
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