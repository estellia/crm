/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 9:27:06
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
using System.ComponentModel;
using System.Text;

namespace JIT.Utility.Entity
{
    /// <summary>
    /// 持久化状态
    /// <remarks>
    /// <para>Temporary:临时状态。当实体刚刚被实例化时，实体的状态为Temporary。</para>
    /// <para>Read:读状态。数据从数据库中读取到实体中。并且这些数据在应用程序中没有被修改过。</para>
    /// <para>Dirty:脏状态。数据从数据库中读取到实体中，并且这些数据在应用程序中被修改过。</para>
    /// <para>Delete:删除状态。数据从数据库中读取到实体中，并且在应用程序中，实体被删除了。</para>
    /// <para>New:创建状态。实体直接在程序中创建且数据库中没有实体所对应的条目。</para>
    /// <para>Destory:销毁状态。实体直接在应用程序中New并且在程序运行期间被Delete的。</para>
    /// <para>Exists:存在状态。存在状态=读状态+脏状态+创建状态</para>
    /// <para>All:所有状态。所有状态=临时状态+读状态+脏状态+删除状态+销毁状态+创建状态</para>
    /// </remarks>
    /// </summary>
    [Flags]
    public enum PersistenceStatus
    {
        /// <summary>
        /// 临时状态。
        /// <remarks>
        /// <para>当实体刚刚被实例化时，实体的状态为Temporary。</para>
        /// </remarks>
        /// </summary>
        [Description("临时状态")]
        Temporary = 1
        ,
        /// <summary>
        /// 读状态。数据从数据库中读取到实体中。并且这些数据在应用程序中没有被修改过。
        /// </summary>
        [Description("读状态")]
        Read = 2
        ,
        /// <summary>
        /// 脏状态。数据从数据库中读取到实体中，并且这些数据在应用程序中被修改过。
        /// </summary>
        [Description("脏状态")]
        Dirty = 4
        ,
        /// <summary>
        /// 删除状态。数据从数据库中读取到实体中，并且在应用程序中，实体被删除了。
        /// </summary>
        [Description("删除状态")]
        Delete = 8
        ,
        /// <summary>
        /// 创建状态。实体直接在程序中创建且数据库中没有实体所对应的条目。
        /// </summary>
        [Description("创建状态")]
        New = 16
        ,
        /// <summary>
        /// 销毁状态。实体直接在应用程序中New并且在程序运行期间被Delete的。
        /// </summary>
        [Description("销毁状态")]
        Destory = 32
        ,
        /// <summary>
        /// 存在状态。存在状态=读状态+脏状态+创建状态
        /// </summary>
        [Description("存在状态")]
        Exists = 22
        ,
        /// <summary>
        /// 所有状态。所有状态=临时状态+读状态+脏状态+删除状态+销毁状态+创建状态
        /// </summary>
        [Description("所有状态")]
        All = 63
    }
}
