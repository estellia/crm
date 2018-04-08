using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Interface.Offline;
using JIT.Utility.Pay.Alipay.Interface.Offline.CreateAndPay;
using System.Net;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;

namespace JIT.TestUtility.TestPay.TestAliWavePay
{
    public partial class frmWavePay : Form
    {
        public frmWavePay()
        {
            InitializeComponent();
            //绑定声波控件事件
            this.ctlWave.OnReceiveDataStarted += new EventHandler(ctlWave_OnReceiveDataStarted);
            this.ctlWave.OnDataReceived += new AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnDataReceivedEventHandler(ctlWave_OnDataReceived);
            this.ctlWave.OnReceiveDataTimeout += new EventHandler(ctlWave_OnReceiveDataTimeout);
            this.ctlWave.OnReceiveDataFailed += new AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnReceiveDataFailedEventHandler(ctlWave_OnReceiveDataFailed);
            this.ctlWave.OnReceiveDataInfo += new AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnReceiveDataInfoEventHandler(ctlWave_OnReceiveDataInfo);
        }

        public int QCodeAmount
        {
            get
            {
                try
                {
                    return Convert.ToInt32(string.IsNullOrEmpty(this.textBox1.Text));
                }
                catch { return 1; }
            }
        }

        #region 声波控件事件处理
        /// <summary>
        /// 接收声波数据正常开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctlWave_OnReceiveDataStarted(object sender, EventArgs e)
        {
            this.txtWaveResult.Text += string.Format("接收声波数据正常开始.{0}", Environment.NewLine);
            this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
            this.txtWaveResult.ScrollToCaret();
        }
        /// <summary>
        /// 接收声波数据完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctlWave_OnDataReceived(object sender, AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnDataReceivedEvent e)
        {
            this.txtWaveResult.Text += string.Format("声波数据接收完毕.{0}", Environment.NewLine);
            this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
            this.txtWaveResult.ScrollToCaret();
            this.txtWaveDynamicID.Text = e.str;
            MessageBox.Show(string.Format("声波数据接收完毕.接收数据为:[{0}]!", e.str), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 接收声波数据超时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctlWave_OnReceiveDataTimeout(object sender, EventArgs e)
        {
            this.txtWaveResult.Text += string.Format("声波数据接收超时.{0}", Environment.NewLine);
            this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
            this.txtWaveResult.ScrollToCaret();
            MessageBox.Show("声波数据接收超时!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 接收声波数据失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctlWave_OnReceiveDataFailed(object sender, AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnReceiveDataFailedEvent e)
        {
            this.txtWaveResult.Text += string.Format("声波数据接收失败,失败原因为：{1}.{0}", Environment.NewLine, e.reason);
            this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
            this.txtWaveResult.ScrollToCaret();
            MessageBox.Show("声波数据接收失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 声波数据接收反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ctlWave_OnReceiveDataInfo(object sender, AxSonicWaveNFCLib._DSonicWaveNFCEvents_OnReceiveDataInfoEvent e)
        {
            this.txtWaveResult.Text += string.Format("声波数据：{1}{0}", Environment.NewLine, e.info);
            this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
            this.txtWaveResult.ScrollToCaret();
        }
        #endregion

        /// <summary>
        /// 检查麦克风
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckMicrophone_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var status = this.ctlWave.GetMicrophoneStatus();
                switch (status)
                {
                    case 0:
                        MessageBox.Show("麦克风已连接!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        MessageBox.Show("麦克风不存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show(string.Format("未知的麦克风状态值[{0}].", status), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 开启声波接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartReceiveWave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.txtWaveResult.Text = string.Empty;
                this.txtWaveDynamicID.Text = string.Empty;
                //
                var timeout = Convert.ToInt32(this.nmWaveTimeout.Value);
                var waveThreshold = Convert.ToInt16(this.nmWaveThreshold.Value);
                //
                this.txtWaveResult.Text += string.Format("开启声波接收：[超时时间={0}][声幅阀值={1}]{2}", timeout, waveThreshold, Environment.NewLine);
                this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
                this.ctlWave.StartReceiveData(timeout, waveThreshold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 停止声波接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopReceiveWave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.txtWaveResult.Text += string.Format("停止声波接收.{0}", Environment.NewLine);
                this.txtWaveResult.SelectionStart = this.txtWaveResult.TextLength;
                this.ctlWave.StopReceiveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CreateAndPayRequest request = new CreateAndPayRequest()
            //{
            //    SellerID = AliPayConfig.Partner_SoundWave,
            //    Subject = "测试",
            //    TotalFee = "0.01",
            //    Partner = AliPayConfig.Partner_SoundWave,
            //    OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
            //    DynamicIDType = "soundwave",
            //    DynamicID = this.txtWaveDynamicID.Text
            //};
            //var t = AliPayOffLineGeteway.OfflineCreateAndPay(request);

            //string url = "http://121.199.42.125:6001/DevPayTest.ashx?";
            string url = "http://localhost:1266/Gateway.ashx?";
            CreateOrderParameters para = new CreateOrderParameters()
            {
                AppOrderAmount = 1,
                AppOrderDesc = "测试",
                AppOrderID = Guid.NewGuid().ToString("N"),
                AppOrderTime = DateTime.Now.ToJITFormatString(),
                PayChannelID = 4,
                MobileNO = "18626336617",
                DynamicIDType = "soundwave",
                DynamicID = this.txtWaveDynamicID.Text
            };
            TradeRequest request1 = new TradeRequest()
            {
                AppID = 1,
                ClientID = "27",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=CreateOrder&request={0}", request1.ToJSON());
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(url, data);
            MessageBox.Show(res);
        }

        public static string GetResponseStr(string url, byte[] data)
        {
            Encoding code = Encoding.GetEncoding("utf-8");
            using (var stream = GetResponseStream(url, data))
            {
                StreamReader reader = new StreamReader(stream, code);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }
                return responseData.ToString();
            }
        }

        public static Stream GetResponseStream(string url, byte[] data)
        {
            Encoding code = Encoding.GetEncoding("utf-8");
            //请求远程HTTP
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                var stream = myReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                //发送POST数据请求服务器
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();

                //获取服务器返回信息
                return myStream;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Width = 200;
            frm.Height = 30;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            TextBox txtbox = new TextBox();
            txtbox.Multiline = true;
            txtbox.Height = frm.Height;
            txtbox.Dock = DockStyle.Fill;
            frm.Controls.Add(txtbox);
            frm.KeyPreview = true;
            txtbox.KeyPress += (s, t) =>
                {
                    TextBox tt = s as TextBox;
                    if (t.KeyChar == (char)Keys.Enter)
                    {
                        ((Form)tt.Parent).Close();
                    }
                };
            frm.ShowDialog();
            //CreateAndPayRequest request = new CreateAndPayRequest()
            //{
            //    SellerID = AliPayConfig.Partner_SoundWave,
            //    Subject = "测试",
            //    TotalFee = "0.01",
            //    Partner = AliPayConfig.Partner_SoundWave,
            //    OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
            //    DynamicIDType = "soundwave",
            //    DynamicID = this.txtWaveDynamicID.Text
            //};
            //var t = AliPayOffLineGeteway.OfflineCreateAndPay(request);

            string url = "http://121.199.42.125:6001/DevPayTest.ashx?";

            CreateOrderParameters para = new CreateOrderParameters()
            {
                AppOrderAmount = 1,
                AppOrderDesc = "测试",
                AppOrderID = Guid.NewGuid().ToString("N"),
                AppOrderTime = DateTime.Now.ToJITFormatString(),
                PayChannelID = 4,
                MobileNO = "18626336617",
                DynamicIDType = this.radioButton1.Checked ? "barcode" : "qrcode",
                DynamicID = txtbox.Text
            };
            TradeRequest request1 = new TradeRequest()
            {
                AppID = 1,
                ClientID = "27",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=CreateOrder&request={0}", request1.ToJSON());
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(url, data);
            this.richTextBox1.Text += res.ToJSON();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "http://121.199.42.125:6001/DevPayTest.ashx?";
            CreateOrderParameters para = new CreateOrderParameters()
            {
                AppOrderAmount = QCodeAmount,
                AppOrderDesc = "测试",
                AppOrderID = Guid.NewGuid().ToString("N"),
                AppOrderTime = DateTime.Now.ToJITFormatString(),
                PayChannelID = 4,
                MobileNO = "18626336617",
            };
            TradeRequest request1 = new TradeRequest()
            {
                AppID = 1,
                ClientID = "27",
                Parameters = para,
                UserID = "1111"
            };
            string parameter = string.Format("action=CreateOrder&request={0}", request1.ToJSON());
            var data = Encoding.GetEncoding("utf-8").GetBytes(parameter);
            var res = GetResponseStr(url, data);
            var response = res.DeserializeJSONTo<TradeResponse>();
            var imageurl = response.Datas.ToJSON().DeserializeJSONTo<CreateOrderResponse>().QrCodeUrl;
            //using (var stream = GetResponseStream(imageurl, new byte[] { }))
            //{
            //    using (Bitmap bm = new Bitmap(stream))
            //    {
            //        this.pictureBox1.Image = bm;
            //    }
            //}
            this.webBrowser1.DocumentText = string.Format("<img src='{0}' style='width:120px;height:120px;' />", imageurl);
        }
    }

    public class CreateOrderParameters
    {
        /// <summary>
        /// 对应数据库中的Channel,1-AliPayWap支付,2-Union语音支付,3-UnionWap支付
        /// </summary>
        public int? PayChannelID { get; set; }
        /// <summary>
        /// 客户端提交的订单ID
        /// </summary>
        public string AppOrderID { get; set; }
        /// <summary>
        /// 提交订单时间
        /// </summary>
        public string AppOrderTime { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public int? AppOrderAmount { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string AppOrderDesc { get; set; }
        /// <summary>
        /// 货币种类,当前只支持1-人民币
        /// </summary>
        public int? Currency { get; set; }
        /// <summary>
        /// 用于语音支付的电话号码
        /// </summary>
        public string MobileNO { get; set; }

        /// <summary>
        /// 动态ID,用于声波,条码,二维码面支付(非预订单)
        /// </summary>
        public string DynamicID { get; set; }

        /// <summary>
        /// 动态ID类型
        /// <remarks>
        /// <para>声波:soundwave</para>
        /// <para>二维码:qrcode</para>
        /// <para>条码:barcode</para>
        /// </remarks>
        /// </summary>
        public string DynamicIDType { get; set; }

        public DateTime GetDateTime()
        {
            if (string.IsNullOrEmpty(this.AppOrderTime))
                throw new Exception("AppOrderTime不能为空");
            return DateTime.ParseExact(this.AppOrderTime, "yyyy-MM-dd HH:mm:ss fff", null);
        }
    }

    public class TradeRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TradeRequest()
        {
        }
        #endregion

        public int? AppID { get; set; }

        /// <summary>
        /// 发送交易请求的客户ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 发送交易请求的用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 预留，用于安全认证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 请求参数
        /// <remarks>
        /// <para>请求参数与请求操作一一对应</para>
        /// </remarks>
        /// </summary>
        public object Parameters { get; set; }

        public T GetParameter<T>()
        {
            if (this.Parameters == null)
                return default(T);
            else
                return this.Parameters.ToJSON().DeserializeJSONTo<T>();
        }

        public BasicUserInfo GetUserInfo()
        {
            BasicUserInfo user = new BasicUserInfo() { ClientID = this.ClientID, UserID = this.UserID };
            return user;
        }
    }

    public class TradeResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TradeResponse()
        {
        }
        #endregion

        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 交易响应返回的数据
        /// </summary>
        public object Datas { get; set; }
    }

    public class CreateOrderResponse
    {
        /// <summary>
        /// 返回状态码:0-99 成功,100-199 UnionPayWap请求订单失败,200-299 UnionPayIVR请求订单失败,300-399 AliPayWap请求订单失败
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public long? OrderID { get; set; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// 二维码地址(Offline支付时)
        /// </summary>
        public string QrCodeUrl { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
}
