using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;

namespace cPos.Dex.Model
{
    public class PosVersionInfo
    {
        #region VersionId
        private string _VersionId;
        public string VersionId
        {
            get
            {
                return this._VersionId;
            }
            set
            {
                this._VersionId = value;
            }
        }
        #endregion

        #region VersionNo
        private string _VersionNo;
        public string VersionNo
        {
            get
            {
                return this._VersionNo;
            }
            set
            {
                this._VersionNo = value;
            }
        }
        #endregion

        #region VersionPath
        private string _VersionPath;
        public string VersionPath
        {
            get
            {
                return this._VersionPath;
            }
            set
            {
                this._VersionPath = value;
            }
        }
        #endregion

        #region VersionUrl
        private string _VersionUrl;
        public string VersionUrl
        {
            get
            {
                return this._VersionUrl;
            }
            set
            {
                this._VersionUrl = value;
            }
        }
        #endregion

        #region FileName
        private string _FileName;
        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }
        #endregion

        #region VersionStatus
        private string _VersionStatus;
        public string VersionStatus
        {
            get
            {
                return this._VersionStatus;
            }
            set
            {
                this._VersionStatus = value;
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

        #region VersionSize
        private int _VersionSize;
        public int VersionSize
        {
            get
            {
                return this._VersionSize;
            }
            set
            {
                this._VersionSize = value;
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

        #region Remark
        private string _Remark;
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
        #endregion

    }
}