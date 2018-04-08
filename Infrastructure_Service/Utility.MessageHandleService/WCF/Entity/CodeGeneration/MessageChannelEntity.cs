/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/3 17:21:10
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

namespace JIT.Utility.Message.WCF.Entity
{
    /// <summary>
    /// ʵ�壺 ��Ϣͨ�� 
    /// </summary>
    public partial class MessageChannelEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MessageChannelEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public Int32? ChannelID { get; set; }

		/// <summary>
		/// �ֻ���ƽ̨1=Android2=IOS
		/// </summary>
		public Int32? MobilePlatform { get; set; }

		/// <summary>
		/// ��JSON����ʽ�洢������
		/// </summary>
		public String Settings { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// �����ߵ��û�ID
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// �������ߵ��û�ID
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}