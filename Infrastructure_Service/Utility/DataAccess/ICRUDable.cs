/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:41:55
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
using System.Data;
using System.Data.SqlClient;

using JIT.Utility.Entity;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// CRUD操作的接口
    /// <remarks>
    /// <para>C=Create,新增</para>
    /// <para>R=Read,查询</para>
    /// <para>U=Update,更新</para>
    /// <para>D=Delete,删除</para>
    /// </remarks>
    /// </summary>
    public interface ICRUDable<T> where T:IEntity
    {
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        void Create(T pEntity);

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Create(T pEntity, IDbTransaction pTran);

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        T GetByID(object pID);

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        T[] GetAll();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Update(T pEntity,IDbTransaction pTran);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        void Update(T pEntity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        void Delete(T pEntity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Delete(T pEntity, IDbTransaction pTran);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Delete(object pID, IDbTransaction pTran);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Delete(T[] pEntities, IDbTransaction pTran);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        void Delete(T[] pEntities);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        void Delete(object[] pIDs, IDbTransaction pTran);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        void Delete(object[] pIDs);
    }
}
