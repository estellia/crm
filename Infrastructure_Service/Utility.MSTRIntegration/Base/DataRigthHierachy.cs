using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// 用于控制数据权限的层系结构信息
    /// </summary>
    public class DataRigthHierachy
    {
        /// <summary>
        /// 所有权限限定条件
        /// </summary>
        private List<PromptAnswerItem> _lstHierachyItems = new List<PromptAnswerItem>();

        /// <summary>
        /// 添加一级权限控制
        /// </summary>
        /// <param name="pLevel">层级（从1开始）</param>
        /// <param name="pValues">当前层的限定值</param>
        public void Add(int pLevel, string[] pValues)
        {
            PromptAnswerItem promptAnswerItem = new PromptAnswerItem();
            promptAnswerItem.PromptCode = "JitHierachyLevel" + pLevel.ToString();
            promptAnswerItem.PromptType = PromptAnswerType.ElementsPromptAnswer;
            promptAnswerItem.QueryCondition = pValues;
            this._lstHierachyItems.Add(promptAnswerItem);
        }

        /// <summary>
        /// 清空当前数据权限控制信息
        /// </summary>
        public void Clear()
        {
           this. _lstHierachyItems = new List<PromptAnswerItem>();
        }

        /// <summary>
        /// 当前数据权限控制条件集合
        /// </summary>
        public PromptAnswerItem[] PromptAnswerItems
        {
            get
            {
                return this._lstHierachyItems.ToArray();
            }
        }
    }
}
