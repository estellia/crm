/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:59:25
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data
{
    /// <summary>
    /// Ext.data.StoreManager 
    /// </summary>
    public static class StoreManager
    {
        #region 根据数据集id查找指定的数据集
        /// <summary>
        /// 根据数据集id查找指定的数据集
        /// </summary>
        /// <param name="pStoreID">数据集id</param>
        /// <returns>相应的Javascript语句</returns>
        public static IJavascriptObject Lookup(string pStoreID)
        {
            JavascriptBlock block = new JavascriptBlock();
            block.Sentences = new List<string>();
            block.Sentences.Add(string.Format("{0}.lookup({1}{2}{1})",ClassName,JSONConst.STRING_WRAPPER,pStoreID));
            //
            return block;
        }
        #endregion

        private const string ClassName = "Ext.data.StoreManager";
    }
}
