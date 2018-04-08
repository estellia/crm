/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/29 19:01:51
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

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 顺序的钻取路由 
    /// </summary>
    public class SequenceDrillRouting:IDrillRouting
    {
        #region 构造函数
        /// <summary>
        /// 顺序的钻取路由 
        /// </summary>
        public SequenceDrillRouting()
        {
            this.DrillingRoute = new List<AnalysisReportDrilling>();
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 表格剖面数组
        /// </summary>
        public AnalysisReportDrillSection[] Sections { get; set; }

        /// <summary>
        /// 当前的索引
        /// </summary>
        private int CurrentIndex = 0;

        /// <summary>
        /// 钻入的路线
        /// </summary>
        private List<AnalysisReportDrilling> DrillingRoute { get; set; }

        /// <summary>
        /// 是否将第一个Section加入到钻入的路线
        /// </summary>
        private bool IsIncludeFirstSection = false;
        #endregion

        #region IDrillRouting 成员
        
        /// <summary>
        /// 是否可以钻入
        /// </summary>
        /// <param name="pTargetColumn">用户选择钻入的报表列</param>
        /// <returns>是否可以钻入</returns>
        public bool CanDrillIn(AnalysisColumn pTargetColumn)
        {
            if (this.Sections != null && this.Sections.Length > 0)
            {
                return this.CurrentIndex < this.Sections.Length - 1;
            }
            return false;
        }

        /// <summary>
        /// 当前的表格剖面
        /// </summary>
        public AnalysisReportDrillSection CurrentSection
        {
            get
            {
                return this.Sections[this.CurrentIndex];
            }
        }

        /// <summary>
        /// 钻入
        /// </summary>
        /// <param name="pTargetColumn">用户选择钻入的报表列</param>
        /// <param name="pDrilledDimValue">钻入项(是一个维度项)的值</param>
        /// <param name="pDrilledDimText">钻入项(是一个维度项)的文本</param>
        /// <returns>钻入后的表格剖面</returns>
        public AnalysisReportDrillSection DrillIn(AnalysisColumn pTargetColumn,string pDrilledDimValue,string pDrilledDimText)
        {
            if (this.CanDrillIn(pTargetColumn))
            {
                //插入默认钻入的第一个钻取剖面
                if (this.IsIncludeFirstSection == false)
                {
                    AnalysisReportDrilling drilling = new AnalysisReportDrilling();
                    drilling.To = this.Sections[0];
                    this.DrillingRoute.Insert(0, drilling);
                    this.IsIncludeFirstSection = true;
                }
                //设置当前钻取的值和文本
                this.DrillingRoute[this.CurrentIndex].DrilledDimText = pDrilledDimText;
                this.DrillingRoute[this.CurrentIndex].DrilledDimValue = pDrilledDimValue;
                //初始化下此钻取的相关信息
                AnalysisReportDrilling d = new AnalysisReportDrilling();
                d.From = this.Sections[this.CurrentIndex];
                d.To = this.Sections[this.CurrentIndex + 1];
                this.DrillingRoute.Add(d);
                //
                this.CurrentIndex++;

                return this.CurrentSection;
            }
            else
            {
                throw new NotSupportedException("当前不支持钻入.");
            }
        }

        /// <summary>
        /// 跳转到指定钻取剖面
        /// </summary>
        /// <param name="pSectionName">指定钻取剖面的ID</param>
        /// <param name="pConditionsByDrilling">由钻入带来的筛选条件,字典的KEY为钻取剖面的ID</param>
        /// <returns>如果跳转成功，则返回相应的钻取剖面</returns>
        public AnalysisReportDrillSection Goto(string pSectionID,Dictionary<string, IWhereCondition[]> pConditionsByDrilling)
        {
            AnalysisReportDrillSection section = null;
            //找到跳转后的钻取剖面
            for (int i = 0; i < this.Sections.Length; i++)
            {
                var s = this.Sections[i];
                if (s.SectionID == pSectionID)
                {
                    section = s;
                    this.CurrentIndex = i;
                    //
                    break;
                }
            }
            //处理钻取带来的筛选条件，保留部分，移除部分
            if (pConditionsByDrilling != null)
            {
                for (var i = this.CurrentIndex; i < this.Sections.Length; i++)
                {
                    pConditionsByDrilling.Remove(this.Sections[i].SectionID);
                }
            }
            //处理内部保存的钻取路线
            var isFind = false;
            var index =0;
            for (var i = 0; i < this.DrillingRoute.Count; i++)
            {
                if(!isFind)
                {
                    if (this.DrillingRoute[i].To.SectionID == pSectionID)
                    {
                        //清空跳转到的钻取剖面的钻取值
                        this.DrillingRoute[i].DrilledDimText = null;
                        this.DrillingRoute[i].DrilledDimValue = null;
                        //
                        isFind = true;
                        index =i+1;
                    }
                }
                else
                {
                    this.DrillingRoute.RemoveRange(index, this.DrillingRoute.Count - index);
                    break;
                }
            }
            //
            return section;
        }

        /// <summary>
        /// 获得钻取的路线
        /// </summary>
        /// <returns></returns>
        public AnalysisReportDrilling[] GetDrillingRoute()
        {
            //插入默认钻入的第一个钻取剖面
            if (this.IsIncludeFirstSection == false)
            {
                AnalysisReportDrilling drilling = new AnalysisReportDrilling();
                drilling.To = this.Sections[0];
                this.DrillingRoute.Insert(0, drilling);
                this.IsIncludeFirstSection = true;
            }
            return this.DrillingRoute.ToArray();
        }
        #endregion
    }
}
