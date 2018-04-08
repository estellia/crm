/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/3 17:21:09
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
    /// ʵ�壺 ������Ϣ�� 
    /// </summary>
    public partial class MessageEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MessageEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public Guid? MessageID { get; set; }

        /// <summary>
        /// ����ID����MessageChannel�����
        /// </summary>
        public Int32? ChannelID { get; set; }

        /// <summary>
        /// ����Ϣ���ʹ��ĸ�APP��
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// ������Ϣ���û������Ŀͻ�ID
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// ������Ϣ���û��������û�ID
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public String MessageContent { get; set; }

        /// <summary>
        /// ������Ϣ�Ĳ���,����������ΪJSON
        /// </summary>
        public String MessageParameters { get; set; }

        /// <summary>
        /// ���͵Ĵ���
        /// </summary>
        public Int32? SendCount { get; set; }

        /// <summary>
        /// ״̬0=δ����1=����ʧ��2=���ͳɹ�
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// ʧ��ԭ��
        /// </summary>
        public String FaultReason { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// �������û�ID
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ���������û�ID
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}