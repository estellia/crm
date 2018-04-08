/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/10 14:11:50
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
    /// ʵ�壺 MSTR����ʾ,���������õ���Prompt��Ϊ���֣�
    ///1.��������Ȩ��Prompt
    ///2.����ҳ���е����ݹ��˵�Prompt 
    /// </summary>
    public partial class MSTRPromptEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MSTRPromptEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public int? PromptID { get; set; }

		/// <summary>
		/// ���Ƿ�,����ƽ̨ͨ��PromptCode���ҵ���Ӧ��Prompt.��ͬ��Ŀ�µ�ͬһ��Prompt��Code��һ����.
		/// </summary>
		public string PromptCode { get; set; }

		/// <summary>
		/// Prompt�����GUID
		/// </summary>
		public string PromptGUID { get; set; }

		/// <summary>
		/// MSTR Prompt�����ͣ�1=ElementPrompt
		/// </summary>
		public int? PromptType { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// �����ߵ��û�ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
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