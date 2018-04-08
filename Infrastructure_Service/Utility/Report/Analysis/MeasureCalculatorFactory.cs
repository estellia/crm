/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/2 17:33:07
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
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 度量计算工厂 
    /// </summary>
    public static class MeasureCalculatorFactory
    {
        /// <summary>
        /// 创建度量计算委托
        /// <remarks>
        /// <para>使用前提：</para>
        /// <para>1.属性的值可以被强制转换为double</para>
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">T为事实数据实体类,该类必须实现接口IFactData</typeparam>
        /// <param name="pCalculationType">计算类型</param>
        /// <param name="pPropertyName">需要进行计算的属性名</param>
        /// <returns>度量计算委托</returns>
        public static Func<IFactData[], double> Create<T>(MeasureCalculatorTypes pCalculationType, string pPropertyName) where T : IFactData
        {
            switch (pCalculationType)
            {
                case MeasureCalculatorTypes.Sum:
                    {
                        //需要生成的Lamda表达式为：(pGroupingItems) => pGroupingItems.Sum(item=>((T)item).Sales_volume);
                        //step1:生成item=>((T)item).Sales_volume 子句
                        var para1 = Expression.Parameter(typeof(IFactData), "item");
                        var cast1 = Expression.Convert(para1, typeof(T));
                        var getPV1 = Expression.Property(cast1, pPropertyName);
                        var cast2 = Expression.Convert(getPV1, typeof(double));
                        var lamda1 = Expression.Lambda<Func<IFactData, double>>(cast2, para1);
                        //step2:生成(pGroupingItems) => pGroupingItems.Sum()子句
                        var para2 = Expression.Parameter(typeof(IFactData[]), "pGroupingItems");
                        var sum1 = Expression.Call(typeof(Enumerable), "Sum", new Type[] { typeof(IFactData) }, para2, lamda1);
                        var lamda2 = Expression.Lambda<Func<IFactData[], double>>(sum1, para2);
                        //step3:编译并返回
                        var method = lamda2.Compile();
                        return method;
                    }
                default:
                    throw new NotImplementedException();
            }
            //
            return null;
        }
    }
}
