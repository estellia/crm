using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.ETCL.Base
{
    public class ETCLTemplateInfo
    {
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

        public ETCLWorkSheetInfo GetWorksheetByName(string pWorksheetName)
        {
            foreach (var worksheetItem in WorkSheets)
            {
                if (worksheetItem.WorkSheetName == pWorksheetName)
                    return worksheetItem;
            }
            return null;
        }
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
