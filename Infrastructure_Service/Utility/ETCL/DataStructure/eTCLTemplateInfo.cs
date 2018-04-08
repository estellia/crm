using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// ETCL通用处理类的模板结构及业务数据类
    /// </summary>
    public class ETCLTemplateInfo
    {
        /// <summary>
        /// 操作语言(1:中文，2:英文.)
        /// </summary>
        public int Lang { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string TemplateDescription { get; set; }

        /// <summary>
        /// 此模板所设计到的所有 业务WorkSheet 集合
        /// </summary>
        public List<ETCLWorkSheetInfo> WorkSheets { get; set; }

        /// <summary>
        /// 根据名称获取ETCL工作簿
        /// </summary>
        /// <param name="pWorksheetName">工作簿名称</param>
        /// <returns>ETCL工作簿</returns>
        public ETCLWorkSheetInfo GetWorksheetByName(string pWorksheetName)
        {
            foreach (var worksheetItem in WorkSheets)
            {
                if (worksheetItem.WorkSheetName == pWorksheetName)
                    return worksheetItem;
            }
            throw new ETCLException(300,string.Format("Excel中未找到工作簿:[{0}],请重新下载模板!", WorkSheets.Aggregate("", (i, j) => i + j.WorkSheetName + ",").Trim(',')));            
        }

        /// <summary>
        /// 根据名称获取ETCL表
        /// </summary>
        /// <param name="pTableName">表名称</param>
        /// <returns>ETCL表</returns>
        public ETCLTable GetTableByName(string pTableName)
        {
            foreach (var worksheetItem in WorkSheets)
            {
                var tableItem = worksheetItem.GetTableByName(pTableName);
                if (tableItem != null)
                    return tableItem;
            }
            return null;
        }
    }
}
