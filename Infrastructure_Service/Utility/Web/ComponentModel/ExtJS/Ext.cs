/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 14:17:18
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

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// Ext 
    /// </summary>
    public static class Ext
    {
        #region 获得HTML dom 的body
        /// <summary>
        /// 获得HTML dom 的body
        /// </summary>
        /// <returns></returns>
        public static IJavascriptObject GetBody()
        {
            return new JavascriptBlock(string.Format("{0}.getBody()",ClassName));
        }
        #endregion

        private const string ClassName = "Ext";
    }
}
