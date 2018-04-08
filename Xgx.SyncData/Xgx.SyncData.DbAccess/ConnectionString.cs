using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.DbAccess
{
    public class ConnectionString
    {
        #region cpos_bs_xgx库
        /// <summary>
        /// cpos_bs_xgx库只读连接
        /// </summary>
        public static string XgxSelect
        {
            get
            {
                if (string.IsNullOrEmpty(_xgxSelect))
                {
                    _xgxSelect = ConfigurationManager.ConnectionStrings["xgx_select"].ConnectionString;
                }
                return _xgxSelect;
            }
        }
        /// <summary>
        /// cpos_bs_xgx库写连接
        /// </summary>
        public static string XgxInsert
        {
            get
            {
                if (string.IsNullOrEmpty(_xgxInsert))
                {
                    _xgxInsert = ConfigurationManager.ConnectionStrings["xgx_insert"].ConnectionString;
                }
                return _xgxInsert;
            }
        }
        #endregion
        #region cpos_ap库
        /// <summary>
        /// cpos_ap库只读连接
        /// </summary>
        public static string XgxApSelect
        {
            get
            {
                if (string.IsNullOrEmpty(_xgxApSelect))
                {
                    _xgxApSelect = ConfigurationManager.ConnectionStrings["xgx_ap_select"].ConnectionString;
                }
                return _xgxApSelect;
            }
        }
        /// <summary>
        /// cpos_ap库写连接
        /// </summary>
        public static string XgxApInsert
        {
            get
            {
                if (string.IsNullOrEmpty(_xgxApInsert))
                {
                    _xgxApInsert = ConfigurationManager.ConnectionStrings["xgx_ap_insert"].ConnectionString;
                }
                return _xgxApInsert;
            }
        }
        #endregion
        private static string _xgxSelect;
        private static string _xgxInsert;
        private static string _xgxApSelect;
        private static string _xgxApInsert;

    }
}
