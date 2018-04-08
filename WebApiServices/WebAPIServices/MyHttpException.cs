using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
//using MagicBox.Log;

namespace WebAPIServices
{
    /// <summary>
    /// Web Api 异常处理
    /// </summary>
    [Serializable]
    public class MyHttpException : HttpResponseException
    {
        /// <summary>
        /// Http 异常处理
        /// </summary>
        /// <param name="ex">Exception</param>
        public MyHttpException(Exception ex)
            : base(
                  new HttpResponseMessage()
                  {
                      Content = new StringContent(ex.ToString(), Encoding.UTF8),
                      ReasonPhrase = ex.Source,
                  })
        {
            Logger.Writer(ex.Message.ToString());
        }
        /// <summary>
        /// Http 异常处理
        /// </summary>
        /// <param name="ReasonPhrase">异常短语</param>
        /// <param name="ex">Exception</param>
        public MyHttpException(string ReasonPhrase, Exception ex)
            : base(
                  new HttpResponseMessage()
                  {
                      Content = new StringContent(ex.ToString(), Encoding.UTF8),
                      ReasonPhrase = ReasonPhrase,
                  })
        {
            Logger.Writer("Http 异常处理：" + ReasonPhrase+ex.Message.ToString());
            Logger.Writer(ex.Message.ToString());
        }
    }
}
