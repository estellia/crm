using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Customer
{
    /// <summary>
    /// �ͻ��µ��ն�
    /// </summary>
    [Serializable]
    [XmlRoot("data")]
    public class CustomerTerminalInfo : Base.ObjectOperateInfo
    {
        public CustomerTerminalInfo()
            : this(new CustomerInfo())
        { }

        public CustomerTerminalInfo(CustomerInfo customer)
            : base()
        {
            this.Customer = customer;
        }

        /// <summary>
        /// �����ͻ�
        /// </summary>
        [XmlIgnore()]
        public CustomerInfo Customer
        { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [XmlElement("terminal_id")]
        public string ID
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("terminal_type")]
        public string Type
        { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [XmlIgnore()]
        public string TypeDescription
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("terminal_code")]
        public string Code
        { get; set; }

        /// <summary>
        /// ���к�
        /// </summary>
        [XmlElement("terminal_sn")]
        public string SN
        { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [XmlElement("terminal_purchase_date")]
        public string PurchaseDate
        { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [XmlElement("terminal_insurance_date")]
        public string InsuranceDate
        { get; set; }

        /// <summary>
        /// ���ݽ�����ַ
        /// </summary>
        [XmlElement("terminal_ws")]
        public string WS
        { get; set; }

        /// <summary>
        /// �������ݽ�����ַ
        /// </summary>
        [XmlElement("terminal_ws2")]
        public string WS2
        { get; set; }

        /// <summary>
        /// ����İ汾
        /// </summary>
        [XmlElement("terminal_software_version")]
        public string SoftwareVersion
        { get; set; }

        /// <summary>
        /// ���ݿ�İ汾
        /// </summary>
        [XmlElement("terminal_db_version")]
        public string DBVersion
        { get; set; }

        /// <summary>
        /// ���з�ʽ
        /// </summary>
        [XmlElement("terminal_hold_type")]
        public string HoldType
        { get; set; }

        /// <summary>
        /// ���з�ʽ����
        /// </summary>
        [XmlIgnore()]
        public string HoldTypeDescription
        { get; set; }

        /// <summary>
        /// Ʒ��
        /// </summary>
        [XmlElement("terminal_brand")]
        public string Brand
        { get; set; }

        /// <summary>
        /// �ͺ�
        /// </summary>
        [XmlElement("terminal_model")]
        public string Model
        { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [XmlElement("terminal_remark")]
        public string Remark
        { get; set; }

        /// <summary>
        /// Ǯ��
        /// </summary>
        [XmlElement("terminal_have_cashbox")]
        public int HaveCashbox
        { get; set; }

        /// <summary>
        /// Ǯ����
        /// </summary>
        [XmlElement("terminal_cashbox_no")]
        public string CashboxNo
        { get; set; }

        /// <summary>
        /// СƱ��ӡ��
        /// </summary>
        [XmlElement("terminal_have_printer")]
        public int HavePrinter
        { get; set; }

        /// <summary>
        /// СƱ��ӡ�����
        /// </summary>
        [XmlElement("terminal_printer_no")]
        public string PrinterNo
        { get; set; }

        /// <summary>
        /// ɨ��ǹ
        /// </summary>
        [XmlElement("terminal_have_scanner")]
        public int HaveScanner
        { get; set; }

        /// <summary>
        /// ɨ��ǹ���
        /// </summary>
        [XmlElement("terminal_scanner_no")]
        public string ScannerNo
        { get; set; }

        /// <summary>
        /// ˢ����
        /// </summary>
        [XmlElement("terminal_have_ecard")]
        public int HaveEcard
        { get; set; }

        /// <summary>
        /// ˢ�������
        /// </summary>
        [XmlElement("terminal_ecard_no")]
        public string EcardNo
        { get; set; }

        /// <summary>
        /// ֧��
        /// </summary>
        [XmlElement("terminal_have_holder")]
        public int HaveHolder
        { get; set; }

        /// <summary>
        /// ֧�ܱ��
        /// </summary>
        [XmlElement("terminal_holder_no")]
        public string HolderNo
        { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [XmlElement("terminal_have_client_display")]
        public int HaveClientDisplay
        { get; set; }

        /// <summary>
        /// ���Ա��
        /// </summary>
        [XmlElement("terminal_client_display_no")]
        public string ClientDisplayNo
        { get; set; }

        /// <summary>
        /// �����豸
        /// </summary>
        [XmlElement("terminal_have_other_device")]
        public int HaveOtherDevice
        { get; set; }

        /// <summary>
        /// �����豸���
        /// </summary>
        [XmlElement("terminal_other_device_no")]
        public string OtherDeviceNo
        { get; set; }
    }
}
