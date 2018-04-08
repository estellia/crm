using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Base
{
    public abstract class BaseETCL:IETCL
    {
        protected BasicUserInfo CurrentUserInfo { get; set; }
        /// <summary>
        /// 从源数据中抽取到的直接数据。
        /// </summary>
        protected DataTable OriginalData { get; set; }
        #region 处理
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="pSource">数据源(数据源可能会多种多样.它可能是一个文件名、可能是一个工作表对象、可能是一个工作簿)</param>
        /// <param name="pResult">导入结果</param>
        /// <returns>是否成功</returns>
        public abstract bool Process(object pSource, out ImportingResult pResult);
        #endregion

        /// <summary>
        /// 将原始数据解析为DataTable
        /// <remarks>
        /// <para>解析规则:</para>
        /// <para>1.默认第一行为列标题,之后的所有行都为数据行</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWorkSheet">工作簿对象</param>
        /// <param name="pResult">抽取结果详情</param>
        /// <returns>抽取出的DataTable</returns>
        public abstract DataTable Extract(object pSource, out IETCLResultItem[] pResult);	

        #region 对数据进行格式转换并将DataRow转换成强类型的IExcelDataItem实例
		/// <summary>
		/// 对数据进行格式转换并将DataRow转换成强类型的IExcelDataItem实例
        /// </summary>
        /// <param name="pSourceDataTable">从源取出的直接数据</param>
        /// <param name="pResult">转换处理结果详情</param>
		/// <returns>转换出的ETCL数据项</returns>
        protected abstract IETCLDataItem[] Transform(DataTable pSourceDataTable, out IETCLResultItem[] pResult);
        #endregion

        #region 对导入的数据执行检查
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
        protected abstract bool Check(IETCLDataItem[] pDataItems, out IETCLResultItem[] oResults);
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="pItems">需要导入的数据条目数组</param>
        /// <param name="pResult">加载结果详情</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>被加载的记录数</returns>
        protected abstract int Load(IETCLDataItem[] pItems, out IETCLResultItem[] pResult, IDbTransaction pTran);
        #endregion
         
    }
}
