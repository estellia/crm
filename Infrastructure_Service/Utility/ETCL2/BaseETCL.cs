/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/12 15:22:59
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
using System.Data;
using System.Text;

using JIT.Utility;
using JIT.Utility.ETCL2.ValueObject;

namespace JIT.Utility.ETCL2
{
    /// <summary>
    /// ETCL过程的基类
    /// <remarks>
    /// <para>template method模式</para>
    /// </remarks>
    /// </summary>
    public abstract class BaseETCL:IETCL
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseETCL(BasicUserInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 当前的用户信息
        /// </summary>
        public BasicUserInfo CurrentUserInfo { get; set; }
        #endregion

        #region IETCL 成员
        /// <summary>
        /// 执行ETCL处理
        /// </summary>
        /// <returns>处理结果</returns>
        public ETCLProcessResult Process()
        {
            var result = new ETCLProcessResult();
            //
            this.OnInit();
            //从数据源中抽取数据
            var dts = this.RetrieveData();
            if (dts == null || dts.Length <= 0)
                return result;
            else
            {
                //统计数据源记录数
                foreach (var dt in dts)
                {
                    if (dt != null)
                        result.SourceItemCount += dt.Rows.Count;
                }
                //
                var items = this.Transfer(dts);
                if (items == null || items.Length <= 0)
                {//转换后无数据,直接返回
                    return result;
                }
                else
                {
                    result.TransferedItemCount = items.Length;
                }
                //
                CheckResult[] cr = null;
                bool isPass = this.Check(items, out cr);
                result.CheckResults = cr;
                if (isPass)
                {//检查通过,将数据导入目的地
                    result.IsSuccess = true;
                    result.DestinationItemCount = this.Load(items);
                }
                else
                {//检查后不通过,不能导入,并进行错误处理
                    result.IsSuccess = false;
                    this.OnCheckingNotPass(cr);
                }
            }
            //
            return result;
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void OnInit();

        /// <summary>
        /// 从数据源中抽取数据
        /// </summary>
        /// <returns>数据</returns>
        protected abstract DataTable[] RetrieveData();

        /// <summary>
        /// 对数据进行转换,并最终返回一个强类型的数据实例
        /// </summary>
        /// <param name="pDataes"></param>
        /// <returns></returns>
        protected abstract IETCLDataItem[] Transfer(DataTable[] pDataes);

        /// <summary>
        /// 对导入的数据执行检查
        /// <remarks>
        /// <para>在数据检查过程中可能需要做的事情有:</para>
        /// <para>1.数据项的有效性检查.有效性包含:数据的类型有效、数据项的值范围合法</para>
        /// <para>2.数据的重复性检查.重复性检查包含导入的数据源中就重复,还有一种是和数据库中的重复</para>
        /// <para>3.外键依赖性检查.根据编码检查外键记录是否存在并且唯一.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pDataItems">数据</param>
        /// <param name="oResults">如果检查不通过时,返回检查结果数组</param>
        /// <returns>是否检查通过</returns>
        protected abstract bool Check(IETCLDataItem[] pDataes, out CheckResult[] oResults);

        /// <summary>
        /// 当检查不通过时,调用本方法
        /// </summary>
        /// <param name="pResults">检查结果数组</param>
        protected abstract void OnCheckingNotPass(CheckResult[] pResults);

        /// <summary>
        /// 将检查通过的结果数组写入目的地
        /// </summary>
        /// <param name="pDataItems">数据</param>
        /// <returns></returns>
        protected abstract int Load(IETCLDataItem[] pDataItems);
        #endregion
    }
}
