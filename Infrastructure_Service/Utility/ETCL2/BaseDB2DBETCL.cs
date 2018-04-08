/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/13 20:15:10
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
using System.Data;
using System.Text;

using JIT.Utility;
using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// Database to Database的ETCL基类
    /// </summary>
    public abstract class BaseDB2DBETCL:BaseETCL
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseDB2DBETCL(BasicUserInfo pUserInfo):base(pUserInfo)
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// ETCL过程的数据源
        /// </summary>
        public IDataSource DataSource { get; set; }
        #endregion 

        #region BaseETCL的成员

        /// <summary>
        /// 从数据源中抽取数据
        /// </summary>
        /// <returns>数据</returns>
        protected override DataTable[] RetrieveData()
        {
            return this.DataSource.RetrieveData(this.CurrentUserInfo);
        }
        #endregion
    }
}
