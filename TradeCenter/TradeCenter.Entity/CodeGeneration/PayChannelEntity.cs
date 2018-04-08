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
    /// ʵ�壺 ֧��ͨ��֧��ͨ��+������Ϣ �����֧���������������Ϣ�����Ӧ���¸����ͻ����տ��˻���һ��,��Ӧ���µĸ����ͻ���֧��ͨ���ǲ�ͬ�ġ� 
    /// </summary>
    public partial class PayChannelEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PayChannelEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// ֧������ID����������
        /// </summary>
        public Int32? ChannelID { get; set; }

        /// <summary>
        /// ֧����ʽ
        /// <remarks>
        /// <para>��ǰ֧�ֵ��У�</para>
        /// <para>1=����WAP֧��</para>
        /// <para>2=��������֧��</para>
        /// <para>3=֧����WAP֧��</para>
        /// <para>4=֧��������֧��</para>
        /// <para>5=΢��֧��</para>
        /// <para>6=����֧��</para>
        /// </remarks>
        /// </summary>
        public Int32? PayType { get; set; }

        /// <summary>
        /// ֧��ͨ���Ĳ���,�������͵�֧����ʽ�Ĳ����᲻һ������˲�����JSON�ĸ�ʽ�洢
        /// </summary>
        public String ChannelParameters { get; set; }

        /// <summary>
        /// �Ƿ��ǲ���Channel
        /// </summary>
        public Boolean? IsTest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NotifyUrl { get; set; }

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