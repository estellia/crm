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
    /// PackageFileInfo
    /// </summary>
    [Serializable]
    public class PackageFileInfo
    {
        #region PkgfId
        private string _PkgfId;
        public string PkgfId
        {
            get
            {
                return this._PkgfId;
            }
            set
            {
                this._PkgfId = value;
            }
        }
        #endregion

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

        #region PkgfName
        private string _PkgfName;
        public string PkgfName
        {
            get
            {
                return this._PkgfName;
            }
            set
            {
                this._PkgfName = value;
            }
        }
        #endregion

        #region PkgfSeq
        private Int64 _PkgfSeq;
        public Int64 PkgfSeq
        {
            get
            {
                return this._PkgfSeq;
            }
            set
            {
                this._PkgfSeq = value;
            }
        }
        #endregion

        #region PkgfPath
        private string _PkgfPath;
        public string PkgfPath
        {
            get
            {
                return this._PkgfPath;
            }
            set
            {
                this._PkgfPath = value;
            }
        }
        #endregion

        #region UrlPath
        public string UrlPath
        {
            get
            {
                string path = this._PkgfPath.Trim().Replace("\\", "/");
                if (!path.StartsWith("/")) path = "/" + path;
                return path;
            }
        }
        #endregion

        #region PkgfStatus
        private string _PkgfStatus;
        public string PkgfStatus
        {
            get
            {
                return this._PkgfStatus;
            }
            set
            {
                this._PkgfStatus = value;
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