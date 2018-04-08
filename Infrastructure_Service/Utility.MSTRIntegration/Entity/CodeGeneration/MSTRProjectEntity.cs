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
    /// ʵ�壺 MSTR����Ŀ,�������Ϣ��Ҫ���ڴ���MSTR��Session��ͬʱ��Ŀ����ϢҲ�����ڼ���ʱ���ɷ���MSTR�����Url 
    /// </summary>
    public partial class MSTRProjectEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MSTRProjectEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public int? ProjectID { get; set; }

		/// <summary>
		/// ��MSTR��Ŀ�����ĸ��ͻ�,ͨ��Ϊһ���ͻ�����һ��MSTR��Ŀ,���,һ���ͻ����ֻ��һ��MSTR��Ŀ
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// MSTR IServer������
		/// </summary>
		public string IServerName { get; set; }

		/// <summary>
		/// MSTR IServer�Ķ˿�,����ǲ���Ĭ�϶˿�,��ֵΪ0
		/// </summary>
		public int? IServerPort { get; set; }

		/// <summary>
		/// ����MSTR����ĸ�Url
		/// </summary>
		public string WebServerBaseUrl { get; set; }

		/// <summary>
		/// MSTR��Ŀ������
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// MSTR����Ŀ��ID
		/// </summary>
		public string ProjectGUID { get; set; }

		/// <summary>
		/// ��MSTR��Ŀ��ʹ�õ�MSTR�û�������Щ��Ϣ���ڴ���MSTR��Session
		/// </summary>
		public string MSTRUserName { get; set; }

		/// <summary>
		/// MSTR�û�������,����Ϣ���ڴ���MSTR��Session
		/// </summary>
		public string MSTRUserPassword { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// �������û�ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// �������û�ID
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}