/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 19:34:40
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
using JIT.Utility.Entity;

namespace JIT.Utility.SMS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SMSSendEntity : BaseEntity 
    {
        #region 属性集
        public string SMSCustomerID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        #endregion

    }
}