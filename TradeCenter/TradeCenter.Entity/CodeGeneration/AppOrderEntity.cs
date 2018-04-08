/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
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

namespace JIT.TradeCenter.Entity
{
    /// <summary>
    /// ʵ�壺 Ӧ�ö����� 
    /// </summary>
    public partial class AppOrderEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AppOrderEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// ������������IDΪ���͸�֧��ƽ̨���ڱ�ʶ������Ϣ�Ķ���ID
        /// </summary>
        public Int64? OrderID { get; set; }

        /// <summary>
        /// Ӧ��ID����App�����
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// Ӧ�õĿͻ�ID
        /// </summary>
        public String AppClientID { get; set; }

        /// <summary>
        /// Ӧ�����µ����û���ID��
        /// </summary>
        public String AppUserID { get; set; }

        /// <summary>
        /// ֧��ͨ��ID����PayChannel�����
        /// </summary>
        public Int32? PayChannelID { get; set; }

        /// <summary>
        /// Ӧ�õĶ���ID
        /// </summary>
        public String AppOrderID { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public String AppOrderDesc { get; set; }

        /// <summary>
        /// �µ�ʱ��
        /// </summary>
        public DateTime? AppOrderTime { get; set; }

        /// <summary>
        /// �������(���ĵ�λΪ�����µ���С���ҵ�λ,���磺RMB��Ϊ��)
        /// </summary>
        public Int32? AppOrderAmount { get; set; }

        /// <summary>
        /// �����ߵ��ֻ��š���Ϊ��������֧��ʱ,���ֶα�����д��
        /// </summary>
        public String MobileNO { get; set; }

        /// <summary>
        /// ���֣�����ֵΪ��1=RMBĬ��ΪRMB
        /// </summary>
        public Int32? Currency { get; set; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public String ErrorMessage { get; set; }

        /// <summary>
        /// ֧����תҳ
        /// </summary>
        public String PayUrl { get; set; }

        /// <summary>
        /// �Ƿ���֪ͨ
        /// </summary>
        public Boolean? IsNotified { get; set; }

        /// <summary>
        /// ֪ͨ����
        /// </summary>
        public Int32? NotifyCount { get; set; }

        /// <summary>
        /// �´�֪ͨʱ��
        /// </summary>
        public DateTime? NextNotifyTime { get; set; }

        /// <summary>
        /// ״̬0=��ʼ1=Ԥ�����ɹ�2=֧���ɹ�
        /// </summary>
        public Int32? Status { get; set; }

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

        /// <summary>
        /// ΢���û�id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// �ͻ���ip
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// �첽֪ͨ��ַ
        /// </summary>
        public string NotifyUrl { get; set; }



        #endregion

    }
}