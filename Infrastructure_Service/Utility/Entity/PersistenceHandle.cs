/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 9:40:30
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

namespace JIT.Utility.Entity
{
    /// <summary>
    /// 持久化状态句柄 
    /// </summary>
    [Serializable]
    public class PersistenceHandle
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PersistenceHandle()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 实体的持久化状态
        /// </summary>
        private PersistenceStatus _status = PersistenceStatus.Temporary;

        /// <summary>
        /// 实体的持久化的状态
        /// </summary>
        public virtual PersistenceStatus Status
        {
            get { return this._status; }
        }
        #endregion

        #region 一组判断当前实体的持久化状态方法
        /// <summary>
        /// 实体状态是否为临时状态
        /// </summary>
        public bool IsTemporary
        {
            get
            {
                if (this._status == PersistenceStatus.Temporary)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为读状态
        /// </summary>
        public bool IsRead
        {
            get
            {
                if (this._status == PersistenceStatus.Read)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为脏状态
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (this._status == PersistenceStatus.Dirty)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为删除状态
        /// </summary>
        public bool IsDelete
        {
            get
            {
                if (this._status == PersistenceStatus.Delete)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为创建状态
        /// </summary>
        public bool IsNew
        {
            get
            {
                if (this._status == PersistenceStatus.New)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为销毁状态
        /// </summary>
        public bool IsDestory
        {
            get
            {
                if (this._status == PersistenceStatus.Destory)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 实体状态是否为存在状态
        /// </summary>
        public bool IsExists
        {
            get
            {
                if ((PersistenceStatus.Exists | this._status) == PersistenceStatus.Exists)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region 一组导致实体状态变更的方法
        /// <summary>
        /// 实体从DB中加载了数据后调用本方法更新实体的持久化状态
        /// </summary>
        public void Load()
        {
            this._status = PersistenceStatus.Read;
        }

        /// <summary>
        /// 实体中的数据被修改后调用本方法更新实体的持久化状态
        /// </summary>
        public void Modify()
        {
            switch (this._status)
            {
                case PersistenceStatus.Temporary:
                    throw new PersistenceEntityException("实体未加载数据,没有数据可修改.");
                case PersistenceStatus.Read:
                    this._status = PersistenceStatus.Dirty;
                    break;
                case PersistenceStatus.Dirty:
                case PersistenceStatus.New:
                    break;
                case PersistenceStatus.Delete:
                    throw new PersistenceEntityException("实体被删除,无法修改.");
                case PersistenceStatus.Destory:
                    throw new PersistenceEntityException("实体被销毁,无法修改.");
                default:
                    throw new PersistenceEntityException(string.Format("未知的实体持久化状态{0}.", this._status.ToString()));
            }
        }
        /// <summary>
        /// 在内存中创建新的实体实例时调用本方法更新实体的持久化状态。
        /// </summary>
        public virtual void New()
        {
            this._status = PersistenceStatus.New;
        }

        /// <summary>
        /// 删除实体时调用本方法更新实体的持久化状态。
        /// </summary>
        public virtual void Delete()
        {
            switch (this._status)
            {
                case PersistenceStatus.Temporary:
                    throw new PersistenceEntityException("实体未加载数据,无法删除.");
                case PersistenceStatus.Read:
                case PersistenceStatus.Dirty:
                    this._status = PersistenceStatus.Delete;
                    break;
                case PersistenceStatus.New:
                    this._status = PersistenceStatus.Destory;
                    break;
                case PersistenceStatus.Destory:
                    break;
                default:
                    throw new PersistenceEntityException(string.Format("未知的实体持久化状态{0}.", this._status.ToString()));

            }
        }
        /// <summary>
        /// 持久化实体后调用本方法更新实体的持久化状态
        /// </summary>
        public virtual void Save()
        {
            switch (this._status)
            {
                case PersistenceStatus.Temporary:
                    throw new PersistenceEntityException("临时状态的实体无法持久化.");
                case PersistenceStatus.Read:
                    break;
                case PersistenceStatus.Dirty:
                case PersistenceStatus.New:
                    this._status = PersistenceStatus.Read;
                    break;
                case PersistenceStatus.Destory:
                    throw new PersistenceEntityException("销毁状态的实体无法持久化.");
                default:
                    throw new PersistenceEntityException(string.Format("未知的实体持久化状态{0}.", this._status.ToString()));

            }
        }
        #endregion

        #region 类成员
        /// <summary>
        /// 获取创建状态的持久化句柄
        /// </summary>
        /// <returns></returns>
        public static PersistenceHandle GetNewStatusHandle()
        {
            PersistenceHandle h = new PersistenceHandle();
            h.New();
            return h;
        }
        /// <summary>
        /// 获取读状态的持久化句柄
        /// </summary>
        /// <returns></returns>
        public static PersistenceHandle GetReadStatusHandle()
        {
            PersistenceHandle h = new PersistenceHandle();
            h.Load();
            return h;
        }
        #endregion
    }
}
