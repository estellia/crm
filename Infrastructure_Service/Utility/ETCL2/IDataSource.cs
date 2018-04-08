﻿/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 11:03:59
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

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// ETCL过程的数据源接口 
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// 从数据源中抽取数据
        /// </summary>
        /// <param name="pUserInfo">当前的用户信息</param>
        /// <returns></returns>
        DataTable[] RetrieveData(BasicUserInfo pUserInfo);
    }
}
