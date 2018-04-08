namespace JIT.TestUtility.TestPay.TestAliWavePay
{
    partial class frmWavePay
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWavePay));
            this.btnCheckMicrophone = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWaveResult = new System.Windows.Forms.TextBox();
            this.nmWaveThreshold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nmWaveTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStopReceiveWave = new System.Windows.Forms.Button();
            this.btnStartReceiveWave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtWaveDynamicID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ctlWave = new AxSonicWaveNFCLib.AxSonicWaveNFC();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaveThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaveTimeout)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlWave)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckMicrophone
            // 
            this.btnCheckMicrophone.Location = new System.Drawing.Point(6, 87);
            this.btnCheckMicrophone.Name = "btnCheckMicrophone";
            this.btnCheckMicrophone.Size = new System.Drawing.Size(75, 23);
            this.btnCheckMicrophone.TabIndex = 1;
            this.btnCheckMicrophone.Text = "检查麦克风";
            this.btnCheckMicrophone.UseVisualStyleBackColor = true;
            this.btnCheckMicrophone.Click += new System.EventHandler(this.btnCheckMicrophone_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWaveResult);
            this.groupBox1.Controls.Add(this.nmWaveThreshold);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nmWaveTimeout);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnStopReceiveWave);
            this.groupBox1.Controls.Add(this.btnCheckMicrophone);
            this.groupBox1.Controls.Add(this.btnStartReceiveWave);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 468);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "声波";
            // 
            // txtWaveResult
            // 
            this.txtWaveResult.Location = new System.Drawing.Point(6, 116);
            this.txtWaveResult.Multiline = true;
            this.txtWaveResult.Name = "txtWaveResult";
            this.txtWaveResult.ReadOnly = true;
            this.txtWaveResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWaveResult.Size = new System.Drawing.Size(284, 332);
            this.txtWaveResult.TabIndex = 5;
            // 
            // nmWaveThreshold
            // 
            this.nmWaveThreshold.Location = new System.Drawing.Point(87, 48);
            this.nmWaveThreshold.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nmWaveThreshold.Name = "nmWaveThreshold";
            this.nmWaveThreshold.Size = new System.Drawing.Size(49, 21);
            this.nmWaveThreshold.TabIndex = 4;
            this.nmWaveThreshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "声幅阀值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "秒";
            // 
            // nmWaveTimeout
            // 
            this.nmWaveTimeout.Location = new System.Drawing.Point(87, 21);
            this.nmWaveTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nmWaveTimeout.Name = "nmWaveTimeout";
            this.nmWaveTimeout.Size = new System.Drawing.Size(49, 21);
            this.nmWaveTimeout.TabIndex = 3;
            this.nmWaveTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "声波接收超时：";
            // 
            // btnStopReceiveWave
            // 
            this.btnStopReceiveWave.Location = new System.Drawing.Point(168, 87);
            this.btnStopReceiveWave.Name = "btnStopReceiveWave";
            this.btnStopReceiveWave.Size = new System.Drawing.Size(75, 23);
            this.btnStopReceiveWave.TabIndex = 1;
            this.btnStopReceiveWave.Text = "停止接收";
            this.btnStopReceiveWave.UseVisualStyleBackColor = true;
            this.btnStopReceiveWave.Click += new System.EventHandler(this.btnStopReceiveWave_Click);
            // 
            // btnStartReceiveWave
            // 
            this.btnStartReceiveWave.Location = new System.Drawing.Point(87, 87);
            this.btnStartReceiveWave.Name = "btnStartReceiveWave";
            this.btnStartReceiveWave.Size = new System.Drawing.Size(75, 23);
            this.btnStartReceiveWave.TabIndex = 0;
            this.btnStartReceiveWave.Text = "开启接收";
            this.btnStartReceiveWave.UseVisualStyleBackColor = true;
            this.btnStartReceiveWave.Click += new System.EventHandler(this.btnStartReceiveWave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.webBrowser1);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtWaveDynamicID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(305, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 468);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "订单";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(61, 150);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "二维码";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(8, 150);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "条码";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(270, 11);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(176, 160);
            this.webBrowser1.TabIndex = 13;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 227);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(440, 235);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(337, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 38);
            this.button3.TabIndex = 7;
            this.button3.Text = "二维码预订单支付(扫)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "条码支付";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "声波支付";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWaveDynamicID
            // 
            this.txtWaveDynamicID.Location = new System.Drawing.Point(78, 23);
            this.txtWaveDynamicID.Name = "txtWaveDynamicID";
            this.txtWaveDynamicID.ReadOnly = true;
            this.txtWaveDynamicID.Size = new System.Drawing.Size(134, 21);
            this.txtWaveDynamicID.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "声波动态ID：";
            // 
            // ctlWave
            // 
            this.ctlWave.Enabled = true;
            this.ctlWave.Location = new System.Drawing.Point(657, 401);
            this.ctlWave.Name = "ctlWave";
            this.ctlWave.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ctlWave.OcxState")));
            this.ctlWave.Size = new System.Drawing.Size(100, 50);
            this.ctlWave.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "输入生成二维码的金额";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(251, 187);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 21);
            this.textBox1.TabIndex = 17;
            // 
            // frmWavePay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 471);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctlWave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWavePay";
            this.Text = "Alipay - 线下支付测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaveThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaveTimeout)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlWave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxSonicWaveNFCLib.AxSonicWaveNFC ctlWave;
        private System.Windows.Forms.Button btnCheckMicrophone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStopReceiveWave;
        private System.Windows.Forms.Button btnStartReceiveWave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmWaveTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmWaveThreshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWaveResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtWaveDynamicID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}

