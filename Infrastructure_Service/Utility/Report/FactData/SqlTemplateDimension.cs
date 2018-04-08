/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 13:28:35
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
using System.Data.SqlClient;
using System.Text;

using JIT.Utility.Locale;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.Utility.Report.FactData
{
    /// <summary>
    /// Sql模板的维度列 
    /// </summary>
    public class SqlTemplateDimension:IDimension
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SqlTemplateDimension()
        {
            this.HasIsDelete = false;
            this.IDFieldName = "IsDelete";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// ID字段名
        /// </summary>
        public string IDFieldName { get; set; }

        /// <summary>
        /// 根据语言获取文本字段名称
        /// </summary>
        public Func<Languages, string> GetTextFieldNameByLanguage;

        /// <summary>
        /// 数据访问助手
        /// </summary>
        public ISQLHelper SqlHelper { get; set; }

        /// <summary>
        /// SQL模板
        /// </summary>
        public string SqlTemplate { get; set; }

        /// <summary>
        /// 数据是否包含逻辑删除标志(IsDelete)
        /// </summary>
        public bool? HasIsDelete { get; set; }

        /// <summary>
        /// 逻辑删除字段的名称
        /// </summary>
        public string IsDeleteFieldName { get; set; }
        #endregion

        #region IDimension 成员

        /// <summary>
        /// 获取维度数据的文本
        /// </summary>
        /// <param name="pIDs">维度数据ID数组</param>
        /// <param name="pLanguage">用户的语言选择</param>
        /// <returns>维度数据的文本</returns>
        public Dictionary<string, string> GetTexts(string[] pIDs, Languages pLanguage)
        {
            if (pIDs == null || pIDs.Length <= 0)
                return new Dictionary<string,string>();
            //组织SQL语句
            List<IWhereCondition> wheres =new List<IWhereCondition>();
            wheres.Add(new InCondition<string>() { FieldName=this.IDFieldName, Values =pIDs });
            if (this.HasIsDelete.HasValue && this.HasIsDelete.Value == true)
            {
                wheres.Add(new EqualsCondition() { FieldName =this.IsDeleteFieldName,Value =1 });
            }

            string textFieldName = this.GetTextFieldNameByLanguage(pLanguage);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct {0},{1} ",this.IDFieldName,textFieldName);
            sql.AppendFormat(" from ({0}) a where 1=1 ",this.SqlTemplate);
            sql.AppendFormat(WhereConditions.GenerateWhereSentence(wheres.ToArray()));
            //执行并返回结果
            Dictionary<string, string> result = new Dictionary<string, string>();
            //
            using(SqlDataReader rdr =this.SqlHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    string key =null, value =null;
                    if (rdr[0] != DBNull.Value)
                    {
                        key = rdr[0].ToString();
                    }
                    if (rdr[1] != DBNull.Value)
                    {
                        value = rdr[1].ToString();
                    }
                    if (key != null)
                    {
                        result.Add(key, value);
                    }
                }
            }
            //返回结果
            return result;
        }
        #endregion
    }
}
