/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/16 10:08:08
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

namespace JIT.Utility.Cache
{
    /// <summary>
    /// 缓存操作的入口 
    /// </summary>
    public static class Caches
    {
        static Caches()
        {
            DEFAULT = new JITMemoryCache();
        }

        /// <summary>
        /// 默认的缓存
        /// </summary>
        public static readonly IJITCache DEFAULT = null;
    }
}
