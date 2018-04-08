/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/13 13:43:54
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

namespace JIT.Utility
{
    /// <summary>
    /// 固定尺寸的队列
    /// <remarks>
    /// <para>1.线程安全</para>
    /// </remarks>
    /// </summary>
    public class FixedSizeQueue<T>
    {
        /// <summary>
        /// 内部的队列
        /// </summary>
        private List<T> _innerList = null;

        /// <summary>
        /// 内部的并发锁
        /// </summary>
        private object _locker = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pCapacity"></param>
        public FixedSizeQueue(int pCapacity)
        {
            this.Capacity = pCapacity;
            this._innerList = new List<T>();
        }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 队列中的元素数
        /// </summary>
        public int Count { get { return this._innerList.Count; } }

        /// <summary>
        /// 清除所有元素
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            this._innerList.Clear();
        }

        /// <summary>
        /// 确定某元素是否在队列中
        /// </summary>
        /// <param name="pElement"></param>
        /// <returns></returns>
        public bool Contain(T pElement)
        {
            return this._innerList.Contains(pElement);
        }

        /// <summary>
        /// 将元素入队
        /// </summary>
        /// <param name="pElement"></param>
        public void Enqueue(T pElement)
        {
            lock (_locker)
            {
                if (this.Count >= this.Capacity)
                {
                    this._innerList.RemoveAt(0);
                }
                this._innerList.Add(pElement);
            }
        }

        /// <summary>
        /// 移除并返回位于Queue开始处的对象。 
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            lock (_locker)
            {
                T temp = this._innerList[0];
                this._innerList.RemoveAt(0);
                return temp;
            }
        }

        /// <summary>
        /// 转换成数组
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return this._innerList.ToArray();
        }

        /// <summary>
        /// 索引指示器
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public T this[int pIndex]
        {
            get { return this._innerList[pIndex]; }
        }
    }
}
