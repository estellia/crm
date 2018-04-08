/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/21 11:48:32
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
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.Utility.MSTRIntegration.Entity
{
    /// <summary>
    /// ʵ�壺 MSTR�ı������,��Ҫ���ڼ���ʱ���ɷ���MSTR�����Url 
    /// </summary>
    public partial class MSTRReportEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MSTRReportEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public int? ReportID { get; set; }

		/// <summary>
		/// ��MSTRProject�����.���ڱ�ʾ���������ĸ���Ŀ
		/// </summary>
		public int? ProjectID { get; set; }

		/// <summary>
		/// ���������
		/// </summary>
		public string ReportName { get; set; }

		/// <summary>
		/// MSTR�б�������ID
		/// </summary>
		public string ReportGUID { get; set; }

		/// <summary>
		/// �������ͣ�1��Report��2��Document��
		/// </summary>
		public int? ReportType { get; set; }

		/// <summary>
		/// �鿴ģʽ��1�����2��ͼ�Σ�3�����+ͼ�Ρ���
		/// </summary>
		public int? ReportViewMode { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// �����ߵ��û�ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// �������ߵ��û�ID
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}