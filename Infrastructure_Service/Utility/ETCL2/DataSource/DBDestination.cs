/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/15 9:52:39
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

using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2.DataSource
{
    /// <summary>
    /// 数据库作为ETCL的目的地 
    /// </summary>
    public class DBDestination:IDestination
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DBDestination()
        {
        }
        #endregion

        #region IDestination 成员
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <param name="pDataItems">需要保存的数据条目</param>
        /// <returns>保存成功的记录数</returns>
        public int Save(BasicUserInfo pUserInfo, IETCLDataItem[] pDataItems)
        {
            return 0;
        }

        #endregion
    }
}
