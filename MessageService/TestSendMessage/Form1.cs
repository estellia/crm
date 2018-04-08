using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JIT.MessageService;
using JIT.MessageService.Entity;

namespace TestSendMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageHandler handler = new MessageHandler();
            SMSSendEntity entity = new SMSSendEntity();
            entity.SMSSendNO = 66472;
            entity.MobileNO = "18019438327";
            entity.SMSContent = "测试111";
            var str = handler.Process(entity);
        }
    }
}
