/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:45:19
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

using JIT.Utility.DataAccess.Query;
using JIT.Utility.Entity;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 查询接口 
    /// </summary>
    public interface IQueryable<T> where T:IEntity
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <returns></returns>
        T[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys);

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        PagedQueryResult<T> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex);

        /// <summary>
        /// 根据实体进行查询
        /// </summary>
        /// <param name="pQueryEntity">查询实体,如果实体的属性值不为null,则表示为等值条件</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <returns></returns>
        T[] QueryByEntity(T pQueryEntity, OrderBy[] pOrderBys);

        /// <summary>
        /// 根据实体进行分页查询
        /// </summary>
        /// <param name="pQueryEntity">查询实体,如果实体的属性值不为null,则表示为等值条件</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        PagedQueryResult<T> PagedQueryByEntity(T pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex);
    }
}
