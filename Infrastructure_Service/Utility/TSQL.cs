/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/13 13:20:19
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
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// T-SQL语句 
    /// </summary>
    [Serializable]
    public class TSQL:ICloneable
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TSQL()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据库服务器名
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// T-SQL语句
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public TimeSpan ExecutionTime { get; set; }
        #endregion

        #region ICloneable 成员
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region 深拷贝
        /// <summary>
        /// ICloneable
        /// </summary>
        /// <returns></returns>
        public TSQL Clone()
        {
            return this.MemberwiseClone() as TSQL;
        }
        #endregion 
    }
}
