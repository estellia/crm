using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIServices
{
    public class Logger
    {
        public static void Writer(string msg)
        {
            var path = string.Empty;
            path = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var filePath = string.Empty;
            filePath = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (System.IO.File.Exists(filePath) == false)
            {
                System.IO.File.Exists(filePath);
            }

            var sr = System.IO.File.AppendText(filePath);
            sr.WriteLine("***************************************************************");
            sr.WriteLine("Datetime：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff"));
            sr.WriteLine(msg);
            sr.WriteLine("**************************************************************");
            sr.Close();
            sr.Dispose();

        }
    }
}