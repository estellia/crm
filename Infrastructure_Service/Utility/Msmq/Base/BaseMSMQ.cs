using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.ExtensionMethod;
using System.Messaging;

namespace JIT.Utility.Msmq.Base
{
    public abstract class BaseMSMQ<T>:IMSMQ<T>
    {
        #region 构造函数
        public BaseMSMQ()
        {
            _msq = CreateMsq();
            _msqNotifyMsq = CreateNotifyMsq();
            Type type = typeof(T);
            Lable = type.Name + "_" + DateTime.Now.ToShortDateString();
        }

        public BaseMSMQ(Action<Message> ac)
            : this()
        {
            AfterRecieve += ac;
        }
        #endregion

        #region 事件
        private event Action<Message> AfterRecieve;
        #endregion

        #region 字段
        /// <summary>
        /// 缓存队列
        /// </summary>
        MessageQueue _msq;
        /// <summary>
        /// 通知队列
        /// </summary>
        MessageQueue _msqNotifyMsq;
        #endregion

        #region 属性
        /// <summary>
        /// 标签
        /// </summary>
        public string Lable { get; set; }
        /// <summary>
        /// 配置类
        /// </summary>
        public IMessageConfig MessageConfig { get; set; }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 创建缓存队列
        /// </summary>
        /// <returns></returns>
        public abstract MessageQueue CreateMsq();
        /// <summary>
        /// 创建通知队列
        /// </summary>
        /// <returns></returns>
        public abstract MessageQueue CreateNotifyMsq();
        #endregion

        #region 接口方法
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="obj"></param>
        public void Send(T obj)
        {
            Message m = new Message(obj, new BinaryMessageFormatter());
            if (MessageConfig != null)
            {
                MessageConfig.Config(m);
            }
            m.Label = Lable;
            if (_msq != null)
                _msq.Send(m);
            _msqNotifyMsq.Send(m);
        }

        /// <summary>
        /// 监听
        /// </summary>
        public void Listen()
        {
            _msqNotifyMsq.BeginReceive(MessageQueue.InfiniteTimeout, _msqNotifyMsq, new AsyncCallback(CallBack));
        }

        /// <summary>
        /// 获取缓存队列的枚举器
        /// </summary>
        /// <returns></returns>
        public MessageEnumerator GetEnumerator()
        {
            Valid();
            return _msq.GetMessageEnumerator2();
        }
        #endregion

        #region 基方法
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public T[] GetAllMessage()
        {
            Valid();
            var temps = GetEnumerator();
            List<T> list = new List<T> { };
            while (temps.MoveNext())
            {
                Message m = temps.Current;
                m.Formatter = new BinaryMessageFormatter();
                if (m.Body.GetType() == typeof(T))
                    list.Add((T)m.Body);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 按标签获取所有数据
        /// </summary>
        /// <param name="Lable"></param>
        /// <returns></returns>
        public T[] GetAllMessageByLable(string Lable)
        {
            Valid();
            var temps = GetEnumerator();
            List<T> list = new List<T> { };
            while (temps.MoveNext())
            {
                Message m = temps.Current;
                m.Formatter = new BinaryMessageFormatter();
                if (m.Body.GetType() == typeof(T) && m.Label == Lable)
                    list.Add((T)m.Body);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 验证缓存队列
        /// </summary>
        private void Valid()
        {
            if (_msq == null)
                throw new Exception("未设置缓存队列");
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        public void ClearAll()
        {
            var temps = _msq.GetMessageEnumerator2();
            while (temps.MoveNext())
            {
                Message m = temps.Current;
                m.Formatter = new BinaryMessageFormatter();
                if (m.Body.GetType() == typeof(T))
                    temps.RemoveCurrent();
            }
        }

        /// <summary>
        /// 设置配置类
        /// </summary>
        /// <param name="config"></param>
        public void SetConfig(IMessageConfig config)
        {
            MessageConfig = config;
        }

        /// <summary>
        /// 监听回调函数
        /// </summary>
        /// <param name="result"></param>
        private void CallBack(IAsyncResult result)
        {
            if (AfterRecieve != null)
            {
                MessageQueue msq = result.AsyncState as MessageQueue;
                var msg = msq.EndReceive(result);
                msg.Formatter = new BinaryMessageFormatter();
                AfterRecieve(msg);
                msq.BeginReceive(MessageQueue.InfiniteTimeout, msq, new AsyncCallback(CallBack));
            }
        }
        #endregion

    }
}
