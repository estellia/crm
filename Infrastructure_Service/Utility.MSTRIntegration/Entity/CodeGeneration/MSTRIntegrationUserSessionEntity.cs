/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/13 13:32:19
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
    /// ʵ�壺 �⻧ƽ̨���û���¼��վ��,����һ�������ļ�¼.MSTR����֤ģ����ݼ�¼�е���Ϣ������/���¼��� MSTR��Session. 
    /// </summary>
    public partial class MSTRIntegrationUserSessionEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MSTRIntegrationUserSessionEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public int? SessionID { get; set; }

		/// <summary>
		/// �ͻ�ID
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// �û�ID
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// ����ƽ̨��վ��Session ID
		/// </summary>
		public string WebSessionID { get; set; }

		/// <summary>
		/// ��վ�û���IP
		/// </summary>
		public string IP { get; set; }

		/// <summary>
		/// �Ƿ���IP,������IP���û���¼ƽ̨ʱ��IP�ͷ��ʱ����IP������һ�µ�.Ŀ���Ƿ�ֹ�û��������URL��������,��ɷǷ�����.
		/// </summary>
		public int? IsCheckIP { get; set; }

		/// <summary>
		/// �����Ա�ʶ.���������ʶ��Ӱ�쵽���Ե�����.����������Ա�ʶ��Windowsϵͳ�µ�,ͨ������Ϊ2052,Ӣ��(����)Ϊ1033
		/// </summary>
		public int? LCID { get; set; }

		/// <summary>
		/// �⻧ƽ̨��վ�Ƿ������MSTR���ɻỰ��һЩ����.ͨ���Ǹ�������վ����������.
		/// </summary>
		public int? IsChange { get; set; }

		/// <summary>
		/// �Ự������MSTR IServer������.
		/// </summary>
		public string MSTRIServerName { get; set; }

		/// <summary>
		/// �Ự������MSTR IServer�Ķ˿ںţ����IServer���õ���Ĭ�϶˿ڣ���ֵΪ0
		/// </summary>
		public int? MSTRIServerPort { get; set; }

		/// <summary>
		/// �Ự������MSTR�û����û���
		/// </summary>
		public string MSTRUserName { get; set; }

		/// <summary>
		/// �Ự������MSTR�û�������
		/// </summary>
		public string MSTRUserPassword { get; set; }

		/// <summary>
		/// �Ự������MSTR��Ŀ����Ŀ����
		/// </summary>
		public string MSTRProjectName { get; set; }

		/// <summary>
		/// MSTR Session��ID
		/// </summary>
		public string MSTRSessionID { get; set; }

		/// <summary>
		/// MSTR Session��״̬��Ϣ,����������Щ��Ϣ��ԭMSTR Session����.
		/// </summary>
		public string MSTRSessionState { get; set; }

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