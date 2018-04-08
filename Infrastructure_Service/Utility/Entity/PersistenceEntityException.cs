/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 9:38:54
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

using JIT.Utility;
using JIT.Const;

namespace JIT.Utility.Entity
{
    /// <summary>
    /// 持久化实体的异常 
    /// </summary>
    public class PersistenceEntityException:JITException
    {
        /// <summary>
        /// 持久化实体的异常
        /// </summary>
        /// <param name="pErrorMessage">错误信息</param>
        public PersistenceEntityException(string pErrorMessage) : base(ERROR_TYPES.INFRASTRUCTURE, INFRASTRUCTURE_ERROR_CODES.PERSISTENCE_STATUS_ERROR, pErrorMessage) { }

        /// <summary>
        /// 持久化实体的异常
        /// </summary>
        /// <param name="pErrorMessageTemplate">错误信息模板</param>
        /// <param name="pMessageParams">错误信息模板参数</param>
        public PersistenceEntityException(string pErrorMessageTemplate,params string[] pMessageParams) : base(ERROR_TYPES.INFRASTRUCTURE, INFRASTRUCTURE_ERROR_CODES.PERSISTENCE_STATUS_ERROR,pErrorMessageTemplate,pMessageParams) { }
    }
}
