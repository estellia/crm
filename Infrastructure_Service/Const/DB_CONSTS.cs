/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/10/31 20:38:07
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace JIT.Const
{
    /// <summary>
    /// 数据库的常量 
    /// </summary>
    public static class DB_CONSTS
    {
        /// <summary>
        /// 逻辑删除的数据库字段名
        /// </summary>
        public readonly static string DELETE_FIELD_NAME = "IsDelete";

        /// <summary>
        /// 客户ID的字段名
        /// </summary>
        public readonly static string CLIENT_FIELD_NAME = "ClientID";

        /// <summary>
        /// 数据的最后更新时间的字段名
        /// </summary>
        public readonly static string LAST_UPATE_TIME_FIELD_NAME = "LastUpdateTime";

        /// <summary>
        /// 数据的创建时间的字段名
        /// </summary>
        public readonly static string CREATE_TIME_FIELD_NAME = "CreateTime";

        /// <summary>
        /// 静态构造函数,初始化常量
        /// </summary>
        static DB_CONSTS()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["delete_field_name"]))
            {
                DB_CONSTS.DELETE_FIELD_NAME = ConfigurationManager.AppSettings["delete_field_name"].Trim();
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["client_field_name"]))
            {
                DB_CONSTS.CLIENT_FIELD_NAME = ConfigurationManager.AppSettings["client_field_name"].Trim();
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["create_time_field_name"]))
            {
                DB_CONSTS.CREATE_TIME_FIELD_NAME = ConfigurationManager.AppSettings["create_time_field_name"].Trim();
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["last_update_time_field_name"]))
            {
                DB_CONSTS.LAST_UPATE_TIME_FIELD_NAME = ConfigurationManager.AppSettings["last_update_time_field_name"].Trim();
            }
        }
    }
}
